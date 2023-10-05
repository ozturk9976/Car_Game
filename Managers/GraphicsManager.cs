using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

//This class changes current settings 
//Check SettingsAPI for further settings
public class GraphicsManager : MonoBehaviour
{

    public static GraphicsManager instance;
    public UniversalRenderPipelineAsset urp;


    private void Awake()
    {
        instance = this;
    }

    public void ChangeShadowDistance(string shadowDistance)
    {
        switch (shadowDistance)
        {
            case "low":
                urp.shadowDistance = 13;
                PlayerPrefs.SetInt("URPRenderScale", 13);
                break;
            case "mid":
                urp.shadowDistance = 18;
                PlayerPrefs.SetInt("URPRenderScale", 18);
                break;
            case "high":
                urp.shadowDistance = 25;
                PlayerPrefs.SetInt("URPRenderScale", 25);
                break;
        }
    }
    public void ChangeResulotionScale(string resulotionScale)
    {
        var cameraData = Camera.main.GetUniversalAdditionalCameraData();
        switch (resulotionScale)
        {
            case "low":
                urp.renderScale = 0.70f;
                PlayerPrefs.SetFloat("URPRenderScale", 0.70f);
                break;
            case "mid":
                urp.renderScale = 0.85f;
                PlayerPrefs.SetFloat("URPRenderScale", 0.85f);
                break;
            case "high":
                urp.renderScale = 1;
                PlayerPrefs.SetFloat("URPRenderScale", 1f);
                break;
        }
    }
    // public void ChangeAntialiasingStatus(string status)
    // {
    //     var cameraData = Camera.main.GetUniversalAdditionalCameraData();
    //     switch (status)
    //     {
    //         case "On":
    //             cameraData.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
    //             PlayerPrefs.SetString("AntialiasingMode", "On");
    //             break;
    //         case "Off":
    //             cameraData.antialiasing = AntialiasingMode.None;
    //             PlayerPrefs.SetString("AntialiasingMode", "Off");
    //             break;
    //     }
    // }

    // public void ChangeMaxFPS(int fps)
    // {
    //     Application.targetFrameRate = fps;
    //     PlayerPrefs.SetInt("FPS", fps);
    // }

}
