using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class GlobalData
{
    public int CurrentPlayableThemeCount;
    public int TotalStarData;
    public int UnlockedThemeCount;
    public List<ThemeBlueprint> themes = new List<ThemeBlueprint>();
}


[System.Serializable]
public class ThemeBlueprint
{
    public string ThemeName;

    public int ThemeIndex;

    public int UnlockedLevelCount;

    public int ThemeStars;

    public int PlayedLevelCount;

    public bool isLastTheme;

    public List<LevelBlueprint> Levels = new List<LevelBlueprint>(new LevelBlueprint[50]);

}

[System.Serializable]
public class LevelBlueprint
{

    // public int WhichTheme;
    public int levelStars;
    // public int LevelIndex;
    public bool IsPlayedBefore;
    // public string levelName;



}


[System.Serializable]
public class GlobalDataManager : MonoBehaviour
{
    public int TotalThemeCount;
    public GlobalData _globalData;

    public static GlobalDataManager instance;

    //Read current saved data at awake
    private void Awake()
    {
        instance = this;
        _globalData = LoadSavedData;
    }

    //Save data to json file then playerprefs
    public void SaveData(GlobalData globalData)
    {
        string saveJson = JsonUtility.ToJson(_globalData);
        PlayerPrefs.SetString("GlobalData", saveJson);
        PlayerPrefs.Save();
    }

    //Load saved data and set new stars
    public GlobalData LoadSavedData
    {
        get
        {
            if (PlayerPrefs.HasKey("GlobalData"))
            {
                var globalDataPrefs = PlayerPrefs.GetString("GlobalData");
                _globalData = JsonUtility.FromJson<GlobalData>(globalDataPrefs);
            }
            else
            {
                for (int i = 1; i < _globalData.CurrentPlayableThemeCount + 1; i++)
                {
                    CreateNewTheme("Theme" + i, i, 1, 0);
                    CreateNewLevel(/* "Theme" + i + "_" + i,*/ i, 1, 0, false);
                }

                //If there is no data create first theme
                _globalData.UnlockedThemeCount = 1;
                _globalData.TotalStarData = 0;
                _globalData.themes[_globalData.CurrentPlayableThemeCount - 1].isLastTheme = true;
                SaveData(_globalData);
            }

            return _globalData;

        }

    }

    public void CreateNewLevel(/*string _LevelName,*/ int _WhichTheme, int _LevelIndex, int _LevelStars, bool _IsPlayedBefore)
    {
        LevelBlueprint levelBlueprint = new LevelBlueprint();
        // levelBlueprint.levelName = _LevelName;
        levelBlueprint.levelStars = _LevelStars;
        // levelBlueprint.WhichTheme = _WhichTheme;
        // levelBlueprint.LevelIndex = _LevelIndex;
        levelBlueprint.IsPlayedBefore = _IsPlayedBefore;
        _globalData.themes[_WhichTheme - 1].Levels.Insert(_LevelIndex - 1, levelBlueprint);


    }

    public void CreateNewTheme(string _ThemeName, int _ThemeIndex, int _UnlockedLevelCount, int _ThemeStars)
    {
        Debug.Log("new theme created");
        ThemeBlueprint _newTheme = new ThemeBlueprint();
        _newTheme.ThemeName = _ThemeName;
        _newTheme.ThemeIndex = _ThemeIndex;
        _newTheme.UnlockedLevelCount = _UnlockedLevelCount;
        _newTheme.ThemeStars = _ThemeStars;
        _globalData.themes.Add(_newTheme);
        SaveData(_globalData);
    }

    //Set stars of new level
    public void SetLevelStars(string LevelName, int LevelIndex, int ThemeIndex, int LevelStar)
    {
        _globalData.themes[ThemeIndex].Levels[LevelIndex].levelStars = LevelStar;
    }

    //Unlock new level
    public void UnlockNewLevel(int ThemeIndex, string LevelName)
    {
        LevelBlueprint _newLevel = new LevelBlueprint();
        //_newLevel.levelName = LevelName;
        _newLevel.levelStars = 0;
        _globalData.themes[ThemeIndex].Levels.Add(_newLevel);
        SaveData(_globalData);
    }
}
