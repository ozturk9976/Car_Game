using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class SettingsAPI : MonoBehaviour
{
    // PLAYER SETTINGS  OPTIMIZATION KISMINDAKI  PREBAKE COLLISION MESHES ACILDI
    // OPTİMİZE MESH DATA AÇILDI
    UniversalRenderPipelineAsset universalRenderPipelineAsset;
    private void Awake()
    {
        universalRenderPipelineAsset = Resources.Load<UniversalRenderPipelineAsset>("Assets/Resources/URP/UniversalRP.asset");
        Debug.Log(universalRenderPipelineAsset);
    }
    public static void ExternalStartWithSettings()
    {
        // PlayerPrefs.GetInt("URPRenderScale", 25);
        // PlayerPrefs.GetFloat("URPRenderScale", 1f);
        // Application.targetFrameRate = PlayerPrefs.GetInt("FPS", 60);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        // Physics.reuseCollisionCallbacks = true;
        // Application.targetFrameRate = 120;
    }
}
