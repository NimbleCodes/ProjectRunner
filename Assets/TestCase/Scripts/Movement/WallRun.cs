using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    //Wall Running
    [SerializeField] LayerMask whatIsWall;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] float wallRunForce;
    
    
    //Input Var
    float x,y;

    //Detection
    [SerializeField] float wallCheckDistance;
    [SerializeField] float minJumpHeight;
    RaycastHit leftWallhit, rightWallHit;
    public bool wallLeft = false, wallRight = false;
    public bool wallRunning = false;
    
    //Reference
    [SerializeField] Transform _player;
    [SerializeField] Animator _ani; 
    Rigidbody rb;
    MoveNRotate mn;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mn = GetComponent<MoveNRotate>();
    }
    void Update()
    {
        CheckWall();
        StateMachine();
    }
    void FixedUpdate()
    {
        if(wallRunning) WallRunningMovement();
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

        if((wallLeft | wallRight) && y > 0 && AboveGround()){
            if(wallRight){
                _player.localRotation = Quaternion.Euler(0,_player.eulerAngles.y,30);
                _ani.SetBool("Isright", true);
                StartWallRun();
            }else if(wallLeft){
                _player.localRotation = Quaternion.Euler(0,_player.eulerAngles.y,-30);
                _ani.SetBool("Isleft", true);
                StartWallRun();
            }
        }else{
            StopWallRun();
        }

    }

    void StartWallRun(){
        wallRunning = true;
        mn.wallRunning = true;
        _ani.SetBool("OnWall",true);
    }

    void WallRunningMovement(){
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(_player.forward * wallRunForce, ForceMode.Force);
    }

    void StopWallRun(){
        wallRunning = false;
        mn.wallRunning = false;
        rb.useGravity = true;
        _ani.SetBool("OnWall",false);
        _ani.SetBool("Isleft", false);
        _ani.SetBool("Isright", false);
    }
}
