using UnityEngine;

public class HowToPlayPanel : MonoBehaviour
{
    [SerializeField] GameObject _uiController;
    bool isSettingsOpen;

    void Update()
    {
        TurnMouseOn();
    }

    void TurnMouseOn(){
        isSettingsOpen = _uiController.GetComponent<UIController>().GetOpenner();

        if(isSettingsOpen == false){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
