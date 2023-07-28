using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed; 
    Rigidbody _rig;
    public bool _isRotated = false;
    public bool _isGround = false;
    int _jumpCount = 1;
    float _speedLimit = 10; 


    void Start()
    {
        _rig = this.GetComponent<Rigidbody>();
        _rig.freezeRotation = true;
    }


    void Update()
    {
        PlayerMovement();
        CheckSpeedLimit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Jumping"))
        {
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }
    }

    void MoveLocalTransform()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 vPos = transform.position;
        vPos += transform.right * x * Time.deltaTime * _speed;
        vPos += transform.forward * y * Time.deltaTime * _speed;
        transform.position = vPos;
    }

    void PlayerMovement(){
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 velocity = transform.forward * y + transform.right * x;
        

        Debug.Log(y);
        if(y > 0){
            _rig.AddForce(velocity.normalized * _speed, ForceMode.Force);
        }
        else if(y < 0){
            velocity = transform.forward * -y + transform.right * x;
            _rig.AddForce(velocity.normalized * _speed, ForceMode.Force);
        }
        if(_isGround && velocity.magnitude == 0){
            GetComponent<Animator>().Play("Idle");
        }
        else if(_isGround && velocity.magnitude > 0){
            GetComponent<Animator>().Play("FastRun");
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            _isGround = false;
            StartCoroutine(jumpControll());
            _rig.AddForce(transform.up * 6, ForceMode.Impulse);
        }
    }

    void CheckSpeedLimit(){
        Vector3 flatVal = new Vector3(_rig.velocity.x,0,_rig.velocity.z);

        if(flatVal.magnitude > _speedLimit){
            Vector3 limitedVal = flatVal.normalized * _speedLimit;
            _rig.velocity = new Vector3(limitedVal.x,_rig.velocity.y,limitedVal.z);
        }
    }

    IEnumerator jumpControll(){
        GetComponent<Animator>().Play("Jumping");

        yield return new WaitForSeconds(0.6135f);
    }
}


