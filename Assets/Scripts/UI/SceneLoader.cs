using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Text loadingText;
    [SerializeField] Text ExitText;
    UnityEngine.AsyncOperation asyncScene = new UnityEngine.AsyncOperation();

    DummyWrapper wrapper = new DummyWrapper();
    string loading ="Loading......";
    bool loaded = false;
    int stringCount =0;
    string json;

    private void Start() {
        StartCoroutine(LoadScene("KSUmap01"));
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        else if(Input.anyKeyDown && loaded == true){
            asyncScene.allowSceneActivation = true;
            loaded = false;
        }
    }
    bool _loadEnd = false;  
    bool _readStart = false;
    IEnumerator LoadScene(string sceneName){
        AsyncLoadData();  

        asyncScene = SceneManager.LoadSceneAsync("KSUMap01");
        asyncScene.allowSceneActivation =false;
        
        while(loaded == false){
            yield return new WaitForSeconds(0.1f);
            if(_loadEnd == true && _readStart == false){
                _readStart = true;
                jsonRead();
            }
            loadingText.text += loading[stringCount];
            stringCount++;
            if(stringCount >= loading.Length){
                loadingText.text = "";
                stringCount =0;
            }
        }
        loadingText.text = "-press anykey to Start-";
        ExitText.text = "-ESC to Quit Game-";

    }

    void AsyncLoadData(){
        ResourceRequest rq = Resources.LoadAsync("TestCase/Json/dummyData");
        
        rq.completed += (op) =>
        {
            Debug.Log("loading " + loaded);
            json = ((TextAsset)rq.asset).text;
            _loadEnd = true;
        };
    }

    async void jsonRead()
    {
        await Task.Run(() => {
            Debug.Log("writting");
            if(string.IsNullOrEmpty(json) == false){
                wrapper = JsonUtility.FromJson<DummyWrapper>(json);   
                loaded = true;
                Debug.Log("json");
            }
        }); 

    }
}

[Serializable]
public class DummyWrapper{
    public List<DummyData> _datas = new List<DummyData>();
}
[Serializable]
public class DummyData{
    public double dValue;
    public int iValue;
}