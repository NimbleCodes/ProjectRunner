using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class LoadDummyData : MonoBehaviour
{
    DummyWrapper wrapper = new DummyWrapper();
    

    void Start()
    {
        LoadDummyDatas();
    }
    void LoadDummyDatas()
    {
        string json;
        using (StreamReader rd = new StreamReader("Assets/Resources/TestCase/Json/ItemData"))
        {
            json = rd.ReadToEnd();
        }

        if (string.IsNullOrEmpty(json) == false)
        {
            wrapper = JsonUtility.FromJson<DummyWrapper>(json);
            Debug.Log($"Result of Data : {wrapper._datas[0].dValue}, {wrapper._datas[0].iValue}");
        }
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
