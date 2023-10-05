using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    void AttackAniFin()
    {
        GetComponent<Animator>().SetLayerWeight(2, 1f);
    }
}
