using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    [SerializeField] GameObject[] _itemSlot;
    public bool _AllslotFull = false;
    List<ItemController> Items = new List<ItemController>();

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

    public void ShowItemImg(ItemController item)
    {
        for (int i = 0; i < _itemSlot.Length; i++)
        {
            if (_itemSlot[i].GetComponent<Slot>().isItemIn == false)
            {
                _itemSlot[i].GetComponent<Slot>().isItemIn = true;
                _itemSlot[i].SetActive(true);
                _itemSlot[i].GetComponent<Image>().sprite = item.itemImg;
                break;
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

    public void DeleteItemImg(ItemController item)
    {
        for (int i = 0; i < _itemSlot.Length; i++)
        {
            if (_itemSlot[i].GetComponent<Slot>().isItemIn == true)
            {
                _itemSlot[i].GetComponent<Slot>().isItemIn = false;
                _itemSlot[i].SetActive(false);
                break;
            }
        }
    }
}
