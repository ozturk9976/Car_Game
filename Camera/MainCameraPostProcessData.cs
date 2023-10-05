using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MainCameraPostProcessData : MonoBehaviour
{
    void Start()
    {
        string _currentAntiAliasingMode = PlayerPrefs.GetString("AntialiasingMode", "On");
        var cameraData = Camera.main.GetUniversalAdditionalCameraData();


        switch (_currentAntiAliasingMode)
        {
            case "On":
                cameraData.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
                break;
            case "Off":
                cameraData.antialiasing = AntialiasingMode.None;
                break;
        }
    }

}
