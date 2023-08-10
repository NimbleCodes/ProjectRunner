using UnityEngine;

public class Camcontroll : MonoBehaviour
{
    [SerializeField] Transform _realCam;
    [SerializeField] Transform _fvPos; //1��Ī ������
    [SerializeField] Transform _follow;
    //3��Ī ������

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
        //Vector3.Lerp();//1��Ī ����, 3��Ī ����
        //Mathf.Lerp(0, 1, 0.3f) =  a(b-a)*t
        if(whellPos > 1) whellPos = 1;
        if (whellPos < 0) whellPos = 0;
        _realCam.localPosition = Vector3.Lerp(_fvPos.localPosition, _tvPos, whellPos);
        //localPosition �θ�κ����� �����ġ�� �������ʴ´�.       
    }
}
//����
// ī�޶� ��ġ�� 1��Ī���� �����ؼ� 
// ���콺 ���� �Ʒ��� ������ 3��Ī�� �ǰ�
// ���� �ø��� �ٽ� 1��Ī�� �Ǵ�
// ī�޶� �����ϼ���
// ��, ��Ŭ���� 1��Ī�� �ٷ� �ǰ�

