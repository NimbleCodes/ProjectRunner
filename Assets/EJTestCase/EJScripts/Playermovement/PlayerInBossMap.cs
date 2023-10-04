using UnityEngine;

public class PlayerInBossMap : MonoBehaviour
{
    AudioSource _audio;
    [SerializeField] AudioClip _BossMap;
    GameObject _Inven;
    public GameObject[] Enemyz = null;
    GameObject _BossTrigger; 

    private void Start()
    {
        _audio = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        _Inven = GameObject.FindGameObjectWithTag("Inven");
        Enemyz = GameObject.FindGameObjectsWithTag("Enemy");
        _BossTrigger = GameObject.FindGameObjectWithTag("BossTrigger");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "BossTrigger")
        {
            _Inven.GetComponent<Inventory>().ResetItem();
            _audio.clip = _BossMap;

            for (int i = 0; i < Enemyz.Length; i++)
            {
                Enemyz[i].SetActive(false);
            }

            _BossTrigger.SetActive(false);
        }
    }
}
