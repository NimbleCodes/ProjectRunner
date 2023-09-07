using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _Boss;
    [SerializeField] Transform weaponPoint;
    [SerializeField] GameObject[] _weapon;
    [SerializeField] Animator _ani;
    GameObject _temp;
    [SerializeField] float throwPower;
    private bool equipped = false;

    private void Start()
    {
        Spawn();
        StartCoroutine(ThrowObject()); 
    }

    void Update()
    {
        LookAt();
    }

    void LookAt()
    {
        _Boss.LookAt(_player);
    }


    void Spawn()
    {
        Vector3 target = _player.position - _Boss.position;
        float distance = Vector3.Distance(_player.position, _Boss.position);
        if (equipped == false)
        {
            int selection = Random.Range(0, _weapon.Length);
            GameObject selectedWeapon = _weapon[selection];

            Transform selectedSpot = weaponPoint;

            _temp = Instantiate(selectedWeapon);
            _temp.transform.SetParent(selectedSpot);
            _temp.transform.localPosition = Vector3.zero;
            _temp.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

            _temp.GetComponent<Rigidbody>().isKinematic = true;
            _temp.GetComponent<BoxCollider>().isTrigger = true;

            equipped = true;
        }
    }


    IEnumerator ThrowObject() //2.5 초마다 생성된 오브젝트 던지기
    {
        while (true)
        {
            Vector3 target = _player.position - _Boss.position;
            float distance = Vector3.Distance(_player.position, _Boss.position);
            
            if (distance < 20f && equipped)
            {
                Invoke("RealThrow", 3f);
            }
            Invoke("Spawn", 1f);
            yield return new WaitForSeconds(2.5f);
        }
    }

    void RealThrow()
    {
        Vector3 target = _player.position - _Boss.position;
        _temp.transform.SetParent(null);
        _temp.GetComponent<Rigidbody>().isKinematic = false;
        _temp.GetComponent<BoxCollider>().isTrigger = false;
        _temp.GetComponent<Rigidbody>().AddForce(target * throwPower, ForceMode.Impulse);
        _temp.GetComponent<Rigidbody>().AddForce(_temp.transform.up * 6f, ForceMode.Impulse);

        equipped = false;
    }

}
