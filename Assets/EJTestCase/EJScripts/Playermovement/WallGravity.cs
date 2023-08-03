using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGravity : MonoBehaviour
{
    public LayerMask whatIsWall; // �� ���̾� 
    public LayerMask whatIsGround; // �� ���̾� 
    public float wallRunForce; // ���� ���� float
    public float wallClimbSpeed; // �� Ÿ�� speed
    public float maxWallRunTime; // �� Ÿ�� �ִ� ���� �ð� 

    private bool upwardsRunning;
    private bool downwardsRunning;
    private float horizontalInput; // ���� ��
    private float verticalInput; // ���� �� 

    public float wallCheckDistance; // ������ �Ÿ� üũ float
    public float minJumpHeight; // �ּ� ���� ���� float
    private RaycastHit leftWallhit; // ���� �� Ȯ�� ���� 
    private RaycastHit rightWallhit; // ������ �� Ȯ�� ���� 
    private bool wallLeft; // ���� ���� Ÿ�� �ֳ� ��Ÿ�� �ֳ� bool
    private bool wallRight; // ������ �� bool 

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
        // ��ŸƮ ����Ʈ, ����, hit ����, �Ÿ� 
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround() // �÷��̾ �� �޸��⸦ �� �� �ְԲ� ����� ���̿� �� �ִ��� Ȯ�� 
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); 
        verticalInput = Input.GetAxisRaw("Vertical");


        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround()) // ���� �پ��ִ°�? �� �޸��⸦ �� ��ŭ�� ���̿� �ִ°�? // ���� ���� 0���� ū��?
        {
            if (!ms.wallRunning)
            {
                StartWallRun(); // �޸��� �ڵ� ���� 
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
