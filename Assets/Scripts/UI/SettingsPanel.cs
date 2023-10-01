using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] Slider mouseControll;
    [SerializeField] Slider soundControll;
    FreeCam fc;
    private void Start() {
        fc = GameObject.Find("CamPoint").GetComponent<FreeCam>();
    }

    void OnMouseControllChange(float value){
        fc.ChangeCamSpeed(value);
    }

    void OnSoundControllChange(float value){
        
    }
}
