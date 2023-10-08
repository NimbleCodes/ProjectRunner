using UnityEngine;

public class PlayerPrefsHolder : MonoBehaviour
{
    public static PlayerPrefsHolder instance;
    private void Awake() {
        if(instance == null){
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
        /* 
        PlayerPrefs.SetInt("isHowToPlayShown", 0);
        PlayerPrefs.SetInt("isBGMOff", 0);
        PlayerPrefs.SetFloat("BGMVolume", 1f);
        */
    }

    private void Start() {
        Debug.Log(PlayerPrefs.GetInt("isBGMOff"));
    }
}
