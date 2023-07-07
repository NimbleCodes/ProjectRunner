using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    [SerializeField] Transform _cam;
    [SerializeField] GameObject _getItemPanel;
    [SerializeField] GameObject[] _itemSlot;
    [SerializeField] Transform _Weapons; 
    public float _playerActionDistance;
    public bool active = false;
    RaycastHit hit;
    GameObject _holding, _objName;


    public Rigidbody rig;
    public BoxCollider coll;
    public Transform Player, FpsCam;

    public float pickUpRange;
    public bool equipped;
    public static bool slotfull;

    void Update()
    {
        FullItemSlot(); 

        Vector3 distanceToPlayer = hit.collider.transform.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && !slotfull)
        {
            PickUpItem();
        }
    }

    private void FullItemSlot()
    {
        active = Physics.Raycast(_cam.position, _cam.TransformDirection(Vector3.forward), out hit, _playerActionDistance);
        if (active == true && hit.collider.CompareTag("Weapon"))
        {
            _getItemPanel.SetActive(true);
        }
        if (active == false)
        {
            _getItemPanel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F) && active == true)
        {
            _itemSlot[0].gameObject.SetActive(true);
            _itemSlot[0].GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/ItemIcons/" + hit.collider.name);
            _objName = hit.collider.gameObject; 
        }
    }

    private void PickUpItem()
    {
        equipped = true;
        slotfull = true;
        rig.isKinematic = true;
        coll.isTrigger = true;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/" + _itemSlot[0].GetComponent<Image>().sprite.name);
            if(obj.name == _itemSlot[0].GetComponent<Image>().sprite.name)
            {
                _holding = Instantiate(obj, _Weapons);
                _holding.transform.localPosition = _Weapons.transform.localPosition;
                _holding.transform.localRotation = _Weapons.transform.localRotation;
                transform.localScale = _Weapons.transform.localScale;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
    }

}
