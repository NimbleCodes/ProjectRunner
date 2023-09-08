using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image _health;
    [SerializeField] GameObject playerRes;
    [SerializeField] Transform _respwanPoint;
    [SerializeField] Animator _playeranim; 
    float _damageNheal = 0.25f;
    

    void SetHealth(){
        _health.fillAmount = 1.0f;
    }

    public void GetCurrentHealth(float health){
        health = _health.fillAmount;
    }

    void AddHealth(){
        _health.fillAmount += _damageNheal;
    }

    void MinusHealth(){
        _health.fillAmount -= _damageNheal;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag == "Enemy"){
            MinusHealth();
        }
        if(other.collider.tag == "HealPack"){
            AddHealth();
        }
    }

    private void Update()
    {
        if (_health.fillAmount <= 0)
        {
            _playeranim.Play("Die");
            playerRes.GetComponent<PlayerRes>()._isDead = true;
            playerRes.GetComponent<PlayerRes>()._dead = true;
            StartCoroutine(waitSecond());
            //playerRes.GetComponent<PlayerRes>().setDeath(true);
            
        }
    }

    IEnumerator waitSecond()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        SetHealth();
    }
}
