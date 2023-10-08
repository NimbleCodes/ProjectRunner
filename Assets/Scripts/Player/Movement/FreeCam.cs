using Cinemachine;
using UnityEngine;

public class FreeCam : MonoBehaviour
{
    [SerializeField] Transform _Cam;
    [SerializeField] float rotationSpeed;
    [SerializeField] CinemachineFreeLook _camsense;
    Transform _player;
    Transform _playerObj;
    Transform _orientation;
    MoveNRotate mn;
    float _rotY, _rotX;
    float x, y;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _player = GameObject.Find("Player").transform;
        _playerObj = GameObject.Find("PlayerObj").transform;
        _orientation = GameObject.Find("orientation").transform;
        mn = _player.GetComponent<MoveNRotate>();
    }
    private void Update() 
    {
        CamRotation();
        Vector3 inputDir;
        //transform.position = new Vector3(_player.position.x, _player.position.y + 1, _player.position.z);
        //바라볼 방향 계산
        Vector3 viewDir = _player.position - new Vector3(_Cam.transform.position.x, _player.position.y, _Cam.transform.position.z);
        _orientation.forward = viewDir.normalized;

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        inputDir = _orientation.forward * y + _orientation.right * x;
        if(mn.state == MoveNRotate.MovementState.groundrunning){
            inputDir = _orientation.forward * y + _orientation.right * x;
            
            //바라보는 방향 = 캐릭터 Z방향
            if (inputDir != Vector3.zero) 
            {
                _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            }
        }else if(mn.state == MoveNRotate.MovementState.wallrunning){
            inputDir = _orientation.right * x;
        }        

        
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

    public void ChangeCamSpeed(float value){
        _camsense.m_YAxis.m_MaxSpeed = 4 *value;
        _camsense.m_XAxis.m_MaxSpeed = 400 * value;
    }
    
}
