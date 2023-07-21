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
        if(_mobs == null) _mobs = GameObject.FindGameObjectsWithTag("Mob");
    }

    public void SetSpawnPoints(Transform[] Points){
        _spawnPoints = Points;

    }

    public void SetMobs(GameObject[] mobs){
        _mobs = mobs;
    }

    void SpawnOnce(){
        for(int i =0; i < _spawnPoints.Length; i++){
            int randNum = Random.Range(0,_mobs.Length-1);
            GameObject mob = Instantiate(_mobs[randNum],_patrolPoints[i]);
            mob.SetActive(true);        
        }
    }
}
