using UnityEngine;

public class CameraTrack : MonoBehaviour
{

    public GameObject _CamP; 
    private Vector3 playerPosition;


    private void FixedUpdate()
    {
        transform.position =_CamP.transform.position;
    }
}
