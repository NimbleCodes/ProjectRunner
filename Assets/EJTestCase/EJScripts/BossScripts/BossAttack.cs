using Cinemachine.Utility;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _Boss;
    [SerializeField] Transform[] weaponPoint;
    [SerializeField] Transform finweapon; 
    [SerializeField] GameObject[] _weapon;
    [SerializeField] Animator _ani;
    GameObject _temp;
    [SerializeField] float throwPower;
    private float time;
    private bool equipped = false;

    }

    }
    private void Start()
    {
        Spawn();
        StartCoroutine(ThrowObject());
    }
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

            int selectedPoint = Random.Range(0, weaponPoint.Length);
            Transform selectedSpot = weaponPoint[selectedPoint];

    }


    void Spawn()
    {

        if (equipped == false)
        {
            int selection = Random.Range(0, _weapon.Length);
            GameObject selectedWeapon = _weapon[selection];
            _temp = Instantiate(selectedWeapon);
            _temp.transform.SetParent(selectedSpot);
            _temp.transform.localPosition = Vector3.zero;
            _temp.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

        
        while (true)
            _temp.GetComponent<BoxCollider>().isTrigger = true;

            equipped = true;
        }

    }
                _temp.transform.SetParent(null);
                _temp.GetComponent<Rigidbody>().isKinematic = false;
                _temp.GetComponent<BoxCollider>().isTrigger = false;
                _temp.GetComponent<Rigidbody>().AddForce(target * throwPower, ForceMode.Impulse);
                _temp.GetComponent<Rigidbody>().AddForce(_temp.transform.up * 6f , ForceMode.Impulse); 
        }
                equipped = false;
            }
            
            
            yield return new WaitForSeconds(2.5f);

            Spawn();

        }
    }

        equipped = false;
    }

    void Objecttrig()
    {
        _temp.GetComponent<BoxCollider>().isTrigger = false;
    }

    void FinalSpawn()
    {
        if ((finequipped == false) && (_bosshealth < 8f))
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
        while (_bosshealth < 8f)
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

    void RealFinThrow()
    {
        Vector3 target = _player.position -_temp.transform.position;
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
