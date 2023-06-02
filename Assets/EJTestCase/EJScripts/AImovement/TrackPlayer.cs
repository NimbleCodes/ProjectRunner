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
        _Renderer = _player.gameObject.GetComponent<SpriteRenderer>();
        _Renderer = GetComponent<SpriteRenderer>();
        _Renderer = gameObject.GetComponent<SpriteRenderer>();
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
            Vector3 moveVector = (_player.position - _AI.position);
            Vector3 dirVector = moveVector.normalized;
            Vector3 lastVector = dirVector * _AIspeed; 
            _AI.position = _AI.position + lastVector * Time.deltaTime;
        }
    }
}
