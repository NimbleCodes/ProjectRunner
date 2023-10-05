using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanelController : MonoBehaviour
{
    AudioSource _bgm;
    GameObject _playerRes;
    GameObject _player;
    HealthManager _healthManager;
    private void Start() {
        _bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        _playerRes = GameObject.FindGameObjectWithTag("ResPoint");
        _healthManager = GameObject.FindGameObjectWithTag("PlayerHolder").GetComponent<HealthManager>();
        _player = GameObject.FindGameObjectWithTag("PlayerHolder");
    }
    public void OnRestartkButtonClick(){
        Inventory.Instance.ResetItem();
        _bgm.clip = null;
        _player.SetActive(false);
        _playerRes.GetComponent<PlayerRes>()._isDead = false;
        _playerRes.GetComponent<PlayerRes>()._isRespwan = true;
        _healthManager.SetHealth();
        _playerRes.GetComponent<PlayerRes>()._isPannelClose = true;
        gameObject.SetActive(false);
    }

    public void RestartTest(){
        gameObject.SetActive(false);
        SceneManager.LoadScene("KSUMap01");
    }

    public void OnMainMenuButtonClick(){
        gameObject.SetActive(false);
        SceneManager.LoadScene("Home");
    }
}
