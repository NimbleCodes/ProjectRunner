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

        
    }

    private void CheckForWall()
    {
        Vector3 rayOrigin = new Vector3(orientation.position.x, orientation.position.y +1, orientation.position.z);
        wallRight = Physics.Raycast(rayOrigin, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        // ��ŸƮ ����Ʈ, ����, hit ����, �Ÿ� 
        wallLeft = Physics.Raycast(rayOrigin, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
        if (!wallLeft && !wallRight)
        {
            //Debug.Log("hop");
            StopWallRun();
        }
        Debug.DrawRay(rayOrigin, orientation.right * 1, Color.red); 
        Debug.DrawRay(rayOrigin, -orientation.right * 1, Color.red);
        
        
    }

    private void WallRunInput() //make sure to call in void Update
    {
        if (wallRight && AboveGround())
        {
            animator.GetComponent<Transform>().localRotation = Quaternion.Euler(0, orientation.rotation.y, 30);
            StartWallRun();
        }
        else if (wallLeft && AboveGround())
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
        verticalInput = Input.GetAxisRaw("Vertical");
        rig.useGravity = false; 
        ms.wallRunning = true;

        if (rig.velocity.magnitude <= ms.moveSpeed && AboveGround())
        {
            rig.AddForce(orientation.forward * wallRunForce * Time.deltaTime);

            _playerObj.GetComponent<Animator>().SetFloat("X", x);
            _playerObj.GetComponent<Animator>().SetFloat("Y", y);


            if (wallRight && verticalInput > 0)
            {
                FreezeRo(); 
                animator.SetBool("OnWall", true);
                rig.AddForce(orientation.forward.normalized * wallRunForce / 5 * Time.deltaTime);
            }
            if (wallLeft && verticalInput > 0)
            {
                FreezeRo();
                animator.SetBool("OnWall", true);
                rig.AddForce(orientation.forward.normalized * wallRunForce / 5 * Time.deltaTime);
            }
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
