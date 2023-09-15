using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    void AttackAniFin()
    {
        GetComponent<Animator>().SetLayerWeight(1, 1);
    }
}
