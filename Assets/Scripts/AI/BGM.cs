using UnityEngine;

public class BGM : MonoBehaviour
{
    [SerializeField] AudioClip _BGM;
    GameObject _player;
    AudioSource _audio;
    GameObject _Respoint; 

    private void Start()
    {
        _Respoint = GameObject.FindGameObjectWithTag("ResPoint"); 
        _player = GameObject.FindGameObjectWithTag("PlayerHolder");
        _audio = GetComponent<AudioSource>();
        _audio.clip = _BGM;
        _audio.loop = true; 
        _audio.Play();
    }
}
