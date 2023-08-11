using System;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    private static ItemData instance = null;
    ItemPool _itemPool;
    System.Random _rand;
    public ItemPool itemPool{get{return _itemPool;}}
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
        _itemPool = JsonUtility.FromJson<ItemPool>(dataJson.ToString());
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