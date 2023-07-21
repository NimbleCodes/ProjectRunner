using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    [SerializeField] private float _speed;
    Transform _transform; 
    Rigidbody _rig;
    float x,z; //�յ� �翷 �ӵ� ����� ���� ����
    void Start()
    {
        _transform = this.GetComponent<Transform>();
        _rig = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate(){
        x = Input.GetAxisRaw("Horizontal") * _speed;
        z = Input.GetAxisRaw("Vertical") * _speed;
        _rig.velocity = new Vector3(x, _rig.velocity.y,z);
    }
}
