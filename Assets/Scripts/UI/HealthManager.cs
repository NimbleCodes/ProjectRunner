using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] public Image _health;
    GameObject playerRes;
    Transform _respwanPoint;
    Animator _playeranim;
    GameObject _Inven;
    AudioSource _audioSource;
    [SerializeField] AudioClip _Ouch; 

    Rigidbody _rb; 
    float _damageNheal = 0.25f;
    private bool isHit = false;
    private bool _isfreeze = false;
    float CoolTime;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        playerRes = GameObject.FindGameObjectWithTag("ResPoint"); 
        _respwanPoint = GameObject.FindGameObjectWithTag("ResPoint").transform; 
        _Inven = GameObject.FindGameObjectWithTag("Inven");
        _playeranim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
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
            _audioSource.clip = _Ouch; 
            _audioSource.Play(); 
            Vector3 direction = new Vector3(transform.position.x - other.transform.position.x, 0f, transform.position.z - other.transform.position.z); 
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
        if (other.collider.tag == "BossPoint")
        {
            _Inven.GetComponent<Inventory>().ResetItem();
        }
    }

    private void Update()
    {
        if (_health.fillAmount <= 0)
        {
            _playeranim.Play("Die", 1);
            _playeranim.Play("Die", 2);
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
