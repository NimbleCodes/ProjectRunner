using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] public Image _health;
    [SerializeField] GameObject playerRes;
    [SerializeField] Transform _respwanPoint;
    [SerializeField] Animator _playeranim;
    [SerializeField] GameObject _Inven;
    Rigidbody _rb; 
    float _damageNheal = 0.25f;
    private bool isHit = false;
    float CoolTime;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void SetHealth()
    {
        _health.fillAmount = 1.0f;
    }

    public void GetCurrentHealth(float health)
    {
        health = _health.fillAmount;
    }

    void AddHealth()
    {
        _health.fillAmount += _damageNheal;
    }

    void MinusHealth()
    {
        _health.fillAmount -= _damageNheal;
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.collider.tag == "Enemy")
        {
            var direction = (transform.position - other.transform.position).normalized;
            _rb.AddForce(direction * 30f, ForceMode.Impulse);
        }
        if (other.collider.tag == "Enemy" && isHit == false)
        {
            MinusHealth();
            isHit = true;
        }
        if (other.collider.tag == "HealPack")
        {
            AddHealth();
        }
    }

    private void Update()
    {
        if (_health.fillAmount <= 0)
        {
            _playeranim.Play("Die", 0);
            _playeranim.Play("Die", 1);
            playerRes.GetComponent<PlayerRes>()._isDead = true;
            StartCoroutine(waitSecond());
        }

        if (isHit == true)
        {
            CoolTime += Time.deltaTime;
            if (CoolTime >= 1.3f)
            {
                isHit = false;
                CoolTime = 0;
            }
        }
    }

    IEnumerator waitSecond()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        _Inven.GetComponent<Inventory>().ResetItem();
        playerRes.GetComponent<PlayerRes>()._isDead = false;
        playerRes.GetComponent<PlayerRes>()._isRespwan = true;
        SetHealth();
    }

}
