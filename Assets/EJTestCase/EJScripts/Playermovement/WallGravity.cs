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
    [SerializeField] Animator animator; 


    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ms = GetComponent<MoveNRotate>();
    }
    private void Update()
    {
        CheckForWall();
        WallRunInput(); 
    }

    private void FixedUpdate()
    {
        if (ms.wallRunning == true)
        {
            StartWallRun();
        }
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        // ��ŸƮ ����Ʈ, ����, hit ����, �Ÿ� 
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
        if (!wallLeft && !wallRight) StopWallRun();
    }

    private void WallRunInput() //make sure to call in void Update
    {
        //Wallrun
        if (wallRight && AboveGround())
        {
            StartWallRun(); 
        }
        if (wallLeft && AboveGround())
        {
            StartWallRun(); 
        }
    }

    private bool AboveGround() // �÷��̾ �� �޸��⸦ �� �� �ְԲ� ����� ���̿� �� �ִ��� Ȯ�� 
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }


    private void StartWallRun()
    {
        rig.useGravity = false; 
        ms.wallRunning = true;
        

        if(rig.velocity.magnitude <= ms.moveSpeed && AboveGround())
        {
            rig.AddForce(orientation.forward * wallRunForce * Time.deltaTime);

            if (wallRight)
            {
                rig.AddForce(orientation.forward * wallRunForce / 5 * Time.deltaTime);
                animator.GetComponent<Transform>().localRotation = Quaternion.Euler(0,0,30);
                animator.SetBool("OnWall", true); 
            }
            else if (wallLeft)
            {
                rig.AddForce(-orientation.forward * wallRunForce / 5 * Time.deltaTime);
                animator.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, -30);
                animator.SetBool("OnWall", true);
            }
        }
    }

    private void StopWallRun()
    {
        ms.wallRunning = false;
        rig.useGravity = true;
        animator.SetBool("OnWall", false);
    }

}
