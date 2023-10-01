using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIController : MonoBehaviour
{
    [SerializeField] GameObject _settingsPanel;
    [SerializeField] GameObject _gameoverPanel;
    bool settingsOpen = false;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(settingsOpen == false){
                settingsOpen = true;
                SettingsControll(true);
            }else{
                settingsOpen = false;
                SettingsControll(false);
            }
        }
    }
    void SettingsControll(bool openner){
        if(openner == true){
            _settingsPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }else{
            _settingsPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

}
