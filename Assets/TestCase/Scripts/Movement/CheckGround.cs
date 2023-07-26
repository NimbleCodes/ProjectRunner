using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public bool onGround = false;
    Rigidbody _rig;
    int _jumpCount; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Jumping"))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onGround && _jumpCount > 0)
            {
                onGround = false;
                GetComponent<Animator>().Play("Jumping");
                _rig.AddForce(Vector2.up * 5, ForceMode.Impulse);
                _jumpCount--; 
            }
        }
    }

       
}





    //void Start()
    //{
    //    RaycastHit hit;
    //}

    //void Update()
    //{
    //    Ray ray = new Ray(transform.position, Vector3.down);
    //    int layerMask = (1 << LayerMask.NameToLayer("Ground"));
    //    bool result = Physics.Raycast(ray, out hit, layerMask);

    //    if (result)
    //    {
    //        if (hit.distance < 0.01f)
    //        {
    //            GetComponent<Animator>().SetBool("_isGround", true);
    //            onGround = true;
    //        }
    //        else if (hit.distance > 0.01f)
    //        {
    //    
    //            onGround = false;
    //        }
    //    }
    //}


