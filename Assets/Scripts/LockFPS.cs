using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockFPS : MonoBehaviour
{
    [SerializeField] bool lockFps = false;
    [SerializeField] int lockFpsTo = 60;
    void Awake() {
        QualitySettings.vSyncCount = 0;

        if (lockFps == true)
        {
            Application.targetFrameRate = lockFpsTo;
        } else 
        {
            return;
        }
    }
}
