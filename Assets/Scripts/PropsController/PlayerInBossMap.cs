using UnityEngine;

public class PlayerInBossMap : MonoBehaviour
{
    [SerializeField] AudioClip _BossMap;
    [SerializeField] GameObject[] Enemyz = null;
    GameObject _Inven;
    GameObject _BossTrigger;
    AudioSource _audio; 
    public bool _isTriggered = false;
    private void Start()
    {
        _audio = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        _Inven = GameObject.FindGameObjectWithTag("Inven");
        Enemyz = GameObject.FindGameObjectsWithTag("Enemy");
        _BossTrigger = GetComponent<GameObject>(); 
    }

    public void InitBossStage()
    {
        if(_isTriggered == false){
            _Inven.GetComponent<Inventory>().ResetItem();
            _audio.clip = _BossMap;
            _audio.Play();

            for (int i = 0; i < Enemyz.Length; i++)
            {
                Enemyz[i].SetActive(false);
            }
        }
    }
}
