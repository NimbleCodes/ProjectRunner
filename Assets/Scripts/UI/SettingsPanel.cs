using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] Toggle _BGMOff;
    [SerializeField] Slider _mouseControll;
    [SerializeField] Slider _soundControll;
    [SerializeField] AudioSource _bgm;
    [SerializeField] UIController _UIContorll;
    

    FreeCam _fc;
    float _currentVolume = 1f;

    
    private void Start() 
    {
        _fc = GameObject.Find("CamPoint").GetComponent<FreeCam>();
        AudioListener.volume = PlayerPrefs.GetFloat("BGMVolume");
        _soundControll.value = PlayerPrefs.GetFloat("BGMVolume");
        _fc.ChangeCamSpeed(PlayerPrefs.GetFloat("SensitivityVal"));
        
        if(PlayerPrefs.GetInt("isBGMOff") == 1){
            AudioListener.pause = true;
            _BGMOff.isOn = true;
        }else{
            AudioListener.pause = false;
            _BGMOff.isOn = false;
        }
    }

    public void OnMouseControllChange(float value)
    {
        _fc.ChangeCamSpeed(value);
        PlayerPrefs.SetFloat("SensitivityVal", value);
    }

    public void OnSoundControllChange(float value)
    {
        AudioListener.volume = value;
        _currentVolume = AudioListener.volume;
        PlayerPrefs.SetFloat("BGMVolume", _currentVolume);
    }

    public void OnExitButtonClick()
    {
        PlayerPrefs.SetInt("isHowToPlayShown", 0);
        Application.Quit();
    }

    public void OnResumekButtonClick()
    {
       _UIContorll.SetOpenner(false);
    }

    public void OnMainMenuButtonClick()
    {
        _UIContorll.SetOpenner(false);
        
        SceneManager.LoadScene("Home");
    }

    public void OnBGMToggleClick(bool mute)
    {
        if(mute == true)
        {
            AudioListener.pause = true;
            PlayerPrefs.SetInt("isBGMOff", 1);
        }
        else
        {
            AudioListener.pause = false;
            PlayerPrefs.SetInt("isBGMOff",0);
        }
    }
}
