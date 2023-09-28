using System.Collections;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _Boss;
    [SerializeField] Transform weaponPoint;
    [SerializeField] Transform Camp; 
    [SerializeField] Transform finweapon;
    [SerializeField] GameObject[] _weapon;
    [SerializeField] ParticleSystem _particle; 
    [SerializeField] GameObject document;
    [SerializeField] Animator _ani;
    GameObject _temp;
    GameObject _fintemp;  
    [SerializeField] float throwPower;
    private bool baseequipped = false;
    private bool finequipped = false;
    private bool isStand = false; 
    private bool isFinphase = false; 
    public float _bosshealth;
    Coroutine _coroutine = null;
    private float distance; 


    private void Start()
    {
        BaseSpawn();
        _bosshealth = 10f;
        _coroutine = StartCoroutine(BaseThrowObject());
    }

    void Update()
    {
        LookAt();
        if (_bosshealth <= 3f)
        {
            Destroy(_temp);
            baseequipped = false; 
            StopCoroutine(_coroutine);
            _ani.SetBool("StandUp", true);
            isStand = true;
        }

        if (isStand == true)
        {
            StartCoroutine(FinalThrow());
        }
    }


    void LookAt()
    {
        _Boss.LookAt(_player);
    }


    void BaseSpawn()
    {
        if (baseequipped == false)
        {
            _particle.Play();
            int selection = Random.Range(0, _weapon.Length);
            GameObject selectedWeapon = _weapon[selection];
            _temp = Instantiate(selectedWeapon);
            _temp.transform.SetParent(weaponPoint);
            _temp.transform.localPosition = Vector3.zero;
            _temp.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

            _temp.GetComponent<Rigidbody>().isKinematic = true;
            _temp.GetComponent<BoxCollider>().isTrigger = true;
            _ani.SetBool("isThrow", false);

            baseequipped = true;
        }
    }


    IEnumerator BaseThrowObject() //2.5 초마다 생성된 오브젝트 던지기
    {
        while (_bosshealth > 3f)
        {
            Vector3 target = _player.position - _Boss.position;
            float distance = Vector3.Distance(_player.position, _Boss.position);

            if (distance < 20f && baseequipped == true)
            {
                _ani.SetBool("isThrow", true);
            }
            Invoke("BaseSpawn", 1f);

            yield return new WaitForSeconds(2f);
        }
    }

    void BaseThrowAni()
    {
        Vector3 target = Camp.position - weaponPoint.position;
        _temp.transform.SetParent(null);
        _temp.GetComponent<Rigidbody>().isKinematic = false;
        Invoke("Objecttrig", 0.2f);
        _temp.GetComponent<Rigidbody>().AddForce(target * throwPower, ForceMode.Impulse);
        _temp.GetComponent<Rigidbody>().AddForce(_temp.transform.up * 6f, ForceMode.Impulse);
        baseequipped = false;
    } 

    void Objecttrig()
    {
        _temp.GetComponent<BoxCollider>().isTrigger = false;
    }

    IEnumerator FinalThrow()
    {
        while ( _bosshealth > 0 && _bosshealth <= 3f)
        {
            if (isFinphase == false)
            {
                _ani.SetBool("FinalThrow", false);
            }
            else if (isFinphase == true)
            {
                _ani.SetBool("FinalThrow", true);
            }
            yield return new WaitForSeconds(5f);
        }
    }

    void changePhase()
    {
        isFinphase = true; 
    }

    void FinalSpawn()
    {
        if (finequipped == false)
        {
            finequipped = true;
            _fintemp = Instantiate(document);
            _fintemp.transform.SetParent(finweapon);
            _fintemp.transform.localPosition = Vector3.zero;

            _fintemp.GetComponent<Rigidbody>().isKinematic = true;
            _fintemp.GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    void ChangeScale()
    {
        _fintemp.transform.localScale = _fintemp.transform.localScale * 2f;
    }

    void FinThrowAni()
    {
        Vector3 target = Camp.position - _fintemp.transform.position;
        _fintemp.transform.SetParent(null);
        _fintemp.GetComponent<Rigidbody>().isKinematic = false;
        Invoke("FinObjecttrig", 0.2f);
        _fintemp.GetComponent<Rigidbody>().AddForce(target * throwPower, ForceMode.Impulse);
        _fintemp.GetComponent<Rigidbody>().AddForce(_fintemp.transform.up * 2f, ForceMode.Impulse);
        isFinphase = false;
        finequipped = false; 
    }

    void FinObjecttrig()
    {
        _fintemp.GetComponent<BoxCollider>().isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            _bosshealth = _bosshealth - 1;
        }
    }
}
