using UnityEngine;

public class WallGravity : MonoBehaviour
{
    [SerializeField] LayerMask whatIsWall; // �� ���̾� 
    [SerializeField] LayerMask whatIsGround; // �� ���̾� 
    public float wallRunForce; // ���� ���� float

    private float horizontalInput; // ���� ��
    private float verticalInput; // ���� �� 
    private float x, y; 

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
    [SerializeField] GameObject _playerObj;

    Vector3 moveDirection; 

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        ms = GetComponent<MoveNRotate>();
    }
    private void Update()
    {
        CheckForWall();
        WallRunInput(); 
        // if (!wallLeft && !wallRight)
        // {
        //     rig.constraints &= ~RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        // }
    }

    private void CheckForWall()
    {
        Vector3 rayOrigin = new Vector3(orientation.position.x, orientation.position.y +1, orientation.position.z);
        wallRight = Physics.Raycast(rayOrigin, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        // ��ŸƮ ����Ʈ, ����, hit ����, �Ÿ� 
        wallLeft = Physics.Raycast(rayOrigin, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
        if (!wallLeft && !wallRight)
        {
            Debug.Log("hop");
            StopWallRun();
        }
        Debug.DrawRay(rayOrigin, orientation.right * 1, Color.red); 
        Debug.DrawRay(rayOrigin, -orientation.right * 1, Color.red);
        
        
    }

    private void WallRunInput() //make sure to call in void Update
    {
        verticalInput = Input.GetAxisRaw("Vertical"); 
        //Wallrun
        if (wallRight && AboveGround() && verticalInput > 0)
        {
            animator.GetComponent<Transform>().localRotation = Quaternion.Euler(0, orientation.rotation.y, 30);
            StartWallRun();
        }
        else if (wallLeft && AboveGround() && verticalInput > 0)
        {
            animator.GetComponent<Transform>().localRotation = Quaternion.Euler(0, orientation.rotation.y, -30);
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
        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if (rig.velocity.magnitude <= ms.moveSpeed && AboveGround())
        {
            rig.AddForce(wallForward* wallRunForce*Time.deltaTime);

             _playerObj.GetComponent<Animator>().SetFloat("X", x);
             _playerObj.GetComponent<Animator>().SetFloat("Y", y);

        }
    }

    void FreezeRo()
    {
        rig.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    private void StopWallRun()
    {
        wallLeft = false;
        wallRight = false;
        ms.wallRunning = false;
        rig.useGravity = true;
        animator.SetBool("OnWall", false);
    }

}
