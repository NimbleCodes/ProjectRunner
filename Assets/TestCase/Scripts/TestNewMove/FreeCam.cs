using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _playerObj;
    [SerializeField] Transform _orientation;
    [SerializeField] Transform _Cam;
    [SerializeField] Rigidbody rb;

    [SerializeField] float rotationSpeed;
    float _rotY,_rotX;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update() {
        transform.position = new Vector3(_player.position.x, _player.position.y + 1, _player.position.z);
        //바라볼 방향 계산

        Vector3 viewDir = _player.position - new Vector3(_Cam.transform.position.x, _player.position.y, _Cam.transform.position.z);
        _orientation.forward = viewDir.normalized;

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = _orientation.forward * y + _orientation.right * x;
        //바라보는 방향 = 캐릭터 Z방향
        if(inputDir != Vector3.zero){
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }

        CamRotation();
    }

    void CamRotation()
    {
        float x = Input.GetAxisRaw("Mouse X");
        float y = Input.GetAxisRaw("Mouse Y");

        _rotY += x;

        _rotX -= y;
        _rotX = Mathf.Clamp(_rotX, -90f, 90f); //y회전 90도 리미트

        //카메라 로테이션
        transform.rotation = Quaternion.Euler(_rotX, _rotY, 0);
    }

}
