using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] GameObject _BossHP;

    private void OnTriggerEnter(Collider other) {
        _BossHP.SetActive(true);
    }
}
