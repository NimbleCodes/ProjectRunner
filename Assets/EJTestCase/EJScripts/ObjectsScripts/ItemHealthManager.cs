using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ItemHealthManager : MonoBehaviour
{
    public int itemHealth = 0;

    private void Start()
    {
        itemHealth = GetComponent<ItemController>().itemLife;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Enemy")
        {
            itemHealth -= 1;
            Debug.Log(itemHealth);
            if (itemHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
