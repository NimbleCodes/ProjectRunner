using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject _gameObject; 
    public Sprite itemImg;
    public int itemHealth; 
    public Collider coll;
    public Rigidbody rig;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            itemHealth -= 1;
            Inventory.Instance.ReduceItemHP(this);
            if (itemHealth == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
