using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.IO;


public class DataManager : MonoBehaviour
{
    private static int currentThemeIndex;
    private static string currentThemeName;
    private static int currentLevelIndex;
    private static int theme;
    private static int level;

    public static Dictionary<string, int> ThemeLevelTresholds = new Dictionary<string, int>()
    {
        {"Theme1", 50},
        {"Theme2", 50},
        {"Theme3", 50},
        {"Theme4", 50},
        {"Theme5", 50},
        {"Theme6", 50},
        {"Theme7", 50},
        {"Theme8", 50}
    };




    public static DataManager instance;
    private void Awake()
    {
        instance = this;
        // DontDestroyOnLoad(this);
    }

    public static int Get_LevelIndex
    {
        get
        {
            var name = SceneManager.GetActiveScene().name;
            var refinedName = name.Remove(0, 7);
            currentLevelIndex = int.Parse(refinedName);
            return currentLevelIndex;
        }
    }

    public static string Get_ThemeName
    {
        get
        {
            var name = SceneManager.GetActiveScene().name;
            currentThemeName = name.Substring(0, 6);

            switch (currentThemeName)
            {
                case "Theme1":
                    currentThemeName = "Theme1";
                    break;
                case "Theme2":
                    currentThemeName = "Theme2";
                    break;
                case "Theme3":
                    currentThemeName = "Theme3";
                    break;
                case "Theme4":
                    currentThemeName = "Theme4";
                    break;
                case "Theme5":
                    currentThemeName = "Theme5";
                    break;
                case "Theme6":
                    currentThemeName = "Theme6";
                    break;
                case "Theme7":
                    currentThemeName = "Theme7";
                    break;
            }
            return currentThemeName;
        }

    }

    public static int Get_ThemeIndex
    {
        get
        {
            string name = Get_ThemeName;

            switch (name)
            {
                case "Theme1":
                    currentThemeIndex = 1;
                    break;
                case "Theme2":
                    currentThemeIndex = 2;
                    break;
                case "Theme3":
                    currentThemeIndex = 3;
                    break;
                case "Theme4":
                    currentThemeIndex = 4;
                    break;
                case "Theme5":
                    currentThemeIndex = 5;
                    break;
                case "Theme6":
                    currentThemeIndex = 6;
                    break;
                case "Theme7":
                    currentThemeIndex = 7;
                    break;
            }
            return currentThemeIndex;
        }
    }

}
