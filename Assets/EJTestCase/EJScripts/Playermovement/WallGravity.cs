using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGravity : MonoBehaviour
{
    public LayerMask whatIsWall; // 벽 레이어 
    public LayerMask whatIsGround; // 땅 레이어 
    public float wallRunForce; // 벽을 위한 float
    public float wallClimbSpeed; // 벽 타는 speed
    public float maxWallRunTime; // 벽 타는 최대 실행 시간 

    private bool upwardsRunning;
    private bool downwardsRunning;
    private float horizontalInput; // 수평 축
    private float verticalInput; // 수직 축 

    public float wallCheckDistance; // 벽과의 거리 체크 float
    public float minJumpHeight; // 최소 점프 높이 float
    private RaycastHit leftWallhit; // 왼쪽 벽 확인 레이 
    private RaycastHit rightWallhit; // 오른쪽 벽 확인 레이 
    private bool wallLeft; // 왼쪽 벽을 타고 있나 안타고 있나 bool
    private bool wallRight; // 오른쪽 벽 bool 

    private Rigidbody rig;
    public Transform orientation;
    private MoveNRotate ms; 


    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ms = GetComponent<MoveNRotate>();

    }
    private void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if (ms.wallRunning == true)
        {
            WallRunningMovement();
        }
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        // 스타트 포인트, 방향, hit 인포, 거리 
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround() // 플레이어가 벽 달리기를 할 수 있게끔 충분한 높이에 떠 있는지 확인 
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        verticalInput = Input.GetAxisRaw("Vertical");


        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround()) // 벽에 붙어있는가? 벽 달리기를 할 만큼의 높이에 있는가? // 수직 축이 0보다 큰가?
        {
            if (!ms.wallRunning)
            {
                StartWallRun(); // 달리기 코드 시작 
            }
        }

        // State 3 - None
        else
        {
            if (ms.wallRunning)
            {
                StopWallRun();
            }
        }
    }

    private void StartWallRun()
    {
        ms.wallRunning = true;
    }

    private void WallRunningMovement()
    {
        rig.useGravity = false;
        rig.velocity = new Vector3(rig.velocity.x, 0f, rig.velocity.z);

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        // forward force
        rig.AddForce(wallForward * wallRunForce, ForceMode.Force);

        // upwards/downwards force
        if (upwardsRunning)
            rig.velocity = new Vector3(rig.velocity.x, wallClimbSpeed, rig.velocity.z);
        if (downwardsRunning)
            rig.velocity = new Vector3(rig.velocity.x, -wallClimbSpeed, rig.velocity.z);

        // push to wall force
        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
            rig.AddForce(-wallNormal * 100, ForceMode.Force);
    }

    private void StopWallRun()
    {
        ms.wallRunning = false;
    }

}
