using UnityEngine;

public class ItemHealthManager : MonoBehaviour
{
    public int itemHealth;

    private void Start()
    {
        //itemHealth = GetComponent<ItemController>().itemLife; 
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
