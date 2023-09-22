using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    void AttackStart()
    {
        this.transform.rotation += Quaternion.Euler(0f, -70f, 0f);
    }

    void AttackAniFin()
    {
        GetComponent<Animator>().SetLayerWeight(1, 1);
        this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
