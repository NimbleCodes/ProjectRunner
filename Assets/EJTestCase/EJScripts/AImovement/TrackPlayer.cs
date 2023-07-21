using System.Collections;
using System.Collections.Generic;
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
        _Enemy = transform;
     
    }
    void FixedUpdate()
    {
        if(_player == null)_player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(_player.position, _AI.position) < 5f)
        {
            followplayer();
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
