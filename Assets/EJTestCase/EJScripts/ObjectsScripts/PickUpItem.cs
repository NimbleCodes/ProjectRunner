using UnityEngine;

public class PickUpItem : MonoBehaviour
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
        public bool _isItmeDestroy = false;
        public bool isItemDestory { get { return _isItmeDestroy; } set { _isItmeDestroy = value; } }


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
                Drop();
            }
        }

        public void PickUp()
        {
            Inventory.Instance.Add(_itemCon);
            Inventory.Instance.ShowItemImg(_itemCon);
            Destroy(gameObject);
        }

        public void GrabSlot1()
        {
            if (_itemSlot[0].GetComponent<Slot>().isItemIn == true && Input.GetKeyDown(KeyCode.Alpha1))
            {
                equipped = true;
                //아이템을 플레이어의 자식 오브젝트로 변경
                transform.SetParent(_itemContainer);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
                transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                transform.localScale = Vector3.one;

                _rig.isKinematic = true;
                coll.isTrigger = true;
            }
        }

        public void DestroyItem()
        {
            // 에너미 태그를 달고 있는 애한테 콜라이더 충돌 시 내구도 1씩 닳게 만들기 
            isItemDestory = true;
            Inventory.Instance.Remove(_itemCon);
            Inventory.Instance.DeleteItemImg(_itemCon);
        }


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


