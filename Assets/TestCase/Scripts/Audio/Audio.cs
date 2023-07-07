using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            audioSource.Stop();
            audioSource.Play();
        }
        Debug.Log(audioSource.timeSamples);
        Debug.Log(audioSource.time);
    }
}
