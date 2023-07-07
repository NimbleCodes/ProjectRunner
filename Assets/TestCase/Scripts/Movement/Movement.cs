using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed; 
    Rigidbody _rig;
    void Start()
    {
        _rig = this.GetComponent<Rigidbody>();
        _rig.freezeRotation = true;
    }


    void FixedUpdate()
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
    }

}
