using System.Reflection;
using UnityEngine;

public class PlayerAniEvent : MonoBehaviour
{
    public bool _isSwing; 

    void ISswingStart()
    {
        _isSwing = true;
    }

    void AttackAniFin()
    {
        GetComponent<Animator>().SetLayerWeight(2, 1f);
        _isSwing = false; 
    }
}
