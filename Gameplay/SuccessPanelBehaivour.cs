using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SuccessPanelBehaivour : MonoBehaviour
{
    [SerializeField] private GameObject[] stars;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject nextLevelButton;

    private void OnEnable()
    {
        SetStars();
    }
    public void SetStars()
    {
        var starCount = StarManager.instance.inGameStarCount;
        for (int i = 0; i < starCount; i++)
        {
            stars[i].gameObject.SetActive(true);
        }

    }
    public void OnGameCompletedBehaivour()
    {
        text.fontSize = 55;
        text.text = "YOU COMPLETED ALL LEVELS";
        nextLevelButton.SetActive(false);
    }
}
