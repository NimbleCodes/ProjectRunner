using UnityEngine;

public class MoveNRotate : MonoBehaviour
{
    [SerializeField] GameObject _playerObj;
    [SerializeField] public float moveSpeed;
    [SerializeField] Transform _orientation;
    [SerializeField] int groundDrag;
    float x,y;
    int _jumpCount = 0;
    Vector3 moveDirection;
    Rigidbody rb;
    public bool _isOnGround = true;
    public bool wallRunning;
    public bool rightWall{get;set;}
    public bool leftWall{get;set;}
    Animation anim;
    MovementState state;

    public enum MovementState
    {
        groundrunning,
        wallrunning, 
        jumping, 
        Attack, 
    }
    
    private void Start() 
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = _playerObj.GetComponent<Animation>();
        state = MovementState.groundrunning; 
    }

    private void FixedUpdate() {
        MovePlayer();
        LimitSpeed();
    }

    private void Update() 
    {
        if(_isOnGround)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
        PlayerAnimation();
        Attack();
    }

    void MovePlayer(){
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        
        if(state == MovementState.groundrunning){
            _playerObj.GetComponent<Animator>().SetFloat("X",x);
            _playerObj.GetComponent<Animator>().SetFloat("Y",y);
        }
        moveDirection = _orientation.forward * y + _orientation.right * x;
        rb.AddForce(moveDirection.normalized * moveSpeed * 3f, ForceMode.Force);
        
    }

    void PlayerAnimation()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (_jumpCount < 1 || _isOnGround == true))
        {
            rb.AddForce(transform.up * 7, ForceMode.Impulse);
            _isOnGround = false;
            _playerObj.GetComponent<Animator>().SetBool("OnTheGround", false);
            _jumpCount++;
            state = MovementState.jumping; 
        }

        

        if(Input.GetKeyDown(KeyCode.Space) && wallRunning){
            if(rightWall){
                rb.AddForce(-transform.right * 3, ForceMode.Impulse);
                rb.AddForce(transform.up * 4, ForceMode.Impulse);
            }else if(leftWall){
                rb.AddForce(transform.right * 3, ForceMode.Impulse);
                rb.AddForce(transform.up * 4, ForceMode.Impulse);
            }
            
            _isOnGround = false;
            state = MovementState.jumping;
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _playerObj.GetComponent<Animator>().Play("AttackHorizontal");
            _playerObj.GetComponent<Animator>().SetLayerWeight(1, 0); 
        }
        if (Input.GetMouseButtonUp(0))
        {
            _playerObj.GetComponent<Animator>().SetBool("OnAttack", false);
        }
    }

    void LimitSpeed(){
        Vector3 flatVal = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit Speed to it's max value
        if(flatVal.magnitude > moveSpeed){
            Vector3 limitedVal = flatVal.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVal.x, rb.velocity.y, limitedVal.z);
        }
    }

    private void OnCollisionEnter(Collision other){
        if(other.collider.CompareTag("Ground")){
            _playerObj.GetComponent<Animator>().SetBool("OnTheGround", true);
            _isOnGround = true;
            _jumpCount = 0;
            state = MovementState.groundrunning; 
        }
        if (other.collider.CompareTag("Wall"))
        {
            state = MovementState.wallrunning; 
        }
       
    }

    private void StateHandler()
    {
        if (wallRunning == true)
        {
            state = MovementState.wallrunning;
        }
        else if((wallRunning == false) && (_isOnGround == false))
        {
            state = MovementState.jumping; 
        }
        else
        {
            state = MovementState.groundrunning; 
        }
    }
}
