using UnityEngine;
using UnityEngine.AI; 

public class TrackPlayer : MonoBehaviour
{
    Transform _player;
    Transform _AI;
    [SerializeField] ParticleSystem _particle; 
    Transform _Enemy;
    [SerializeField] SkinnedMeshRenderer _Renderer;
    AudioSource _AIdead; 
    private Vector3 _targetPos;
    NavMeshAgent agent; 
    public bool _isPlayerDead = false;
    public bool IsPlayerDead { set { _isPlayerDead = value; } }
    private bool AIdead = false;
    private bool isHit = false;
    float CoolTime;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Animator>().Play("Idle");
        _Enemy = transform;
        _player = GameObject.FindGameObjectWithTag("Player").transform;   
        agent = GetComponent<NavMeshAgent>();
        _AI = GetComponent<Transform>();
        _AIdead = GetComponent<AudioSource>();
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

        if (isHit == true)
        {
            CoolTime += Time.deltaTime;
            if (CoolTime >= 1f)
            {
                isHit = false;
                CoolTime = 0;
            }
        }
    }

    void followplayer()
    {
        if (AIdead == false)
        {
            _targetPos = new Vector3(_player.position.x, transform.position.y, _player.transform.position.z);
            _Enemy.LookAt(_targetPos);
            //_Enemy.position = Vector3.MoveTowards(_Enemy.position, _player.position, _AIspeed * Time.deltaTime);
            agent.SetDestination(_player.position); 
        }
    }

    public void behave()
    {
        if (Vector3.Distance(_player.position, _AI.position) < 15f)
        {
            GetComponent<Animator>().Play("AIRunning");
            followplayer();
        }
        else
        {
            GetComponent<Animator>().Play("Idle");
            _targetPos = new Vector3(_player.position.x, transform.position.y, _player.transform.position.z);
            _Enemy.LookAt(_targetPos);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon") && isHit == false)
        {
            AIdead = true;
            _AIdead.Play();
            _particle.Play();
            isHit = true;
            _Renderer.enabled = false;
            Destroy(gameObject, 1f);
        }
    }

}
