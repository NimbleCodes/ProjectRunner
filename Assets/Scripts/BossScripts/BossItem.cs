using UnityEngine;

public class BossItem : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Ground")
        {
            GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 4f);
        }
    }
}
