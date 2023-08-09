using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemList : ScriptableObject
{ 
    public Sprite itemImage; // ������ �̹��� 
    public GameObject itemPrefab; // ������ ������
}
