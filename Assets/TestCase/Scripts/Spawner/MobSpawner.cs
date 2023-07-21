using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _mobs;
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] Transform[] _patrolPoints;

    void FixedUpdate()
    {
        if(_mobs == null) _mobs = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Start()
    {
        SpawnOnce();
    }

    void SetSpawnPoints(Transform[] Points){
        _spawnPoints = Points;

    }

    void SpawnOnce(){
        for(int i =0; i <_spawnPoints.Length; i++){
            int randNum = Random.Range(0,1);
            GameObject mob = Instantiate(_mobs[0],_spawnPoints[0]);
            mob.transform.position = _spawnPoints[i].transform.position;
        }
    }
}
