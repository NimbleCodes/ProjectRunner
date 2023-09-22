using System.Collections;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed; 
    Rigidbody _rig;
    public bool _isRotated = false;
    public bool _is2Ground = false;
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
            _is2Ground = true;
        }
        else
        {
            _is2Ground = false;
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
        if(_is2Ground && velocity.magnitude == 0){
            GetComponent<Animator>().Play("Idle");
        }
        else if(_is2Ground && velocity.magnitude > 0){
            GetComponent<Animator>().Play("FastRun");
        }

        if(Input.GetKeyDown(KeyCode.Space)){
            _is2Ground = false;
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


