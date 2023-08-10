using UnityEngine;
using UnityEngine.UI;

public class PickUpItem1 : MonoBehaviour
{
    Rigidbody _rig;
    BoxCollider coll;
    [SerializeField] Transform _player, _camPoint, _itemContainer;
    [SerializeField] float pickUpRange;
    [SerializeField] float dropFowardForce, dropUpWardForce;
    [SerializeField] GameObject[] _itemSlot;

    public ItemList Item; 
    private bool equipped = false;
    public bool _AllslotFull = false; 

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        //if (!equipped)
        //{
        //    _rig.isKinematic = false;
        //    coll.isTrigger = false;
        //}
        //if (equipped)
        //{
        //    _rig.isKinematic = true;
        //    coll.isTrigger = true;
        //    _slotFull = true;
        //}
    }
    void Update()
    {
        Vector3 distance2Player = _player.position - transform.position;
        if (distance2Player.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F))
        {
            PickUp();
        }
        //if (!equipped && distance2Player.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F) && !_slotFull)
        //{
        //    PickUp();
        //}
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    public void PickUp()
    {
       if(_AllslotFull == false)
       {
            Inventory.Instance.Add(Item);
            ShowItemImg();
            Destroy(gameObject);
       }
    }

    public void ShowItemImg()
    {
       for (int i = 0; i < 3; i++)
       {
            if (_itemSlot[i] == null)
            {
                _itemSlot[i].SetActive(true);
                _itemSlot[i].GetComponent<Image>().sprite = Instantiate(Item.itemImage);
                _AllslotFull = true;
            }
       }
        
    }


    //public void Grab()
    //{
    //    if(_slotFull == false)
    //    {
    //        equipped = true;
    //        _slotFull = true;
    //        //아이템을 플레이어의 자식 오브젝트로 변경
    //        transform.SetParent(_itemContainer);
    //        transform.localPosition = Vector3.zero;
    //        transform.localRotation = Quaternion.Euler(Vector3.zero);
    //        //transform.localRotation = Quaternion.Euler(new Vector3(0,180,0));
    //        transform.localScale = Vector3.one;

    //        _rig.isKinematic = true;
    //        coll.isTrigger = true;

    //        //enable item Script
    //    }
    //}


    public void Drop()
    {
        //equipped = false;
        //_slotFull = false;
        //아이템의 부모 오브젝트를 "없음"으로 변경
        transform.SetParent(null);
        //부모 오브젝트에 자석으로 붙어있던 오브젝트를 자력없음으로 변경하고
        //콜라이더 트리거 상태 해제
        _rig.isKinematic = false;
        coll.isTrigger = false;
        _rig.useGravity = true;
        //아이템에 플레이어의 모멘텀을 적용 //아이템을 버릴때 자연스러움을 추구
        _rig.velocity = _player.GetComponent<Rigidbody>().velocity;
        _rig.AddForce(_camPoint.forward * dropFowardForce, ForceMode.Impulse);
        _rig.AddForce(_camPoint.up * dropUpWardForce, ForceMode.Impulse);
        //아이템을 버릴때 랜덤하게 회전하도록 하는 코드
        float random = Random.Range(-1f, 1f);
        _rig.AddTorque(new Vector3(random, random, random) * 10f);
        //아이템 스크립트 끄기
        //GetComponent<itemScript>().enalble = false;
    }

}
