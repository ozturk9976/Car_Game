using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    public string initScene;
    SceneInstance previousLoadedScene;

    private bool operationCompleted = false;
    float progressValue;
    UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<SceneInstance> operation;
    private void Start()
    {
        // Addressables.UnloadSceneAsync();
        Resources.UnloadUnusedAssets();
        Invoke(nameof(StartDelayed), 0.3f);
    }


    void StartDelayed()
    {
        DOTween.To(() => progressValue, x => progressValue = x, 1, 1f).SetEase(Ease.InOutQuint);

        if (PlayerPrefs.GetString("SceneToLoad") == "MainMenu")
        {
            StartCoroutine(LoadAdressableLevel("MainMenu"));
        }
        else
        {
            StartCoroutine(LoadAdressableLevel(initScene));
        }
    }

    private void Update()
    {
        progressBar.fillAmount = progressValue;
        if (progressBar.fillAmount == 1f && operationCompleted)
        {
            operation.Result.ActivateAsync();
        }
        else
        {
            return;
        }
    }




    private IEnumerator LoadAdressableLevel(string addressableKey)
    {

        operation = Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Single, false);
        operation.Completed += (AsyncOperationHandle) =>
        {
            operationCompleted = true;
        };
        yield return operation;

    }



}
