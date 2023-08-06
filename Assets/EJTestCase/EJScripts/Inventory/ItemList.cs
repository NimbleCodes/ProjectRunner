using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public enum ItempType
    {
        Laptop,
        Chair,
        Plant,
        Holepunch,
        Calculator,
        ClipBoard, 
    }

    public ItempType itemtype;
    public Sprite itemImage; 

}
