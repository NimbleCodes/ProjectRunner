using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<ItemList> Items = new List<ItemList>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemList item)
    {
        if (Items.Count < 3)
        {
            Items.Add(item);
        }
    }

    public void Remove(ItemList item)
    {
        Items.Remove(item);
    }

}
