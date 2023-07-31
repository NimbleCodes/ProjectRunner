using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNRotate : MonoBehaviour
{
    [SerializeField] GameObject _playerObj;
    [SerializeField] float moveSpeed;
    [SerializeField] Transform _orientation;
    [SerializeField] int groundDrag;
    float x,y;
    Vector3 moveDirection;
    Rigidbody rb;
    public bool _isOnGround = true;
    Animation anim;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = _playerObj.GetComponent<Animation>();
    }

    private void FixedUpdate() {
        MovePlayer();
        LimitSpeed();
    }

    private void Update() {
        if(_isOnGround){
            rb.drag = groundDrag;
        }else{
            rb.drag = 0;
        }
        PlayerAnimation();
    }

    void MovePlayer(){
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        
        moveDirection = _orientation.forward * y + _orientation.right * x;
        rb.AddForce(moveDirection.normalized * moveSpeed * 3f, ForceMode.Force);
        _playerObj.GetComponent<Animator>().SetFloat("X",x);
        _playerObj.GetComponent<Animator>().SetFloat("Y",y);

        
    }

    void PlayerAnimation(){
        if(Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(transform.up * 10, ForceMode.Impulse);
            _isOnGround = false;
            _playerObj.GetComponent<Animator>().SetBool("OnTheGround", false);
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
        }
    }
    
}
