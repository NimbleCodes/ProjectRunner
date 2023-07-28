using UnityEngine;

public class TrackPlayer : MonoBehaviour
{

    [SerializeField] Transform _player;
    [SerializeField] Transform _AI;
    [SerializeField] float _AIspeed;
    Transform _Enemy;
    SpriteRenderer _Renderer;
    public bool _isPlayerDead = false;
    public bool IsPlayerDead { set { _isPlayerDead = value; } }

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Animator>().Play("Idle");
        _Enemy = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPlayerDead)
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
            _Enemy.LookAt(_player);
            _Enemy.position = Vector3.MoveTowards(_Enemy.position, _player.position, _AIspeed * Time.deltaTime);
    }

    public void behave()
    {
        if (Vector3.Distance(_player.position, _AI.position) < 4f)
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

}
