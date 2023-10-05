using UnityEngine;


public class UIController : MonoBehaviour
{
    [SerializeField] GameObject _settingsPanel;
    [SerializeField] GameObject _gameoverPanel;
    [SerializeField] GameObject _howToPlay;
    [SerializeField] GameObject _creadits;
    bool settingsOpen = false;
    public static UIController Instance;
    private void Awake() {
        _settingsPanel.SetActive(false);
        _gameoverPanel.SetActive(false);
        _howToPlay.SetActive(false);
        _creadits.SetActive(false);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(settingsOpen == false){
                settingsOpen = true;
            }else{
                settingsOpen = false;
            }
        }

        SettingsOpen(settingsOpen);
    }
    void SettingsOpen(bool openner){
        if(openner == true){
            Time.timeScale = 0f;
            _settingsPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }else{
            _settingsPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }
    public void OpenGameOver()
    {
        _gameoverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void SetOpenner(bool openner){
        settingsOpen = openner;
    }

    public void OnHowToPlayButtonClick(){
        _howToPlay.SetActive(true);        
    }

    public void OnCreaditsButtonClick(){
        _creadits.SetActive(true);
    }
    public void OnHowToPlayCloseButtonClick(){
        _howToPlay.SetActive(false);
    }

    public void OnCreaditsCloseButtonClick(){
        _creadits.SetActive(false);
    }

}
