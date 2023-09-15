using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoint;
    Transform _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        SpawnItem();
    }

    void SpawnItem(){
        for(int i =0; i < _spawnPoint.Length; i++){
            int randItemNum = Random.Range(0,ItemData.Instance.itemPool.itemObjects.Length);
            string itemName = ItemData.Instance.itemPool.itemObjects[randItemNum].ItemName;
            GameObject temp = Instantiate(ItemData.Instance.objPools[itemName]);
            temp.transform.position = new Vector3(_spawnPoint[i].position.x,_spawnPoint[i].position.y + 1, _spawnPoint[i].position.z);
            temp.GetComponent<PickUpItem>().player = _player;
        }
    }
}
