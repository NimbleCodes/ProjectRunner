using UnityEngine;

public class TrackPlayer : MonoBehaviour
{

    [SerializeField] Transform _player;
    [SerializeField] Transform _AI;
    [SerializeField] float _AIspeed;
    [SerializeField] ParticleSystem _particle; 
    Transform _Enemy;
    [SerializeField] SkinnedMeshRenderer _Renderer;
    public bool _isPlayerDead = false;
    public bool IsPlayerDead { set { _isPlayerDead = value; } }
    private bool AIdead = false; 

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Animator>().Play("Idle");
        _Enemy = transform;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPlayerDead == false)
        {
            behave();
        }
        else
        {
            GetComponent<Animator>().Play("Laugh");
        }
    }

    void followplayer()
    {
        if (AIdead == false)
        {
           _Enemy.LookAt(_player);
           _Enemy.position = Vector3.MoveTowards(_Enemy.position, _player.position, _AIspeed * Time.deltaTime);
        }
    }

    public void behave()
    {
        if (Vector3.Distance(_player.position, _AI.position) < 8f)
        {
            GetComponent<Animator>().Play("AIRunning");
            followplayer();
        }
        else
        {
            GetComponent<Animator>().Play("Idle");
            _Enemy.LookAt(_player);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            _particle.Play(); 
            _Renderer.enabled = false;
            AIdead = true;
            Destroy(gameObject, 1f); 
        }
    }

}
