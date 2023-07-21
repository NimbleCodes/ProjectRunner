using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRemain : MonoBehaviour
{
    public static CamRemain instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
