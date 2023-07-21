using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed; 
    Rigidbody _rig;
    public bool _isRotated = false;
    void Start()
    {
        _rig = this.GetComponent<Rigidbody>();
        _rig.freezeRotation = true;
    }


    void Update()
    {
        if(Input.GetKey(KeyCode.Tab)){
            FreeCamMovement();
        }else{
            MoveLocalTransform();
            MovementWithRotation();
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
        
        //blenderTree 전용
        // GetComponent<Animator>().SetFloat("X",x);
        // GetComponent<Animator>().SetFloat("Y",y);
    }

    void MovementAnimation(){ 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //아무 입력도 없을시 idle 실행
        if(!Input.anyKey || (x ==0 && y ==0)){
            GetComponent<Animator>().Play("Idle");
        }
        //대각선 입력시 앞 혹은 뒤로가기 애니메이션
        if((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))){
            GetComponent<Animator>().Play("FastRun");
        }else if((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))){
            GetComponent<Animator>().Play("RunningBack");
        }else if(Input.GetKey(KeyCode.W)){ //전후좌우 애니메이션
            GetComponent<Animator>().Play("FastRun");
        }else if(Input.GetKey(KeyCode.A)){
            GetComponent<Animator>().Play("LeftRun");
        }else if(Input.GetKey(KeyCode.D)){
            GetComponent<Animator>().Play("RightRun");
        }else if(Input.GetKey(KeyCode.S)){
            GetComponent<Animator>().Play("RunningBack");
        }
    }

    void MovementWithRotation(){
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //아무 입력도 없을시 idle 실행
        if(!Input.anyKey || (x ==0 && y ==0)){
            GetComponent<Animator>().Play("Idle");
        }
        //대각선 입력시 앞 혹은 뒤로가기 애니메이션
        if((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))){
            GetComponent<Animator>().Play("FastRun");
        }else if((Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))){
            transform.rotation = transform.rotation * Quaternion.Euler(0,180,0);
            GetComponent<Animator>().Play("FastRun");
        }else if(Input.GetKey(KeyCode.W)){ //전후좌우 애니메이션
            GetComponent<Animator>().Play("FastRun");
        }else if(Input.GetKey(KeyCode.A)){
            transform.rotation = transform.rotation * Quaternion.Euler(0,-90,0);
            GetComponent<Animator>().Play("FastRun");
        }else if(Input.GetKey(KeyCode.D)){
                transform.rotation = transform.rotation * Quaternion.Euler(0,90,0);
            //transform.rotation = Quaternion.Euler(0,90,0);
            GetComponent<Animator>().Play("FastRun");
        }else if(Input.GetKey(KeyCode.S)){
            transform.rotation = transform.rotation * Quaternion.Euler(0,180,0);
            GetComponent<Animator>().Play("FastRun");
        }
    }

    void FreeCamMovement(){
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 vPos = transform.position;
        vPos += transform.forward * x * Time.deltaTime * _speed;
        vPos += transform.forward * y * Time.deltaTime * _speed;
        transform.position = vPos;
        if(Input.GetKey(KeyCode.D)){
            if(!_isRotated){
                transform.rotation = transform.rotation * Quaternion.Euler(0,90,0);
                _isRotated = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.D)){_isRotated = false;}
    }
        
}


