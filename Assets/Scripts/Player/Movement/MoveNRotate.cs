using UnityEngine;

public class MoveNRotate : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] int groundDrag;
    [SerializeField] float wallRunForce;
    [SerializeField] AudioClip _attack; 

    GameObject _playerObj;
    Transform _orientation;
    AudioSource _audio;
    float x,y;
    int _jumpCount = 0;
    Vector3 moveDirection;
    Rigidbody rb;

    public bool _isOnGround = true;
    public bool wallRunning;
    public bool rightWall{get;set;}
    public bool leftWall{get;set;}
    public MovementState state;

    public enum MovementState
    {
        groundrunning,
        wallrunning, 
        jumping, 
        Attack, 
    }
    
    private void Start() 
    {
        _audio = GetComponent<AudioSource>();
        _playerObj = GameObject.FindGameObjectWithTag("Player"); 
        _orientation = GameObject.FindGameObjectWithTag("orientation").transform;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        state = MovementState.groundrunning; 
    }

    //State로 Movment Controll
    private void FixedUpdate() {
        if(state == MovementState.groundrunning || state == MovementState.jumping){
            MovePlayer();
        }else if(state == MovementState.wallrunning){
            WallRunningMovement();
        }
        
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
            rb.drag = 1f;
        }
        StateHandler();
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
        //moveDirection = new Vector3(0f ,x , y).normalized;
        rb.AddForce(moveDirection.normalized * moveSpeed * 3f, ForceMode.Force);
        
    }

    void PlayerAnimation()
    {
        if(Input.GetKeyDown(KeyCode.Space) && (_jumpCount < 1 || _isOnGround == true))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
            rb.AddForce(transform.up * 25, ForceMode.Impulse);
            _isOnGround = false;
            _playerObj.GetComponent<Animator>().SetBool("OnTheGround", false);
            _jumpCount++;
            state = MovementState.jumping;
        }

        

        if(Input.GetKeyDown(KeyCode.Space) && wallRunning){
            if(rightWall){
                rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                rb.AddForce(-transform.right * 15 + transform.up * 7, ForceMode.Impulse);
            }else if(leftWall){
                rb.AddForce(transform.right * 15, ForceMode.Impulse);
                rb.AddForce(transform.up * 7, ForceMode.Impulse);
            }
            
            _isOnGround = false;
            state = MovementState.jumping;
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _audio.clip = _attack; 
            _audio.Play();
            _playerObj.GetComponent<Animator>().Play("AttackHorizontal");
            _playerObj.GetComponent<Animator>().SetLayerWeight(2, 0.3f); 
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

    void WallRunningMovement(){
        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0f ,rb.velocity.z);
        rb.AddForce(_playerObj.transform.forward * wallRunForce, ForceMode.Force);
    }

    void OnCollisionEnter(Collision other){
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
       
    void StateHandler()
    {
        if (wallRunning == true){
            state = MovementState.wallrunning;
        }
        else if((wallRunning == false) && (_isOnGround == false)){
            state = MovementState.jumping;
        }
        else{
            state = MovementState.groundrunning;
        }
    }
}
