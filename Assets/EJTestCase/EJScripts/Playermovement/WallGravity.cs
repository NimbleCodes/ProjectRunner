using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGravity : MonoBehaviour
{

    Rigidbody rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
           rig.useGravity = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            rig.useGravity = true;  
        }
    }



}
