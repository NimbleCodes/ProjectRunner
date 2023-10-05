using UnityEngine;

public class BossDie : MonoBehaviour
{
    [SerializeField] ParticleSystem _bossdead;
    [SerializeField] ParticleSystem _bossbomb;
    [SerializeField] SkinnedMeshRenderer _Renderer; 
    
    float _bosshealth;
    Animator _ani;

    private void Start()
    {
        _ani = GetComponent<Animator>();
    }

    private void Update()
    {
        _bosshealth = GetComponent<BossAttack>()._bosshealth;
        if (_bosshealth <= 0)
        {
            _ani.Play("Die", 0);
            _ani.Play("Die", 1);
        }
    }

    void DeadEffect()
    {
        _bossdead.Play();
    }

    void DeadBomb()
    {
        _Renderer.enabled = false;
        _bossbomb.Play();
        Destroy(gameObject, 4f);
    }
}
