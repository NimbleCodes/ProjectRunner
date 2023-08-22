using UnityEngine;

public class PickUpItem : MonoBehaviour
{
        Rigidbody _rig;
        BoxCollider coll;
        [SerializeField] Transform _player; 
        [SerializeField] float pickUpRange;
        [SerializeField] float dropFowardForce, dropUpWardForce;
        [SerializeField] GameObject[] _itemSlot;
        ItemController _itemCon;

        private bool _isItmeDestroy = false;
        public bool isItemDestory { get { return _isItmeDestroy; } set { _isItmeDestroy = value; } }
        public Transform player {get{return _player;} set{_player = value;}}

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
            Vector3 distance2Player = (_player.position - transform.position);
            if (distance2Player.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.F))
            {
                PickUp();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
               // Drop();
            }
        }

        public void PickUp()
        {
            Inventory.Instance.Add(_itemCon);
            Inventory.Instance.ShowItemImg(_itemCon);
            this.gameObject.SetActive(false);
        }

        //public void GrabSlot1()
        //{
        //    if (_itemSlot[0].GetComponent<Slot>().isItemIn == true && Input.GetKeyDown(KeyCode.Alpha1))
        //    {
        //        this.gameObject.SetActive(true); 
        //        //�������� �÷��̾��� �ڽ� ������Ʈ�� ����
        //        transform.SetParent(_itemContainer);
        //        transform.localPosition = Vector3.zero;
        //        transform.localRotation = Quaternion.Euler(Vector3.zero);
        //        transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        //        transform.localScale = Vector3.one;

        //        _rig.isKinematic = true;
        //        coll.isTrigger = true;
        //    }
        //}

        public void DestroyItem()
        {
            // ���ʹ� �±׸� �ް� �ִ� ������ �ݶ��̴� �浹 �� ������ 1�� ��� ����� 
            isItemDestory = true;
            Inventory.Instance.Remove(_itemCon);
            Inventory.Instance.DeleteItemImg(_itemCon);
        }


//        public void Drop()
//        {
//            //equipped = false;
//            //_slotFull = false;
//            //�������� �θ� ������Ʈ�� "����"���� ����
//            transform.SetParent(null);
//            //�θ� ������Ʈ�� �ڼ����� �پ��ִ� ������Ʈ�� �ڷ¾������� �����ϰ�
//            //�ݶ��̴� Ʈ���� ���� ����
//            _rig.isKinematic = false;
//            coll.isTrigger = false;
//            _rig.useGravity = true;
//            //�����ۿ� �÷��̾��� ������� ���� //�������� ������ �ڿ��������� �߱�
//            _rig.velocity = _player.GetComponent<Rigidbody>().velocity;
//            _rig.AddForce(_camPoint.forward * dropFowardForce, ForceMode.Impulse);
//            _rig.AddForce(_camPoint.up * dropUpWardForce, ForceMode.Impulse);
//            //�������� ������ �����ϰ� ȸ���ϵ��� �ϴ� �ڵ�
//            float random = Random.Range(-1f, 1f);
//            _rig.AddTorque(new Vector3(random, random, random) * 10f);
//            //������ ��ũ��Ʈ ����
//            //GetComponent<itemScript>().enalble = false;
//        }
}


