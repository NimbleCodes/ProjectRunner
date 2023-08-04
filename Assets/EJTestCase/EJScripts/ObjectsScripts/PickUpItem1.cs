using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem1 : MonoBehaviour
{
    Rigidbody _rig;
    MeshCollider _meshcoll;
    [SerializeField] Transform _player, _camPoint, _itemContainer;
    [SerializeField] float pickUpRange;
    [SerializeField] float dropFowardForce, dropUpWardForce;
    [SerializeField] GameObject[] _itemSlot;

    public bool equipped;
    public static bool _slotFull;

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _meshcoll = GetComponent<MeshCollider>();
        if (!equipped)
        {
            _rig.isKinematic = false;
            _meshcoll.isTrigger = false;
        }
        if (equipped)
        {
            _rig.isKinematic = true;
            _meshcoll.isTrigger = true;
            _slotFull = true;
        }
    }
    void Update()
    {
        Vector3 distance2Player = _player.position - transform.position;
        if (!equipped && distance2Player.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F) && !_slotFull)
        {
            PickUp();
        }
        if (equipped && Input.GetKeyDown(KeyCode.Q))
        {
            Drop();
        }
    }


    public void PickUp()
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
        _meshcoll.isTrigger = true;

        //enable item Script

        ShowItemImg();
    }

    public void ShowItemImg()
    {
        _itemSlot[0].SetActive(true);
        _itemSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/ItemIcons/" + gameObject.name);
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
        _meshcoll.isTrigger = false;
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
