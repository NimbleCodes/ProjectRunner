using UnityEngine;

public class Giggling : MonoBehaviour
{
    GameObject _player;
    AudioSource _audio;
    [SerializeField] AudioClip _giggling; 

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("PlayerHolder");
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.GetComponent<HealthManager>().GetCurrentHealth() <= 0)
        {
            _audio.clip = _giggling;
            _audio.Play();
        }
    }
}
