using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] GameObject[] _itemSlot;
    [SerializeField] GameObject[] _itemLife;
    [SerializeField] Transform _itemContainer;
    [SerializeField] Text[] _itemHealthText; 
    public bool _AllslotFull = false;
    public List<ItemController> Items = new List<ItemController>();
    int currentHealth;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // CheckFullSlot();
    }

    public void Add(ItemController item)
    {
        if (Items.Count < 3)
        {
            Items.Add(item);
        }
    }

    public void Remove(ItemController item)
    {
        Items.Remove(item);
    }

    public void SwapItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           if(Items != null){
            foreach(ItemController item in Items){
                item._gameObject.SetActive(false);
            }
            Items[0]._gameObject.SetActive(true);
            Items[0]._gameObject.transform.SetParent(_itemContainer);
            Items[0]._gameObject.transform.localPosition = Vector3.zero;
            Items[0]._gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            Items[0]._gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

            Items[0].rig.isKinematic = true;
            Items[0].coll.isTrigger = true;
           }
           else
           {
                //Do Nothing
           }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(Items.Count >= 2){
                foreach(ItemController item in Items){
                item.GetComponent<GameObject>().SetActive(false);
            }
            Items[1]._gameObject.SetActive(true);
            Items[1]._gameObject.transform.SetParent(_itemContainer);
            Items[1]._gameObject.transform.localPosition = Vector3.zero;
            Items[1]._gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            Items[1]._gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

            Items[1].rig.isKinematic = true;
            Items[1].coll.isTrigger = true;
            }else{
                //Do Nothing
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(Items.Count >= 2){
                foreach(ItemController item in Items){
                item.GetComponent<GameObject>().SetActive(false);
            }
            Items[2]._gameObject.SetActive(true);
            Items[2]._gameObject.transform.SetParent(_itemContainer);
            Items[2]._gameObject.transform.localPosition = Vector3.zero;
            Items[2]._gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            Items[2]._gameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));

            Items[2].rig.isKinematic = true;
            Items[2].coll.isTrigger = true;
            }else{
                //Do Nothing
            }
        }     
    }

    public void ShowItemImg(ItemController item)
    {
        for (int i = 0; i < _itemSlot.Length; i++)
        {
            if (_itemSlot[i].GetComponent<Slot>().isItemIn == false)
            {
                _itemSlot[i].GetComponent<Slot>().isItemIn = true;
                _itemSlot[i].SetActive(true);
                _itemSlot[i].GetComponent<Image>().sprite = item.itemImg;
                _itemLife[i].SetActive(true); 
                _itemLife[i].GetComponent<Text>().text = item.itemHealth.ToString();
                break;
            }
        }
    }

    public void CheckItemHealth(ItemController item)
    {
        for (int i = 0; i < _itemSlot.Length; i++)
        {
            _itemLife[i].GetComponent<Text>().text = item.itemHealth.ToString();
        }
    }

    public void CheckFullSlot()
    {
        int count = 1;
        for (int i = 0; i < _itemSlot.Length; i++)
        {
            if (_itemSlot[i].GetComponent<Slot>().isItemIn == true)
            {
                count++;
            }
        }

        if (count == _itemSlot.Length)
        {
            _AllslotFull = true;
        }
        else
        {
            _AllslotFull = false;
        }
    }

    public void DeleteItemImg(ItemController item)
    {
        for (int i = 0; i < _itemSlot.Length; i++)
        {
            if (_itemSlot[i].GetComponent<Slot>().isItemIn == true && GetComponent<PickUpItem>().isItemDestory == true)
            {
                _itemSlot[i].SetActive(false);
                break;
            }
        }
    }
}
