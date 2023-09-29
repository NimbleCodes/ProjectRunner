using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] GameObject[] _itemSlot;
    [SerializeField] GameObject[] _itemLife;
    [SerializeField] Transform _WeaponPoint; 
    public bool _AllslotFull = false;
    private bool _isEquipped = false;
    private float x, y, z;
    public List<ItemController> Items = new List<ItemController>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CheckFullSlot();
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
           if(Items != null)
            {
                foreach(ItemController item in Items)
                {
                    item._gameObject.SetActive(false);
                }
                if (_isEquipped == false)
                {
                    Items[0]._gameObject.SetActive(true);
                    Items[0]._grabPoint.transform.SetParent(_WeaponPoint);
                    Items[0]._grabPoint.transform.localPosition = Vector3.zero;
                    Vector3 v3 = Items[0].GetComponent<ItemController>().GetRotation();
                    Items[0]._grabPoint.transform.localRotation = Quaternion.Euler(v3);

                    Items[0].rig.isKinematic = true;
                    Items[0].coll.isTrigger = true;
                    _isEquipped = true;
                }
                else if (_isEquipped == true)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        Items[i]._gameObject.SetActive(false);
                    }

                    Items[0]._gameObject.SetActive(true);
                    Items[0]._grabPoint.transform.SetParent(_WeaponPoint);
                    Items[0]._grabPoint.transform.localPosition = Vector3.zero;
                    Vector3 v3 = Items[0].GetComponent<ItemController>().GetRotation();
                    Items[0]._grabPoint.transform.localRotation = Quaternion.Euler(v3);

                    Items[0].rig.isKinematic = true;
                    Items[0].coll.isTrigger = true;
                    _isEquipped = true;
                }
           }
           else
           {
                //Do Nothing
           }
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(Items.Count >= 2)
            {
                foreach(ItemController item in Items)
                {
                    item._gameObject.SetActive(false);
                }
                if (_isEquipped == false)
                {
                    Items[1]._gameObject.SetActive(true);
                    Items[1]._grabPoint.transform.SetParent(_WeaponPoint);
                    Items[1]._grabPoint.transform.localPosition = Vector3.zero;
                    Vector3 v3 = Items[1].GetComponent<ItemController>().GetRotation();
                    Items[1]._grabPoint.transform.localRotation = Quaternion.Euler(v3);

                    Items[1].rig.isKinematic = true;
                    Items[1].coll.isTrigger = true;
                    _isEquipped = true;
                }
                else if (_isEquipped == true)
                {
                    for(int i =0; i < Items.Count; i++)
                    {
                        Items[i]._gameObject.SetActive(false);
                    }

                    Items[1]._gameObject.SetActive(true);
                    Items[1]._grabPoint.transform.SetParent(_WeaponPoint);
                    Items[1]._grabPoint.transform.localPosition = Vector3.zero;
                    Vector3 v3 = Items[1].GetComponent<ItemController>().GetRotation();
                    Items[1]._grabPoint.transform.localRotation = Quaternion.Euler(v3);

                    Items[1].rig.isKinematic = true;
                    Items[1].coll.isTrigger = true;
                    _isEquipped = true;
                }
            }
            else
            {
                //Do Nothing
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if(Items.Count >= 2)
            {
                foreach(ItemController item in Items)
                {
                    item._gameObject.SetActive(false);
                }
                if (_isEquipped == false)
                {
                    Items[2]._gameObject.SetActive(true);
                    Items[2]._grabPoint.transform.SetParent(_WeaponPoint);
                    Items[2]._grabPoint.transform.localPosition = Vector3.zero;
                    Vector3 v3 = Items[2].GetComponent<ItemController>().GetRotation();
                    Items[2]._grabPoint.transform.localRotation = Quaternion.Euler(v3);

                    Items[2].rig.isKinematic = true;
                    Items[2].coll.isTrigger = true;
                    _isEquipped = true;
                }
                else if (_isEquipped == true)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        Items[i]._gameObject.SetActive(false);
                    }

                    Items[2]._gameObject.SetActive(true);
                    Items[2]._grabPoint.transform.SetParent(_WeaponPoint);
                    Items[2]._grabPoint.transform.localPosition = Vector3.zero;
                    Vector3 v3 = Items[2].GetComponent<ItemController>().GetRotation();
                    Items[2]._grabPoint.transform.localRotation = Quaternion.Euler(v3);

                    Items[2].rig.isKinematic = true;
                    Items[2].coll.isTrigger = true;
                    _isEquipped = true;
                }
            }
            else
            {
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

    public void ShowAllItemImg(List<ItemController> items)
    {
        for(int i =0; i < items.Count; i++)
        {
            _itemSlot[i].GetComponent<Slot>().isItemIn = true;
            _itemSlot[i].SetActive(true);
            _itemSlot[i].GetComponent<Image>().sprite = items[i].itemImg;
            _itemLife[i].SetActive(true);
            _itemLife[i].GetComponent<Text>().text = items[i].itemHealth.ToString();
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
                    for (int j = 0; j < _itemSlot.Length; j++)
                    {
                        _itemSlot[j].SetActive(false);
                        _itemLife[j].SetActive(false);
                        Items.Remove(item);
                        _itemSlot[j].GetComponent<Slot>().isItemIn = false;
                        _isEquipped = false;
                    }
                    ShowAllItemImg(Items);
                }
            }
        }
    }

    public void ResetItem()
    {
        Items.Clear();
        for (int i = 0; i < _itemSlot.Length; i++)
        {
            _itemSlot[i].SetActive(false);
            _itemLife[i].SetActive(false);
            _itemSlot[i].GetComponent<Slot>().isItemIn = false;
            _isEquipped = false;
        }
    }

    public void CheckFullSlot()
    {
        if (Items.Count >= 3)
        {
            _AllslotFull = true;
        }
        else
        {
            _AllslotFull = false;
        }
    }
}
