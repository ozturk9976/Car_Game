using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightColorChange : MonoBehaviour
{
    [SerializeField] GameObject greenLight;
    [SerializeField] GameObject redLight;


    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.AddHandler(GameEvent.OnLevelPass, OnGameOver);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
        EventManager.RemoveHandler(GameEvent.OnLevelPass, OnGameOver);
    }

    private void Start()
    {
        redLight.gameObject.SetActive(true);
        greenLight.gameObject.SetActive(false);
    }

    public void ChangeLightColor(string color)
    {
        switch (color)
        {
            case "red":
                redLight.gameObject.SetActive(true);
                greenLight.gameObject.SetActive(false);
                break;

            case "green":
                redLight.gameObject.SetActive(false);
                greenLight.gameObject.SetActive(true);
                break;
        }
    }

    private void OnGameOver()
    {
        StopAllCoroutines();
        this.enabled = false;
    }
}
