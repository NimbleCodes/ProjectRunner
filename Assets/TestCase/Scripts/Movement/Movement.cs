using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    Transform _transform; 
    Rigidbody _rig;
    float x,z; //앞뒤 양옆 속도 계산을 위한 변수
    void Start()
    {
        _transform = this.GetComponent<Transform>();
        _rig = this.GetComponent<Rigidbody>();
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
