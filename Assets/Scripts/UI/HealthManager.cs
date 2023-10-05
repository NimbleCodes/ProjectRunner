using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] Image _health;
    GameObject playerRes;
    Animator _playeranim;
    GameObject _Inven;
    AudioSource _audioSource;
    [SerializeField] AudioClip _Ouch;
    UIController _uiControll;

    Rigidbody _rb; 
    float _damageNheal = 0.25f;
    bool isHit = false;
    float CoolTime;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        playerRes = GameObject.FindGameObjectWithTag("ResPoint"); 
        _Inven = GameObject.FindGameObjectWithTag("Inven");
        _playeranim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.Play();
        _uiControll = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
    }

    void SetHealth()
    {
        _health.fillAmount = 1.0f;
    }

    public float GetCurrentHealth()
    {
        return _health.fillAmount;
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
            Vector3 direction = new Vector3(transform.position.x - other.transform.position.x, 0f, transform.position.z - other.transform.position.z); 
            _rb.AddForce(direction * 30f, ForceMode.Impulse);
        }
        if (other.collider.tag == "Enemy" && isHit == false)
        {
            _audioSource.clip = _Ouch; 
            MinusHealth();
            isHit = true;
        }
        if (other.collider.tag == "BossWeapon")
        {
            _audioSource.clip = _Ouch;
            MinusHealth();
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
            _playeranim.Play("Die", 1);
            _playeranim.Play("Die", 2);
            playerRes.GetComponent<PlayerRes>()._isDead = true;
            _uiControll.OpenGameOver();
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
