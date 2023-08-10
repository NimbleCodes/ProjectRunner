using UnityEngine;

public class Camcontroll : MonoBehaviour
{
    [SerializeField] Transform _realCam;
    [SerializeField] Transform _fvPos; //1인칭 포지션
    [SerializeField] Transform _follow;
    //3인칭 포지션

    float rotX;
    float rotY;
    float whellPos;
    
    Vector3 _tvPos;

    private void Start()
    {
        _tvPos = _realCam.localPosition;
        whellPos = 1;
        _follow = GameObject.FindGameObjectWithTag("Player").transform;

    }

    void Update()
    {
        rotX -= Input.GetAxis("Mouse Y");
        rotY += Input.GetAxis("Mouse X");
        transform.rotation = Quaternion.Euler(rotX, rotY, 0);
        Vector3 playerPos = new Vector3(_follow.position.x, _follow.position.y + 1.5f, _follow.position.z); 
        transform.position = playerPos;
        
        
        
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        whellPos -= wheelInput;
        //Debug.Log("wheel Input value :" + wheelInput);
        //Vector3.Lerp();//1인칭 정보, 3인칭 정보
        //Mathf.Lerp(0, 1, 0.3f) =  a(b-a)*t
        if(whellPos > 1) whellPos = 1;
        if (whellPos < 0) whellPos = 0;
        _realCam.localPosition = Vector3.Lerp(_fvPos.localPosition, _tvPos, whellPos);
        //localPosition 부모로부터의 상속위치를 따라가지않는다.       
    }
}
//문제
// 카메라 위치를 1인칭부터 시작해서 
// 마우스 휠을 아래로 내리면 3인칭이 되고
// 위로 올리면 다시 1인칭이 되는
// 카메라를 구현하세요
// 단, 좌클릭시 1인칭이 바로 되고

