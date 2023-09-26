using Unity.VisualScripting;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    [SerializeField] Transform _Cam;
    [SerializeField] float rotationSpeed;
    Transform _player;
    Transform _playerObj;
    Transform _orientation;
    float _rotY, _rotX;
    float x, y;
    public bool wallRun = false, wallRight = false, wallLeft = false;
    public bool _wallRun {get{return wallRun;} set{wallRun = value;}}
    public bool _wallRight{get{return wallRight;} set{wallRight = value;}}
    public bool _wallLeft{get{return wallLeft;} set{wallLeft = value;}}
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _player = GameObject.Find("Player").transform;
        _playerObj = GameObject.Find("PlayerObj").transform;
        _orientation = GameObject.Find("orientation").transform;
    }
    private void Update() 
    {
        
        transform.position = new Vector3(_player.position.x, _player.position.y + 1, _player.position.z);
        //바라볼 방향 계산
        Vector3 viewDir = _player.position - new Vector3(_Cam.transform.position.x, _player.position.y, _Cam.transform.position.z);
        _orientation.forward = viewDir.normalized;

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        CheckWallRun();
        

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
    void CheckWallRun(){
        Vector3 inputDir;
        if(_wallRun ==  true){
            inputDir = _orientation.right * x;
            
            if(_wallRight == true){
                //inputDir = _orientation.forward;
                _playerObj.localRotation = Quaternion.Euler(_orientation.eulerAngles.x, _orientation.eulerAngles.y,30);
            }
            if(_wallLeft == true){
                
                _playerObj.localRotation = Quaternion.Euler(_orientation.eulerAngles.x, _orientation.eulerAngles.y,-30);
            }   
        }else{
            inputDir = _orientation.forward * y + _orientation.right * x;
        }

        //바라보는 방향 = 캐릭터 Z방향
        if (inputDir != Vector3.zero) 
        {
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    


}
