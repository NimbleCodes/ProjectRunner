using UnityEngine;

public class WallRunning : MonoBehaviour
{
   // WallRunning Objects
   [SerializeField] LayerMask wallLayer;
   [SerializeField] LayerMask groundLayer;
   [SerializeField] float wallRunForce;
   
   // WallJump
   [SerializeField] float wallJumpUpForce;
   [SerializeField] float wallJumpSideForce;
   // Input
   float x,y;

   //Detection
   [SerializeField] float wallCheckDistance;
   [SerializeField] float minJumpHeight;
   RaycastHit leftWallHit, rightWallHit;
   public bool wallLeft, wallRight;

   // Reference
   Transform orientation;
   Transform playerObj;
   Animator _ani;
   MoveNRotate mn;
   Rigidbody rb;

   private void Start() 
   {
    mn = GetComponent<MoveNRotate>();
    rb = GetComponent<Rigidbody>();
    playerObj = GameObject.FindGameObjectWithTag("Player").transform; 
    _ani = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    orientation = GameObject.FindGameObjectWithTag("orientation").transform;
    } 
    private void Update() {
        CheckWall();
        StateMachine();

        if(mn.wallRunning == true) WallRunningMovement();
    }

   void CheckWall(){
        
        Ray rayRight = new Ray(transform.position, playerObj.right);
        Ray rayLeft = new Ray(transform.position, -playerObj.right); 

        wallRight = Physics.Raycast(rayRight, out rightWallHit, wallCheckDistance, wallLayer);
        wallLeft = Physics.Raycast(rayLeft, out leftWallHit, wallCheckDistance, wallLayer);
        
    }

    bool AboveGround(){
        return !Physics.Raycast(transform.position,Vector3.down,minJumpHeight, groundLayer);
    }

    void StateMachine(){
        if((wallLeft || wallRight) && AboveGround()){
            if( wallRight == true){   
                playerObj.localRotation = Quaternion.Euler(orientation.eulerAngles.x, orientation.eulerAngles.y,30);
                StartWallRun();

                if(Input.GetKeyDown(KeyCode.Space)) wallJump();
            }else if(wallLeft == true){
                playerObj.localRotation = Quaternion.Euler(orientation.eulerAngles.x, orientation.eulerAngles.y,-30);
                StartWallRun();
                if(Input.GetKeyDown(KeyCode.Space)) wallJump();
            }
        }else{
            StopWallRun();
        }
    }

    void StartWallRun(){
        mn.wallRunning = true;
        _ani.SetBool("OnWall",true);
    }

    void StopWallRun(){
        rb.useGravity = true;
        _ani.SetBool("OnWall",false);
        mn.wallRunning =false;
    }

    void WallRunningMovement(){
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);
        if((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude){
            wallForward = -wallForward;
        }

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
    }

    void wallJump(){
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
    }
}
