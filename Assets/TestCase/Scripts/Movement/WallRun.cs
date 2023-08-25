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
    bool wallLeft = false, wallRight = false;
    bool wallRunning = false;
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
    }
    void FixedUpdate()
    {
        if(wallRunning) WallRunningMovement();
        
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

        if((wallLeft | wallRight) && y > 0 && AboveGround()){
            if(!wallRunning) StartWallRun();
        }else{
            if(wallRunning) StopWallRun();
        }

    }

    void StartWallRun(){
        wallRunning = true;
        mn.wallRunning = true;
    }

    void WallRunningMovement(){
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal,-_player.up);
        Debug.DrawRay(transform.position, wallForward * 1, Color.red); 

        rb.AddForce(_player.forward * wallRunForce, ForceMode.Force);
        _ani.SetFloat("X", x);
        _ani.SetFloat("Y", y);
        if(wallRight)_player.localRotation = Quaternion.Euler(0, _player.localEulerAngles.y, 30);
        //if(wallLeft)_player.localRotation = Quaternion.Euler(0, 0, 30);
        
    }

    void StopWallRun(){
        wallRunning = false;
        mn.wallRunning = false;
        rb.useGravity = true;
    }
}
