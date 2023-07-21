using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public bool onGround = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Jumping"))
        {
            Player.GetComponent<Movement>()._isGround = true;
            //_jumpCount = 0;
            onGround = true;
        }
    }

    

    private void OnCollisionExit(Collision collision)
    {
        Player.GetComponent<Movement>()._isGround = false;
    }
}
