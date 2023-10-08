using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [SerializeField] GameObject _BossHP;
    [SerializeField] GameObject _Boss;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _BossNamePanel;
    [SerializeField] CinemachineVirtualCamera _vcam1;
    [SerializeField] CinemachineFreeLook _freeCam;
    PlayerInBossMap _BossMapInit;
    bool _triggered= false;

    private void Start()
    {
        _BossMapInit = GetComponent<PlayerInBossMap>();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(_triggered == false){
            _BossHP.SetActive(true);
            _BossMapInit.InitBossStage();
            _BossMapInit._isTriggered = false;

            StartCoroutine(BossStageCinematic());
            _triggered = true;
        }
    }

    IEnumerator BossStageCinematic(){
        Time.timeScale = 0f;

        _freeCam.Priority =0;
        _vcam1.Priority = 10;

        yield return new WaitForSeconds(2f);

        _BossNamePanel.GetComponent<Animator>().SetBool("BossNameOn", true);

        yield return new WaitForSeconds(2.5f);

        _vcam1.Priority = 0;
        _freeCam.Priority = 10;
        _BossNamePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
