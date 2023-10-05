using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
// using UnityEditor.AddressableAssets.Settings;
using UnityEngine.AddressableAssets.Initialization;
using UnityEngine.AddressableAssets.ResourceLocators;
using Unity.Mathematics;
using DG.Tweening;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InitializeScene : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    float progressValue;
    UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<SceneInstance> operation;

    private void Start()
    {
        StartCoroutine(LoadAdressableMainMenu("MainMenu"));
    }
    private void Update()
    {
        progressBar.fillAmount = progressValue;
    }

    private IEnumerator LoadAdressableMainMenu(string adressableKey)
    {
        operation = Addressables.LoadSceneAsync(adressableKey, LoadSceneMode.Single, false);
        operation.Completed += (AsyncOperationHandle) => { NextScene(); };
        yield return null;
    }

    private void NextScene()
    {

        DOTween.To(() => progressValue, x => progressValue = x, 1, 1.5f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            operation.Result.ActivateAsync();
        });

    }
}
