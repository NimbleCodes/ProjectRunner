using UnityEngine;


public class UIController : MonoBehaviour
{
    [SerializeField] GameObject _settingsPanel;
    [SerializeField] GameObject _gameoverPanel;
    [SerializeField] GameObject _howToPlay;
    [SerializeField] GameObject _creadits;
    [SerializeField] GameObject _gameClear;
    [SerializeField] AudioSource _BGM;
    [SerializeField] AudioClip _gameClearSound;
    bool settingsOpen = false;
    bool _isGameClear = false;
    public static UIController Instance;

    private void Start() {
        _settingsPanel.SetActive(false);
        _gameoverPanel.SetActive(false);
        _howToPlay.SetActive(false);
        _creadits.SetActive(false);
        _gameClear.SetActive(false);

        if(PlayerPrefs.GetInt("isHowToPlayShown") == 0){
            _howToPlay.SetActive(true);
            PlayerPrefs.SetInt("isHowToPlayShown", 1);
        }
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
        OpenGameClear(_isGameClear);

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
    public bool GetOpenner(){
        return settingsOpen;
    }

    public void OnHowToPlayButtonClick(){
        _howToPlay.SetActive(true);        
    }

    public void OnCreaditsButtonClick(){
        _creadits.SetActive(true);
    }
    public void OnHowToPlayCloseButtonClick(){
        if(settingsOpen == false){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        _howToPlay.SetActive(false);
    }

    public void OnCreaditsCloseButtonClick(){
        _creadits.SetActive(false);
    }

    void OpenGameClear(bool clear){
        if(clear == true){
            //Time.timeScale =0;
            _gameClear.SetActive(true);
            _BGM.clip = _gameClearSound;
            _BGM.Play();
        }
    }
    public void IsGameClear(bool isit){
        _isGameClear = isit;
    }

}
