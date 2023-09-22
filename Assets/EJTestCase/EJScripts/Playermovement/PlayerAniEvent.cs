using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    
    void AttackStart()
    {
        transform.localEulerAngles += new Vector3(transform.localEulerAngles.x, 90 , transform.localEulerAngles.z );
    }

    void AttackAniFin()
    {
        GetComponent<Animator>().SetLayerWeight(1, 1);
        transform.localEulerAngles += new Vector3(transform.localEulerAngles.x, -90, transform.localEulerAngles.z);
    }
}
