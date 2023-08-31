using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Transform _Boss;
    [SerializeField] Transform[] weaponPoint;
    [SerializeField] GameObject[] _weapon;
    [SerializeField] Animator _ani; 
    public float throwPower; 
    private bool equipped = false;

    void Update()
    {
        LookPlayer();
        Spawn();
    }

    void LookPlayer()
    {
        transform.LookAt(_player);
    }

    void Spawn()
    {
        Vector3 target; 
        float distance = Vector3.Distance(_player.position, _Boss.position);

        Ray ray = new Ray(); 
        RaycastHit playerhit;
        int PlayerMask = LayerMask.GetMask("Player"); 
        

        if (equipped == false)
        {
            int selection = Random.Range(0, _weapon.Length);
            GameObject selectedWeapon = _weapon[selection];

            int selectedPoint = Random.Range(0, weaponPoint.Length);
            Transform selectedSpot = weaponPoint[selectedPoint];

            GameObject instance = Instantiate(selectedWeapon);
            instance.transform.SetParent(selectedSpot);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = Quaternion.Euler(Vector3.zero);
            instance.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

            instance.GetComponent<Rigidbody>().isKinematic = true;
            instance.GetComponent<BoxCollider>().isTrigger = true;

            equipped = true;

            if (Physics.Raycast(ray, out playerhit, 20f, PlayerMask))
            {
                target =  (playerhit.point - selectedSpot.position).normalized;
                instance.transform.SetParent(null);
                target.y = 25f;
                instance.GetComponent<Rigidbody>().isKinematic = false;
                instance.GetComponent<Rigidbody>().AddForce(target * throwPower, ForceMode.Impulse);
                
                equipped = false;
            }
        }
    }


    void FinalAttack()
    {

    }
}
