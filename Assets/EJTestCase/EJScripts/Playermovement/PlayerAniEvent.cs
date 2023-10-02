using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    void AttackAniFin()
    {
        GetComponent<Animator>().SetLayerWeight(2, 1f);
        transform.localEulerAngles += new Vector3(transform.localEulerAngles.x, -90, transform.localEulerAngles.z);
    }
}
