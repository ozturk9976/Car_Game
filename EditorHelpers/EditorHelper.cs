using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class EditorHelper : MonoBehaviour
{

#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Editor_Mode_On()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "InitializeScene" || SceneManager.GetActiveScene().name == "SceneLoader")
        {
            return;
        }
        else
        {
            if (GameObject.FindAnyObjectByType<SceneChangeData_DDOL>() == null)
            {

                Debug.Log("Editor Mode On");

                var defaultPrefabs = Addressables.InstantiateAsync("Theme Default Prefabs");
                defaultPrefabs.Completed += (AsyncOperationHandle) => { GameManager.instance.InitializeLevelData(); };

                var ddol = Addressables.InstantiateAsync("SceneChangeData");
                ddol.Completed += (AsyncOperationHandle) => { DontDestroyOnLoad(ddol.Result.gameObject); };
            }
        }
    }
#endif

}
