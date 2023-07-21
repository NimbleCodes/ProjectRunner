using System.Collections;
using System.Collections.Generic;
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

    void FixedUpdate(){
        x = Input.GetAxisRaw("Horizontal") * _speed;
        z = Input.GetAxisRaw("Vertical") * _speed;
        _rig.velocity = new Vector3(x, _rig.velocity.z);
    }

}
