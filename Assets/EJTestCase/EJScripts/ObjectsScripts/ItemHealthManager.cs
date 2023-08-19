using UnityEngine;

public class ItemHealthManager : MonoBehaviour
{
    private int itemHealth = 0;

    private void Start()
    {
        itemHealth = GetComponent<ItemController>().itemLife;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Enemy")
        {
            itemHealth -= 1;
            if (itemHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
