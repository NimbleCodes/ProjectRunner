using UnityEngine;

public class PickUpItem : MonoBehaviour 
{
    Rigidbody _rig;
    BoxCollider coll;
    Transform _player;
    GameObject _effect;
    private float pickUpRange = 1.8f;
    private ItemController _itemCon;
    private bool _isInside; 
    private bool _isItmeDestroy = false;
    public bool isItemDestory { get { return _isItmeDestroy; } set { _isItmeDestroy = value; } }
    public Transform player {get{return _player;} set{_player = value;}}
    public bool _FpannelOn = true; 

    void Start()
    {
        _rig = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
        _itemCon = GetComponent<ItemController>();
        _player = GameObject.FindGameObjectWithTag("WeaponPoint").transform;
        _effect = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        Vector3 distance2Player = (_player.position - transform.position);
        if (distance2Player.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F) && _isInside == false && Inventory.Instance._AllslotFull == false)
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        Inventory.Instance.Add(_itemCon);
        Inventory.Instance.ShowItemImg(_itemCon);
        Destroy(_effect); 
        this.gameObject.SetActive(false);
        _isInside = true;
        _FpannelOn = false;
    }

}


