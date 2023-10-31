using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoint;
    Transform _player;
    GameObject childObject;
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
            temp.transform.position = new Vector3(_spawnPoint[i].position.x,
            _spawnPoint[i].position.y + 1, _spawnPoint[i].position.z);
            temp.transform.GetChild(0).GetComponent<PickUpItem>().player = _player;
            temp.GetComponent<Rigidbody>().freezeRotation = true;
            temp.GetComponent<Rigidbody>().isKinematic = true;
            temp.transform.GetChild(0).GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
