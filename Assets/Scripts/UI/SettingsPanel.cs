using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] Slider _mouseControll;
    [SerializeField] Slider _soundControll;
    [SerializeField] AudioSource _bgm;
    [SerializeField] UIController _UIContorll;

    Animator _anim;
    FreeCam _fc;
    float _currentVolume = 1f;
    private void Start() {
        _fc = GameObject.Find("CamPoint").GetComponent<FreeCam>();
        _anim = GetComponent<Animator>();
    }

    public void OnMouseControllChange(float value){
        _fc.ChangeCamSpeed(value);
    }

    public void OnSoundControllChange(float value){
        _bgm.volume = value;
        _currentVolume = _bgm.volume;
    }

    public void OnExitButtonClick(){
        Application.Quit();
    }

    public void OnResumekButtonClick(){
        //_anim.SetBool("Active",false);
       //UIController.Instance.SetOpenner(false);
       _UIContorll.SetOpenner(false);
    }

    public void OnMainMenuButtonClick(){
        SceneManager.LoadScene("Home");
    }

    public void OnBGMToggleClick(bool mute){
        if(mute == true){
            _bgm.mute = mute;
        }else{
            _bgm.mute = mute;
            _bgm.volume = _currentVolume;
        }
    }
}
