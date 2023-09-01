using UnityEngine;

public class BossItem : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Ground")
        {
            Destroy(this.gameObject, 4f);
        }
    }
}
