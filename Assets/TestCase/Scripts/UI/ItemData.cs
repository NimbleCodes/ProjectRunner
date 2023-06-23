using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    protected ItemPool _itemPool;
    System.Random _rand;
    //public static ItemData _items;
    void Awake(){
        LoadDataFromJson();
        
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


public class ItemObjects{ //Item Data 정보를 저장할 클래스 생성
    string ItemName;
    string Description;
    string UseCount;
}


public class ItemPool{
    public ItemObjects[] itemPool;
}