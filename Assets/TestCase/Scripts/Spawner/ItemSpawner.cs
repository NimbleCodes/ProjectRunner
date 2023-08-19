using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] itemSlots;
    Transform _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void SpawnItem(){
        int randItemNum = Random.Range(0,ItemData.Instance.itemPool.itemObjects.Length);
        string itemName = ItemData.Instance.itemPool.itemObjects[randItemNum].ItemName;
        GameObject temp = Instantiate(ItemData.Instance.objPools[itemName]);
        temp.transform.position = new Vector3(transform.position.x,transform.position.y + 1, transform.position.z);
        temp.GetComponent<PickUpItem>().player = _player;
    }
}
