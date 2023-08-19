using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemData : MonoBehaviour
{
    private static ItemData instance = null;
    System.Random _rand;
    public ItemPool itemPool{get;set;}
    public Dictionary<string , GameObject> objPools = new Dictionary<string, GameObject>();

    public GameObject testObj;
    void Awake(){
        LoadDataFromJson();
        if(instance == null){
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }else{
            Destroy(this.gameObject);
        }
        
    }

    public static ItemData Instance{ //인스턴스 접근 property
        get{
            if(instance == null){
                return null;
            }
            return instance;
        }
    }

    //Json파일에서 아이템 데이터목록 읽어오기
    void LoadDataFromJson(){
        TextAsset dataJson = Resources.Load("TestCase/Json/ItemData") as TextAsset;
        itemPool = JsonUtility.FromJson<ItemPool>(dataJson.ToString());

        foreach(ItemObjects data in itemPool.itemObjects){
            GameObject temp = Resources.Load("Weaponprefabs/" + data.ItemName) as GameObject;
            objPools.Add(data.ItemName, temp);
        }

        testObj = objPools["ClipBoard"];
    }

    //랜덤으로 아이템 생성을 위한 랜덤번호 생성
    public int GetRandomNumb(int arrLength){
        int rNumber = _rand.Next(0,arrLength);
        return rNumber;
    }
}

[Serializable]
public class ItemObjects{ //Item Data 정보를 저장할 클래스 생성
    public string ItemName;
    public string Description;
    public string UseCount;
}

[Serializable]
public class ItemPool{
    public ItemObjects[] itemObjects;
}