using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRes : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] CapsuleCollider _playerCollider;
    [SerializeField] Transform _respawnPoint;
    public GameObject[] Enemyz = null;
    public bool _isDead = false;
    public bool _dead = false;

    void Update()
    {
        if(_isDead)
        {
            Enemyz=GameObject.FindGameObjectsWithTag("Enemy");
            for(int i=0; i<Enemyz.Length; i++)
            {
                Enemyz[i].GetComponent<TrackPlayer>()._isPlayerDead = true;
                 
            }
            StartCoroutine(Respawn());
            
        }
    }

    public void setDeath(bool isDead)
    {
        _isDead = isDead;
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(3f);
        _player.transform.position = _respawnPoint.position;
        _player.GetComponent<Rigidbody>().useGravity = true;
        _playerCollider.enabled = true;
        _player.SetActive(true);
        _isDead = false;
        for (int i = 0; i < Enemyz.Length; i++)
        {
            Enemyz[i].GetComponent<TrackPlayer>()._isPlayerDead = false;
        }
        Enemyz = null;
    }
}
