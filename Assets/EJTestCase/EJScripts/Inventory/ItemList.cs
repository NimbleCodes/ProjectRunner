using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemList : ScriptableObject
{ 
    public Sprite itemImage; // 아이템 이미지 
    public GameObject itemPrefab; // 아이템 프리팹
}
