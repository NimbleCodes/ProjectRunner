using System.Collections;
using UnityEngine;

public class PlayerRes : MonoBehaviour
{
    GameObject _player;
    [SerializeField] CapsuleCollider _playerCollider;
    Transform _respawnPoint;
    public GameObject[] Enemyz = null;
    public bool _isDead = false;
    public bool _isRespwan = false;
    public bool _isPannelClose = false; 

    private void Start()
    {
        _respawnPoint = GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("PlayerHolder"); 
        _playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if(_isDead == true)
        {
            Enemyz=GameObject.FindGameObjectsWithTag("Enemy");
            for(int i=0; i<Enemyz.Length; i++)
            {
                Enemyz[i].GetComponent<TrackPlayer>().IsPlayerDead = true;
            }
        }

        if (_isDead == false && _isRespwan == true && _isPannelClose == true)
        {
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        _isRespwan = false; 
        _player.SetActive(true);
        _player.transform.position = _respawnPoint.position;
        _player.GetComponent<Rigidbody>().useGravity = true;
        _playerCollider.enabled = true;
        for (int i = 0; i < Enemyz.Length; i++)
        {
            Enemyz[i].GetComponent<TrackPlayer>()._isPlayerDead = false;
        }
        Enemyz = null;
    }
}
