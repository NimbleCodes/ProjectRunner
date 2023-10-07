using UnityEngine;

public class Giggling : MonoBehaviour
{
    [SerializeField] AudioClip _giggling; 
    [SerializeField] AudioSource _BGM;
    GameObject _player;
    AudioSource _audio;
    
    bool _isSoundPlay = true;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerHolder");
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.GetComponent<HealthManager>().GetCurrentHealth() <= 0 && _isSoundPlay == true)
        {
            _BGM.Stop();
            _audio.clip = _giggling;
            _audio.Play();
            _isSoundPlay = false;
            
        }
    }
}
