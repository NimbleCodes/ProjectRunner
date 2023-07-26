using UnityEngine;

public class TrackPlayer : MonoBehaviour
{

    [SerializeField] Transform _player;
    [SerializeField] Transform _AI;
    [SerializeField] float _AIspeed;
    Transform _Enemy; 
    SpriteRenderer _Renderer;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<Animator>().Play("Idle"); 
        _Enemy = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_player.position, _AI.position) < 7f && (Vector3.Distance(_player.position, _AI.position) > 4f))
        {
            GetComponent<Animator>().Play("Patroll");
            followplayer();
        }
        else if(Vector3.Distance(_player.position, _AI.position) < 4f)
        {
            GetComponent<Animator>().Play("AIAttack");
            followplayer(); 
        }
        else
        {
            GetComponent<Animator>().Play("Idle");
        }
    }

    void followplayer()
    {
        {
            _Enemy.LookAt(_player);
            _Enemy.position = Vector3.MoveTowards(_Enemy.position, _player.position, _AIspeed * Time.deltaTime);
        }
    }
}
