using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeManager : MonoBehaviour
{
    [SerializeField] float minStartDistance;
    [SerializeField] float maxStartDistance;
    [SerializeField] float minEndDistance;
    [SerializeField] float maxEndDistance;
    [Header("Pick fog color for theme")]
    [SerializeField] private List<Color32> themeColors = new List<Color32>();

    [Header("Pick secondary fog color for theme")]
    [SerializeField] private List<Color32> themeColors2 = new List<Color32>();


    public static ThemeManager instance;
    void Awake()
    {
        instance = this;
    }

    public void InitializeThemeData()
    {
        float _listChance = Mathf.RoundToInt(Random.Range(1.0f, 2.0f));

        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;

        RenderSettings.fogStartDistance = Random.Range(minStartDistance, maxStartDistance);
        RenderSettings.fogEndDistance = Random.Range(minEndDistance, maxEndDistance);

        if (RenderSettings.fogStartDistance > RenderSettings.fogEndDistance)
        {
            RenderSettings.fogEndDistance = RenderSettings.fogStartDistance + 60;
        }


        int _themeIndex = DataManager.Get_ThemeIndex;

        switch (_listChance)
        {
            case 1:
                RenderSettings.fogColor = themeColors[_themeIndex - 1];
                break;
            case 2:
                RenderSettings.fogColor = themeColors2[_themeIndex - 1];
                break;
        }
    }
}
