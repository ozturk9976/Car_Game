using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetLevelAndThemeAsText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI themeText;


    public static GetLevelAndThemeAsText instance;

    private void Awake()
    {
        instance = this;
    }
    public void InitializeText()
    {
        if (levelText != null)
        {
            levelText.text = "LEVEL:" + " " + DataManager.Get_LevelIndex;
        }

        if (themeText != null)
        {
            themeText.text = "THEME:" + " " + DataManager.Get_ThemeIndex;
        }
    }
}
