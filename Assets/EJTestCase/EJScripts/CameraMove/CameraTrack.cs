using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CameraTrack : MonoBehaviour
{

    public GameObject _CamP; 
    float speed = 3f; // ī�޶� �̵��ӵ� 
    private Vector3 playerPosition;


    private void FixedUpdate()
    {
        transform.position =_CamP.transform.position;
    }
}
