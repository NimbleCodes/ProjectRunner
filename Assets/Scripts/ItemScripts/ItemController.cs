using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject _gameObject;
    public GameObject _grabPoint; 
    public Sprite itemImg;
    public int itemHealth; 
    public Collider coll;
    public Rigidbody rig;
    [SerializeField] float _rotateX;
    [SerializeField] float _rotateY;
    [SerializeField] float _rotateZ; 

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

    public Vector3 GetRotation()
    {
        return new Vector3(_rotateX,_rotateY,_rotateZ);
    }
}
