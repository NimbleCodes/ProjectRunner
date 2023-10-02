using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    //Wall Running
    [SerializeField] LayerMask whatIsWall;
    [SerializeField] LayerMask whatIsGround;
    
    
    
    //Input Var
    float x,y;

    //Detection
    [SerializeField] float wallCheckDistance;
    [SerializeField] float minJumpHeight;
    RaycastHit leftWallhit, rightWallHit;
    public bool wallLeft = false, wallRight = false;
    public bool wallChecking = true;
    
    //Reference
    [SerializeField] Transform _player;
    [SerializeField] Transform _orientation;
    [SerializeField] Animator _ani;
    GameObject cam; 
    Rigidbody rb;
    MoveNRotate mn;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mn = GetComponent<MoveNRotate>();
        cam = GameObject.Find("CamPoint");
    }
    void Update()
    {
        
        CheckWall();
        StateMachine();
        
    }

    void CheckWall(){
        
        Ray rayRight = new Ray(transform.position, _player.right);
        Ray rayLeft = new Ray(transform.position, -_player.right); 

        wallRight = Physics.Raycast(rayRight, out rightWallHit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(rayLeft, out leftWallhit, wallCheckDistance, whatIsWall);
        
    }

    bool AboveGround(){
        return !Physics.Raycast(transform.position,Vector3.down,minJumpHeight, whatIsGround);
    }

    void StateMachine(){
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if((wallLeft | wallRight) && AboveGround()){
            if(wallRight == true){
                mn.rightWall = true;
                _player.localRotation = Quaternion.Euler(_orientation.eulerAngles.x, _orientation.eulerAngles.y,30);
                
                StartWallRun();
            }else if(wallLeft == true){
                mn.leftWall = true;
                _player.localRotation = Quaternion.Euler(_orientation.eulerAngles.x, _orientation.eulerAngles.y,-30);
                
                StartWallRun();
            }
        }

    }

    void StartWallRun(){
        mn.wallRunning = true;
        wallChecking = false;
        cam.GetComponent<FreeCam>()._wallRun = true;
        _ani.SetBool("OnWall",true);
    }
    
    public void StopWallRun(){
        mn.wallRunning = false;
        mn.rightWall = false;
        mn.leftWall  =false;
        cam.GetComponent<FreeCam>()._wallRun = false;
        cam.GetComponent<FreeCam>()._wallRight = false;
        cam.GetComponent<FreeCam>()._wallLeft = false;
        rb.useGravity = true;
        _ani.SetBool("OnWall",false);
    }
}
