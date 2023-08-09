using UnityEngine;
using UnityEngine.UI;

public class PickUpItem1 : MonoBehaviour
{
    Rigidbody _rig;
    BoxCollider coll;
    [SerializeField] Transform _player, _camPoint, _itemContainer;
    [SerializeField] float pickUpRange;
    [SerializeField] float dropFowardForce, dropUpWardForce;
    [SerializeField] Image[] _itemSlot;

    public ItemList Item; 
    public bool equipped = false;
    public static bool _slotFull = false;
    

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        if (!equipped)
        {
            _rig.isKinematic = false;
            coll.isTrigger = false;
        }
        if (equipped)
        {
            _rig.isKinematic = true;
            coll.isTrigger = true;
            _slotFull = true;
        }
    }
    void Update()
    {
        Vector3 distance2Player = _player.position - transform.position;
        if (!equipped && distance2Player.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F))
        {
            PickUp();
        }
        //if (!equipped && distance2Player.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F) && !_slotFull)
        //{
        //    PickUp();
        //}
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }

    public void PickUp()
    {
        Inventory.Instance.Add(Item);
        Destroy(gameObject);
        equipped = true;
    }


    public void Grab()
    {
        if(_slotFull == false)
        {
            equipped = true;
            _slotFull = true;
            //�������� �÷��̾��� �ڽ� ������Ʈ�� ����
            transform.SetParent(_itemContainer);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
            //transform.localRotation = Quaternion.Euler(new Vector3(0,180,0));
            transform.localScale = Vector3.one;

            _rig.isKinematic = true;
            coll.isTrigger = true;

            //enable item Script
        }
    }

    public void ShowItemImg()
    {
        if (equipped == true)
        {
            for (int i = 0; i < _itemSlot.Length; i++)
            {
                Color color = _itemSlot[i].color;
                color.a = 1f;
                _itemSlot[i].color = color; 
                _itemSlot[i].sprite = Item.itemImage; 
                return; 
            }
        }
    }

    public void Drop()
    {
        equipped = false;
        _slotFull = false;
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
