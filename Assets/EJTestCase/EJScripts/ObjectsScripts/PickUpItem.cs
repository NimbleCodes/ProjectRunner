using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
    RaycastHit hit;
    public Rigidbody rig;
    public Collider coll; 
    public Transform Player, FpsCam;

    public float pickUpRange;
    public bool equipped;
    public static bool slotfull;

    [SerializeField] GameObject[] _itemSlot;
    [SerializeField] Transform _Weapons;
    GameObject _holding; 

    void Update()
    {
        //Vector3 distanceToPlayer = hit.collider.transform.position - transform.position;
        //if (!equipped && distanceToPlayer.magnitude <= pickUpRange && !slotfull)
        //{
            //PickItem();
        //}

        PickItem(); 
    }

    private void PickItem()
    {
        equipped = true;
        slotfull = true;
        rig.isKinematic = true;
        coll.isTrigger = true;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/" + _itemSlot[0].GetComponent<Image>().sprite.name);
            obj.transform.SetParent(_Weapons.transform); 
            //_holding = Instantiate(obj, _Weapons);
            obj.transform.localPosition = _Weapons.transform.localPosition;
            obj.transform.localRotation = _Weapons.transform.localRotation;

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

        }
    }
}
