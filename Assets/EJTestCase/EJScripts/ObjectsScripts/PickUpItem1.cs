using Unity.VisualScripting;
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
    ItemController _itemCon;
    private bool equipped = false;
    public bool _AllslotFull = false;
    public bool _Fullslot = false;

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        _itemCon = GetComponent<ItemController>();
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
      _Fullslot = true;
      Inventory.Instance.Add(_itemCon);
      Inventory.Instance.ShowItemImg(_itemCon);
      Destroy(gameObject);
    }

    public void DestroyItem()
    {

    }


    //public void Grab()
    //{
    //    if(_slotFull == false)
    //    {
    //        equipped = true;
    //        _slotFull = true;
    //        //�������� �÷��̾��� �ڽ� ������Ʈ�� ����
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
        //�������� �θ� ������Ʈ�� "����"���� ����
        transform.SetParent(null);
        //�θ� ������Ʈ�� �ڼ����� �پ��ִ� ������Ʈ�� �ڷ¾������� �����ϰ�
        //�ݶ��̴� Ʈ���� ���� ����
        _rig.isKinematic = false;
        coll.isTrigger = false;
        _rig.useGravity = true;
        //�����ۿ� �÷��̾��� ������� ���� //�������� ������ �ڿ��������� �߱�
        _rig.velocity = _player.GetComponent<Rigidbody>().velocity;
        _rig.AddForce(_camPoint.forward * dropFowardForce, ForceMode.Impulse);
        _rig.AddForce(_camPoint.up * dropUpWardForce, ForceMode.Impulse);
        //�������� ������ �����ϰ� ȸ���ϵ��� �ϴ� �ڵ�
        float random = Random.Range(-1f, 1f);
        _rig.AddTorque(new Vector3(random, random, random) * 10f);
        //������ ��ũ��Ʈ ����
        //GetComponent<itemScript>().enalble = false;
    }

}
