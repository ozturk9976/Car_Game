using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StarManager : MonoBehaviour
{
    public int inGameStarCount = 3;
    public int currentTotalStars;
    private string _currentSceneName;
    private string _currentThemeName;
    private int currentLevelStarData;
    public int currentThemeStarCount;
    private GlobalData globalData;
    public static StarManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void InitializeStarData()
    {
        _currentSceneName = SceneManager.GetActiveScene().name;

        _currentThemeName = DataManager.Get_ThemeName;

        currentTotalStars = GlobalDataManager.instance._globalData.TotalStarData;

        currentThemeStarCount = GlobalDataManager.instance._globalData.themes[DataManager.Get_ThemeIndex - 1].ThemeStars;

        currentLevelStarData = GlobalDataManager.instance._globalData.themes[DataManager.Get_ThemeIndex - 1].Levels[DataManager.Get_LevelIndex - 1].levelStars;
    }

    public void DecreaseStars()
    {
        if (inGameStarCount != 0)
        {
            StarPanelBehaivour.instance.DestroyStars();
            inGameStarCount--;
        }
    }

    public void ManageStars()
    {
        // int GameManager.instance.isCurrentLevelPlayedBefore = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_" + "IsPlayedBefore", 0);
        globalData = GlobalDataManager.instance.LoadSavedData;

        SaveLevelStarData();

        if (GameManager.instance.isCurrentLevelPlayedBefore)
        {
            SaveTotalStarData(inGameStarCount - currentLevelStarData);
        }
        else if (!GameManager.instance.isCurrentLevelPlayedBefore)
        {

            SaveTotalStarData(inGameStarCount);
        }
        SaveThemeStarData();
    }
    private void SaveThemeStarData()
    {
        // globalData = GlobalDataManager.instance.LoadSavedData;

        if (GameManager.instance.isCurrentLevelPlayedBefore)
        {
            globalData.themes[DataManager.Get_ThemeIndex - 1].ThemeStars =
            currentThemeStarCount + Mathf.Abs(inGameStarCount - currentLevelStarData);
            GlobalDataManager.instance.SaveData(globalData);

        }
        else if (!GameManager.instance.isCurrentLevelPlayedBefore)
        {
            globalData.themes[DataManager.Get_ThemeIndex - 1].ThemeStars = currentThemeStarCount + inGameStarCount;
            GlobalDataManager.instance.SaveData(globalData);
        }
    }

    private void SaveTotalStarData(int starCountToAdd)
    {
        var new_StarData = currentTotalStars + starCountToAdd;
        currentTotalStars = new_StarData;
        // GlobalData globalData = GlobalDataManager.instance.LoadSavedData;
        globalData.TotalStarData = currentTotalStars;
        // PlayerPrefs.SetInt("TotalStars", currentTotalStars);
    }

    private void SaveLevelStarData()
    {
        // PlayerPrefs.SetInt(_currentSceneName + "_" + "StarCount", inGameStarCount);
        // GlobalData globalData = GlobalDataManager.instance.LoadSavedData;
        globalData.themes[DataManager.Get_ThemeIndex - 1].Levels[DataManager.Get_LevelIndex - 1].levelStars = inGameStarCount;
        GlobalDataManager.instance.SaveData(globalData);

    }
}
