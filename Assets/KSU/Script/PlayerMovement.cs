using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Transform _cam; // 카메라의 각도를 받아서 자기 방향으로 설정
    [SerializeField] float _speed;
    bool _isSloped;
    private void FixedUpdate()
    {

        //카메라가 보는 방향으로 캐릭터의 방향을 설정
        Vector3 camRot = new Vector3(_cam.transform.forward.x, 0, _cam.transform.forward.z);
        // 카메라가 보는 정면의 x축, z축 값.
        transform.rotation = Quaternion.LookRotation(camRot);
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 v3 = (transform.forward * x + transform.right * x) * _speed;
        Rigidbody rig = GetComponent<Rigidbody>();
        rig.AddForce(v3, ForceMode.Force);
        v3.y = GetComponent<Rigidbody>().velocity.y;
        GetComponent<Rigidbody>().velocity = v3;

        Vector3 flatValue = new Vector3(rig.velocity.x, 0f, rig.velocity.z);
        if (flatValue.magnitude > _speed)
        {
            Vector3 speedLimit = flatValue.normalized * _speed;
            rig.velocity = new Vector3(speedLimit.x, rig.velocity.y, speedLimit.z);
        }

    }

    //{
    //    if (_cam == null) //_cam = Camera.main.transform;
    //    {
    //        _cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    //    }
    //}
    private void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + Vector3.up * 0.1f, -transform.up, out hit, 2f)&& _isSloped == false)
        {
            if(Mathf.Abs(Vector3.Angle(Vector3.up,hit.normal))> 40)
            {
                Vector3 slopeForce = Vector3.ProjectOnPlane(Physics.gravity, hit.normal).normalized * 0.5f;
                GetComponent<Rigidbody>().AddForce(slopeForce,ForceMode.Force);
                _isSloped= true;
                Invoke("ResetSloped", 0.1f);
            }
        }
    }
    public void SetCamera(Transform cam)
    {
        _cam = cam;
    }
    void ResetSloped()
    {
        _isSloped = false;
    }
}
