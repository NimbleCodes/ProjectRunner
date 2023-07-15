using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    
    Rigidbody _rig;
    [SerializeField] float _speed;


    void Start()
    {
        _rig = this.GetComponent<Rigidbody>();
        _rig.freezeRotation = true;
    }


    void Update()
    {
        MoveLocalTransform();
    }

    void MoveLocalTransform()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 vPos = transform.position;

        vPos += transform.right * x * Time.deltaTime * _speed;
        vPos += transform.forward * y * Time.deltaTime * _speed;
        transform.position = vPos;

  
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Animator>().SetBool("IsRun", true);
            GetComponent<Animator>().Play("FastRun");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Animator>().SetBool("IsBack", true);
            GetComponent<Animator>().Play("RunningBack");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            GetComponent<Animator>().SetBool("IsRun", false);
            GetComponent<Animator>().Play("Idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            GetComponent<Animator>().SetBool("IsRun", false);
            GetComponent<Animator>().Play("Idle");
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            GetComponent<Animator>().SetBool("IsRun", true); 
            GetComponent<Animator>().Play("FastRun");
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
            GetComponent<Animator>().SetBool("IsRun", true);
            GetComponent<Animator>().Play("FastRun");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
            GetComponent<Animator>().SetBool("IsRun", false);
            GetComponent<Animator>().Play("IdleR");
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
            GetComponent<Animator>().SetBool("IsRun", false);
            GetComponent<Animator>().Play("IdleL");
        }
    }
        
}


