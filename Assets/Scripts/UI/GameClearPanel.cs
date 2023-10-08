using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearPanel : MonoBehaviour
{
    private void Update() {
        if(Input.anyKey){
            Time.timeScale =1f;
            SceneManager.LoadScene("Home");
        }
    }
}
