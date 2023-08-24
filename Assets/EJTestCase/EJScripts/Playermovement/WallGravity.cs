using UnityEngine;

public class WallGravity : MonoBehaviour
{
    public LayerMask whatIsWall; // 벽 레이어 
    public LayerMask whatIsGround; // 땅 레이어 
    public float wallRunForce; // 벽을 위한 float

    private float horizontalInput; // 수평 축
    private float verticalInput; // 수직 축 
    private float x, y; 

    public float wallCheckDistance; // 벽과의 거리 체크 float
    public float minJumpHeight; // 최소 점프 높이 float
    private RaycastHit leftWallhit; // 왼쪽 벽 확인 레이 
    private RaycastHit rightWallhit; // 오른쪽 벽 확인 레이 
    private bool wallLeft; // 왼쪽 벽을 타고 있나 안타고 있나 bool
    private bool wallRight; // 오른쪽 벽 bool 

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
        // 스타트 포인트, 방향, hit 인포, 거리 
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

    private bool AboveGround() // 플레이어가 벽 달리기를 할 수 있게끔 충분한 높이에 떠 있는지 확인 
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
