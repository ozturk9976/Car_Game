using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Threading.Tasks;

public class LevelButtonController : MonoBehaviour
{
    [SerializeField] private Color32 unlockedColor;
    [SerializeField] private Image mainImage;
    public int levelIndex;
    public int themeIndex;
    public bool isLevelLocked;

    AsyncOperationHandle handle;
    [SerializeField] GameObject starContainer;
    public Image[] allStars;


    public void UnlockedButtonState()
    {
        mainImage.color = unlockedColor;
    }

    public void OnClick()
    {
        PlayerPrefs.SetString("SceneToLoad", "Theme" + themeIndex.ToString() + "_" + levelIndex.ToString());
        //Addressables.LoadSceneAsync("SceneLoader", LoadSceneMode.Single);
        SceneManager.LoadScene("SceneLoader");
    }



    public void CheckStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            allStars[i].gameObject.SetActive(true);
        }
    }
}
