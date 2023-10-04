using UnityEngine;

public class PlayerInBossMap : MonoBehaviour
{
    AudioSource _audio;
    [SerializeField] AudioClip _BossMap;
    GameObject _Inven;

    private void Start()
    {
        _audio = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        _Inven = GameObject.FindGameObjectWithTag("Inven");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "BossTrigger")
        {
            _Inven.GetComponent<Inventory>().ResetItem();
            _audio.clip = _BossMap;
        }
    }
}
