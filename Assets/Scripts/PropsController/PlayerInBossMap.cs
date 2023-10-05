using UnityEngine;

public class PlayerInBossMap : MonoBehaviour
{
    [SerializeField] AudioClip _BossMap;
    [SerializeField] GameObject[] Enemyz = null;
    GameObject _Inven;
    GameObject _BossTrigger;
    AudioSource _audio; 

    private void Start()
    {
        _audio = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        _Inven = GameObject.FindGameObjectWithTag("Inven");
        Enemyz = GameObject.FindGameObjectsWithTag("Enemy");
        _BossTrigger = GetComponent<GameObject>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _Inven.GetComponent<Inventory>().ResetItem();
            _audio.clip = _BossMap;
            _audio.Play();

            for (int i = 0; i < Enemyz.Length; i++)
            {
                Destroy(Enemyz[i]);
            }

            _BossTrigger.SetActive(false);
        }
    }

    public void InitBossStage()
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
