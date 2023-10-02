using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] Slider mouseControll;
    [SerializeField] Slider soundControll;
    FreeCam fc;
    private void Start() {
        fc = GameObject.Find("CamPoint").GetComponent<FreeCam>();
    }

    void OnMouseControllChange(float value){
        fc.ChangeCamSpeed(value);
    }

    void OnSoundControllChange(float value){
        
    }

    void OnExitButtonClick(){
        Application.Quit();
    }

    void OnMainMenuButtonClick(){
        SceneManager.LoadScene("Home");
    }
}
