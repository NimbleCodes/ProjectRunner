using UnityEngine;

public class BossRoomEntry : MonoBehaviour
{
    [SerializeField] Animator anim;
    
    private void OnTriggerEnter(Collider other)
    {        
        if(other.name == "Player")
        {
            anim.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            anim.SetBool("Open", false);
        }
    }
}
