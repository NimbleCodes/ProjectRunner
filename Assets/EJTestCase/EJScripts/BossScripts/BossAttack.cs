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
    [SerializeField] float throwPower;
    public bool baseequipped = false;
    private bool finequipped = false;
    private float _bosshealth;
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
        if (_bosshealth < 8f && _finalPhase == false)
        {
            _finalPhase = true;
            Destroy(_temp);
            StopCoroutine(_coroutine);
            FinalSpawn();
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

            if (distance < 20f && baseequipped)
            {
                _ani.SetBool("isThrow", true);
            }
            Invoke("Spawn", 1f);

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

    void FinalSpawn()
    {
        if ((finequipped == false) && (_bosshealth < 3f))
        {
            _temp = Instantiate(_figure);
            _temp.transform.SetParent(finweapon);
            _temp.transform.localPosition = Vector3.zero;

            _temp.GetComponent<Rigidbody>().isKinematic = true;
            _temp.GetComponent<BoxCollider>().isTrigger = true;
            _ani.SetBool("FinalThrow", false);

            finequipped = true;
        }
    }


    IEnumerator FinalThrow()
    {
        while (_bosshealth < 3f)
        {
            Vector3 target = _player.position - _Boss.position;
            float distance = Vector3.Distance(_player.position, _Boss.position);

            if (finequipped == true)
            {
                _ani.SetBool("FinalThrow", true);
            }
            Invoke("FinalSpawn", 1f);
            yield return new WaitForSeconds(2.5f);
        }
    }

    void ChangeScale()
    {
        _temp.transform.localScale = _temp.transform.localScale * 2f;
    }

    void FinThrowAni()
    {
        Vector3 target = Camp.position - _temp.transform.position;
        _temp.transform.SetParent(null);
        _temp.GetComponent<Rigidbody>().isKinematic = false;
        Invoke("Objecttrig", 0.2f);
        _temp.GetComponent<Rigidbody>().AddForce(target * throwPower, ForceMode.Impulse);
        _temp.GetComponent<Rigidbody>().AddForce(_temp.transform.up * 2f, ForceMode.Impulse);

        finequipped = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            _bosshealth = _bosshealth - 1;
        }
    }
}
