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
    [SerializeField] GameObject _figure;
    [SerializeField] Animator _ani;
    GameObject _temp;
    GameObject _fintemp; 
    [SerializeField] float throwPower;
    public bool baseequipped = false;
    private bool finequipped = false;
    private bool finPhase = false; 
    public float _bosshealth;
    Coroutine _coroutine = null;

    bool _finalPhase = false;
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

            yield return new WaitForSeconds(2.5f);
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

    void FinObjecttrig()
    {
        _fintemp.GetComponent<BoxCollider>().isTrigger = false;
    }

    IEnumerator FinalThrow()
    {
        while ( _bosshealth > 0 && _bosshealth <= 3f)
        {
            if (finPhase == false)
            {
                _ani.SetBool("FinalThrow", true);
                finPhase = true;
            }
            yield return new WaitForSeconds(2.5f);
        }
    }

    void FinalSpawn()
    {
        if(finequipped == false)
        {
            _fintemp = Instantiate(_figure);
            _fintemp.transform.SetParent(finweapon);
            _fintemp.transform.localPosition = Vector3.zero;

            _fintemp.GetComponent<Rigidbody>().isKinematic = true;
            _fintemp.GetComponent<BoxCollider>().isTrigger = true;

            finequipped = true;
        }
    }

    void ChangeScale()
    {
        if (finequipped == true)
        {
            _fintemp.transform.localScale = _fintemp.transform.localScale * 2f;
        }
    }

    void FinThrowAni()
    {
        if (finPhase == true)
        {
            Vector3 target = Camp.position - _fintemp.transform.position;
            _fintemp.transform.SetParent(null);
            finequipped = false;
            _fintemp.GetComponent<Rigidbody>().isKinematic = false;
            Invoke("FinObjecttrig", 0.2f);
            _fintemp.GetComponent<Rigidbody>().AddForce(target * throwPower, ForceMode.Impulse);
            _fintemp.GetComponent<Rigidbody>().AddForce(_fintemp.transform.up * 2f, ForceMode.Impulse);

            _ani.SetBool("FinalThrow", false);
            finPhase = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            _bosshealth = _bosshealth - 1;
        }
    }
}
