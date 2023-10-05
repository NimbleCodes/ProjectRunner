using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] AudioClip _BGM;
    [SerializeField] AudioClip _Giggling;
    GameObject _player;
    AudioSource _audio; 

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerHolder"); 
        _audio = GetComponent<AudioSource>();
        _audio.clip = _BGM;
        _audio.Play();
    }

    private void Update()
    {
        if (_player.GetComponent<HealthManager>().GetCurrentHealth() <= 0)
        {
            //_audio.PlayOneShot(_Giggling); 
        }
    }
}