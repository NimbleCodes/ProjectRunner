using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonSimple : MonoBehaviour
{
    [SerializeField] Transform _player;
    float x,y;
    float _rotY,_rotX;
    float _senseY,_senseX;
void Start()
{
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}
    void Update()
    {
        x = Input.GetAxisRaw("Mouse X") + Time.deltaTime * _senseX;
        y = Input.GetAxisRaw("Mouse Y") + Time.deltaTime * _senseY;

        _rotY += x;

        _rotX -= y;
        _rotX = Mathf.Clamp(_rotX, -90f, 90f); //fix Y Pos to 90 degrees 

        // rotation of cam and player
        //Euler : returns rotation
        transform.rotation = Quaternion.Euler(_rotX, _rotY, 0);
        _player.rotation = Quaternion.Euler(0, _rotY, 0);
    }
}
