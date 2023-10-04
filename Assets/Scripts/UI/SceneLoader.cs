using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Text loadingText;
    UnityEngine.AsyncOperation asyncScene = new UnityEngine.AsyncOperation();

    DummyWrapper wrapper = new DummyWrapper();
    string loading ="Loading......";
    bool loaded = false;
    int stringCount =0;

    private void Start() {
        StartCoroutine(LoadScene("KSUmap01"));
    }
    private void Update() {
        if(Input.anyKeyDown && loaded == true){
            asyncScene.allowSceneActivation = true;
            loaded = false;
        }
    }

    IEnumerator LoadScene(string sceneName){
        asyncScene = SceneManager.LoadSceneAsync("KSUMap01");
        AsyncLoadData();    
        asyncScene.allowSceneActivation =false;
        while(loaded == false){
            yield return new WaitForSeconds(0.1f);
            loadingText.text += loading[stringCount];
            stringCount++;
            if(stringCount >= loading.Length){
                loadingText.text = "";
                stringCount =0;
            }
        }
        loadingText.text = "-press anykey to Start-";

    }

    async void AsyncLoadData(){
        string json;
        using(StreamReader rd = new StreamReader("Assets/Resources/TestCase/Json/dummyData.json")){
            json =await rd.ReadToEndAsync();//await is to make work async
        }                                   //also await is needed to null check
                                            //without calling false thousand times
        if(string.IsNullOrEmpty(json) == false){
            await Task.Run(() => {
                wrapper = JsonUtility.FromJson<DummyWrapper>(json);
            });
        }
        loaded = true;
    }

    // void OnEnable()
    // {
    //     StartCoroutine(LoadScene("KSUmap01"));
    // }
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