using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    float deltaTime = 0f;
    float lastFrame =0;
    float currentFrame =0;
    float myDelta =0;

    private void Reset() {
        lastFrame = currentFrame = Time.realtimeSinceStartup;
        myDelta = Time.smoothDeltaTime;
    }

    private void Update() {
        currentFrame = Time.realtimeSinceStartup;
        myDelta = currentFrame - lastFrame;
        myDelta *= Time.timeScale;
        lastFrame = currentFrame;

    }

    private void LateUpdate() {
        deltaTime = (Time.deltaTime + Time.smoothDeltaTime + myDelta) * 0.3333f;
    }
}
