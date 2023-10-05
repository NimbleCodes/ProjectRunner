using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] GameObject _BossHP;
    PlayerInBossMap _BossMapInit;

    private void Start()
    {
        _BossMapInit = GetComponent<PlayerInBossMap>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        _BossHP.SetActive(true);
        _BossMapInit.InitBossStage();
    }
}
