using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement;
using DG.Tweening;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SceneInitilization : MonoBehaviour
{
    AsyncOperationHandle<SceneInstance> operation;
    [SerializeField] GameObject disableAfterInit;

    void Start()
    {
        if (PlayerPrefs.HasKey("SceneToLoad") && PlayerPrefs.GetString("SceneToLoad") != "MainMenu")
        {
            string scene = PlayerPrefs.GetString("SceneToLoad");
            StartCoroutine(LoadAdressableLevel(scene));
        }
        else
        {
            StartCoroutine(LoadAdressableLevel("MainMenu"));
        }
    }

    private IEnumerator LoadAdressableLevel(string adressableKey)
    {
        DeletePrevScene();
        operation = Addressables.LoadSceneAsync(adressableKey, LoadSceneMode.Additive, false);
        Addressables.ResourceManager.Acquire(operation);
        //Addressables.ResourceManager.Acquire(operation);
        operation.Completed += (obs) =>
        {

            operation.Result.ActivateAsync().completed += (os) =>

            {
                SceneChangeData_DDOL.instance.AddScene(operation.Result.Scene.name, operation);
                SetActiveSceneAndInitIt(operation.Result);
                disableAfterInit.SetActive(false);
            };


        };

        yield return operation;

    }

    private void SetActiveSceneAndInitIt(SceneInstance newScene)
    {
        SceneManager.SetActiveScene(newScene.Scene);
        GameManager.instance.InitializeLevelData();
    }


    private void DeletePrevScene()
    {
        if (PlayerPrefs.HasKey("PreviousScene"))
        {
            string previousScene = PlayerPrefs.GetString("PreviousScene");
            SceneChangeData_DDOL.instance.ClearPreviousScene(previousScene);
            Debug.Log("cleared level is" + previousScene);
            //Addressables.Release(operation);
        }
    }
}
