using UnityEngine;

public class PickUpItem : MonoBehaviour
{
        Rigidbody _rig;
        BoxCollider coll;
        [SerializeField] Transform _player;
        private float pickUpRange = 1.8f;
        private float dropFowardForce = 4f;
        private float dropUpWardForce = 2f; 
        private ItemController _itemCon;
        private bool _isInside; 

        private bool _isItmeDestroy = false;
        public bool isItemDestory { get { return _isItmeDestroy; } set { _isItmeDestroy = value; } }
        public Transform player {get{return _player;} set{_player = value;}}

        void Start()
        {
            _rig = GetComponent<Rigidbody>();
            coll = GetComponent<BoxCollider>();
            _itemCon = GetComponent<ItemController>();
        }

        void Update()
        {
            Vector3 distance2Player = (_player.position - transform.position);
            if (distance2Player.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F) && _isInside == false)
            {
                PickUp();
            }
        }

        public void PickUp()
        {
            Inventory.Instance.Add(_itemCon);
            Inventory.Instance.ShowItemImg(_itemCon);
            this.gameObject.SetActive(false);
            _isInside = true;
        }

}


