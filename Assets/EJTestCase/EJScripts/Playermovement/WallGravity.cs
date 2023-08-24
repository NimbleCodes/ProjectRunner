using UnityEngine;

public class WallGravity : MonoBehaviour
{
    public LayerMask whatIsWall; // �� ���̾� 
    public LayerMask whatIsGround; // �� ���̾� 
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
        if (!wallLeft && !wallRight)
        {
            rig.constraints &= ~RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }

        Debug.DrawRay(orientation.position, orientation.right * 15, Color.red); 
        Debug.DrawRay(orientation.position, -orientation.right * 15, Color.red);
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(orientation.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        // ��ŸƮ ����Ʈ, ����, hit ����, �Ÿ� 
        wallLeft = Physics.Raycast(orientation.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
        if (!wallLeft && !wallRight)
        {
            StopWallRun();
        }
    }

    private void WallRunInput() //make sure to call in void Update
    {
        verticalInput = Input.GetAxisRaw("Vertical"); 
        //Wallrun
        if (wallRight && AboveGround() && verticalInput > 0)
        {
            animator.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, 30);
            StartWallRun();
        }
        else if (wallLeft && AboveGround() && verticalInput > 0)
        {
            animator.GetComponent<Transform>().localRotation = Quaternion.Euler(0, 0, -30);
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

        if (rig.velocity.magnitude <= ms.moveSpeed && AboveGround())
        {
            rig.AddForce(orientation.forward * wallRunForce * Time.deltaTime);

            _playerObj.GetComponent<Animator>().SetFloat("X", x);
            _playerObj.GetComponent<Animator>().SetFloat("Y", y);


            if (wallRight)
            {
                FreezeRo(); 
                animator.SetBool("OnWall", true);
                rig.AddForce(moveDirection.normalized * wallRunForce / 5 * Time.deltaTime);
            }
            if (wallLeft)
            {
                FreezeRo();
                animator.SetBool("OnWall", true);
                rig.AddForce(-moveDirection.normalized * wallRunForce / 5 * Time.deltaTime);
            }
        }
    }

    void FreezeRo()
    {
        rig.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    private void StopWallRun()
    {
        ms.wallRunning = false;
        rig.useGravity = true;
        animator.SetBool("OnWall", false);
    }

}
