using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField] Transform _player;

    float x, y;
    float inputX, inputY;
    float _rotY, _rotX;
    float _senseY, _senseX;
    [SerializeField] float rotationSpeed;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        x = Input.GetAxisRaw("Mouse X") + Time.deltaTime * _senseX;
        y = Input.GetAxisRaw("Mouse Y") + Time.deltaTime * _senseY;
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        _rotY += x;
        _rotX -= y;
        _rotX = Mathf.Clamp(_rotX, -30f, 30f); //fix Y Pos to 90 degrees // 
        if(Input.GetKey(KeyCode.Tab)){
            transform.rotation = Quaternion.Euler(_rotX,_rotY,0);
        }else{
            // rotation of cam and player
            // Euler : returns rotation
            transform.rotation = Quaternion.Euler(_rotX, _rotY, 0);
            //_player.rotation = Quaternion.Euler(0, _rotY, 0);
            if(inputY != 0 || inputX != 0){
                Vector3 DirChange = Vector3.forward * inputY + Vector3.right * inputX;
                _player.forward = DirChange * rotationSpeed;
            }
        }
        
    }

}
