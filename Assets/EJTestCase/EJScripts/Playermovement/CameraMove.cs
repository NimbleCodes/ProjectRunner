using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("Refernces")]
    public Transform player;
    public Rigidbody rb;
    public Transform orientation;
    public Transform playerObj; 

    public float rotationSpeed;

    public Transform playerlookAt;

    public CameraStyle currentstyle; 
    public enum CameraStyle
    {
        Basic,
        Combat,
        Topdown
    }
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if (currentstyle == CameraStyle.Basic || currentstyle == CameraStyle.Topdown)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 inputDir = orientation.forward * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

        }
        else if(currentstyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = playerlookAt.position - new Vector3(transform.position.x, playerlookAt.position.y, transform.position.z); 
            orientation.forward = dirToCombatLookAt.normalized;

            playerObj.forward = dirToCombatLookAt.normalized;
        }
       
    }



    
}
