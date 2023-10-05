using UnityEngine;

public class ItemHealthManager : MonoBehaviour
{
    int itemHealth;

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
