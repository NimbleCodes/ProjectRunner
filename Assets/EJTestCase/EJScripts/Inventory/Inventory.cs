using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] GameObject[] _itemSlot;
    [SerializeField] GameObject[] _itemLife;
    [SerializeField] Transform _itemContainer;
    private bool _AllslotFull = false;
    private bool _isEquipped = false; 
    public List<ItemController> Items = new List<ItemController>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // CheckFullSlot();
        SwapItem();
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
        if (Input.GetKeyDown(KeyCode.Alpha1) && _isEquipped == false)
        {
           if(Items != null){
            foreach(ItemController item in Items){
                item._gameObject.SetActive(false);
            }
            _isEquipped = true; 
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
        if (Input.GetKeyDown(KeyCode.Alpha2) && _isEquipped == false)
        {
            if(Items.Count >= 2){
                foreach(ItemController item in Items){
                item.GetComponent<GameObject>().SetActive(false);
            }
            _isEquipped = true; 
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
        if (Input.GetKeyDown(KeyCode.Alpha3) && _isEquipped == false)
        {
            if(Items.Count >= 2){
                foreach(ItemController item in Items){
                item.GetComponent<GameObject>().SetActive(false);
            }
            _isEquipped = true; 
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

    public void ReduceItemHP(ItemController item)
    {
        int itemHP; 
        for(int i = 0; i < Items.Count; i++)
        {
            if (Items[i] == item)
            {
                _itemLife[i].GetComponent<Text>().text = item.itemHealth.ToString();
                itemHP = item.itemHealth;

                if (itemHP <= 0)
                {
                    _itemSlot[i].SetActive(false);
                    _itemLife[i].SetActive(false);
                    Items.Remove(item);
                    _itemSlot[i].GetComponent<Slot>().isItemIn = false;
                    _isEquipped = false;
                    // 1번 아이템이 삭제 되었을 경우 2번, 3번 아이템이 1번, 2번 자리로 
                    // 2번 아이템이 삭제 되었을 경우 3번 아이템이 2번 자리로 
                    if (_itemSlot[0].GetComponent<Slot>().isItemIn == false)
                    {

                    }
                    else if(_itemSlot[1].GetComponent<Slot>().isItemIn == false)
                    {

                    }
                    else if (_itemSlot[2].GetComponent<Slot>().isItemIn == false)
                    {

                    }
                }
            }
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
}
