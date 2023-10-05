using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenner : MonoBehaviour
{
    [SerializeField] Animator _door;

    void OnTriggerEnter(Collider other)
    {
        _door.SetBool("Open", true);
    }
}
