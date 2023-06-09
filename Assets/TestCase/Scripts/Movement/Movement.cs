using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    Transform _playerdir;
    Rigidbody _rig;
    [SerializeField] private LayerMask Ground;
    Vector3 _direction;
    bool _isOnGround;
    float _playerHeight = 2f, _drag = 8f;
    float x,z; //앞뒤 양옆 속도 계산을 위한 변수
    void Start()
    {
        _playerdir = this.GetComponent<Transform>();
        _rig = this.GetComponent<Rigidbody>();
        _rig.freezeRotation = true;
    }

    void FixedUpdate(){
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        _direction = _playerdir.forward * z + _playerdir.right * x;
        _rig.AddForce(_direction.normalized * _speed * 10f, ForceMode.Force);
    }

    private void Update()
    {
        _isOnGround = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.3f, Ground);
        if (_isOnGround)
        {
            _rig.drag = _drag;
        }
        else
        {
            _rig.drag = 0f;
        }
        LimitSpeed();
    }

    void LimitSpeed()
    {
        Vector3 flatValue = new Vector3(_rig.velocity.x, 0f, _rig.velocity.z);
        if (flatValue.magnitude > _speed)
        {                                               //속도가 speed를 넘어가면
            Vector3 speedLimit = flatValue.normalized * _speed;                         //speedLimit 값을 계산하여
            _rig.velocity = new Vector3(speedLimit.x, _rig.velocity.y, speedLimit.z);   //현재 속도 변경
        }
    }
}
