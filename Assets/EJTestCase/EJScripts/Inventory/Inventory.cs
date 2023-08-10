using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;  
    public List<ItemList> Items = new List<ItemList>();
    [SerializeField] SpriteRenderer[] _itemSlot;
    float alpha = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemList item)
    {
        if(Items.Count < 3)
        {
            Items.Add(item);
        }
    }

    public void Remove(ItemList item)
    {
        Items.Remove(item);
    }

    public void ShowItemImg()
    {
        if (GetComponent<PickUpItem1>().equipped == true)
        {
            for (int i = 0; i < _itemSlot.Length; i++)
            {
                Color color = _itemSlot[i].color;
                _itemSlot[i].color = new Color(color.r, color.g, color.b, alpha / 255);
                _itemSlot[i].sprite = GetComponent<ItemList>().itemImage; 
            }
        }
    }
}
