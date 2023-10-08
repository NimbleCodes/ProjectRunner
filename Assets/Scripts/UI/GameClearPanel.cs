using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearPanel : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Return)){
            Time.timeScale =1f;
            SceneManager.LoadScene("Home");
        }
    }
}
