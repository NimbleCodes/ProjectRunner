using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

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
