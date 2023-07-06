using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public Rigidbody rig;
    public BoxCollider coll;
    public Transform Player, Weapom, TpsCam;

    public float pickUpRange;
    public bool equipped;
    public static bool slotfull;

    void Update()
    {
        Vector3 distanceToPlayer = Player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.Alpha1) && !slotfull)
        {
            PickUp();
        }

    }

    private void PickUp()
    {
        equipped = true;
        slotfull = true;

        //transform.SetParent(WeaponPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero); 
        transform.localScale = Vector3.one;


        rig.isKinematic = true;
        coll.isTrigger = true;
    }
}
