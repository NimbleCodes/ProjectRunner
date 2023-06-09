using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody _rig;
    [SerializeField] private Transform _CamDir;
    [SerializeField] private float _rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        Vector3 viewDir = _CamDir.position - new Vector3(transform.position.x, _CamDir.position.y, transform.position.z);
        //_player.forward = viewDir.normalized;
        _player.forward = Vector3.Slerp(_player.forward, viewDir.normalized, Time.deltaTime * _rotationSpeed);

        
    }

}
