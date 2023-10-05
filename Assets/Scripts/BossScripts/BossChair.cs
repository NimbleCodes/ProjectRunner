using UnityEngine;

public class BossChair : MonoBehaviour
{
    public Transform _player;
    public Transform _Boss; 
    Vector3 _targetPos;
    Transform _BossChair;
    float _bosshealth; 

    private void Start()
    {
        _BossChair = GetComponent<Transform>();
        _BossChair.transform.SetParent(_Boss); 
        _BossChair.transform.localPosition = Vector3.zero; 
    }

    void Update()
    {
        _bosshealth = _Boss.GetComponent<BossAttack>()._bosshealth;
        if (_bosshealth > 3f)
        {
            LookAt();
        }
        else if (_bosshealth <= 3f)
        {
            _BossChair.transform.SetParent(null);
        }
    }

    void LookAt()
    {
        _targetPos = new Vector3(_player.position.x, transform.position.y, _player.transform.position.z);
        _BossChair.LookAt(_targetPos);
    }
}
