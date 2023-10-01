using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Text loadingText;
    AsyncOperation asyncScene = new AsyncOperation();
    string loading ="Loading......";
    int stringCount =0;

    private void Start() {
        StartCoroutine(LoadScene("KSUmap01"));
    }
    private void Update() {
        if(Input.anyKeyDown){
            asyncScene.allowSceneActivation = true;
        }
    }

    IEnumerator LoadScene(string sceneName){
        asyncScene = SceneManager.LoadSceneAsync("KSUMap01");
        asyncScene.allowSceneActivation =false;
        while(!asyncScene.isDone){
            yield return new WaitForSeconds(0.1f);
            loadingText.text += loading[stringCount];
            stringCount++;
            if(stringCount >= loading.Length){
                loadingText.text = "";
                stringCount =0;
            }
            
            if(asyncScene.progress >= 0.9f){
                loadingText.text = "-press anykey to Start-";
                if(Input.anyKeyDown){
                    asyncScene.allowSceneActivation = true;
                }
            }
        }

    }
}
