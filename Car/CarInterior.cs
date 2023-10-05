using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarInterior : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI canvasKmh;
    [SerializeField] private Image rightSignal;
    [SerializeField] private Image leftSignal;
    [SerializeField] private Image brakeSign;
    [SerializeField] private GameObject kmhText;

    public static CarInterior instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        EventManager.AddHandler(GameEvent.OnGameOver, OnGameOver);
    }
    private void OnDisable()
    {
        EventManager.RemoveHandler(GameEvent.OnGameOver, OnGameOver);
    }
    public void UpdateKMH(int value)
    {
        canvasKmh.text = value.ToString();
    }

    public void RightSignal()
    {

        if (leftSignal.gameObject.activeSelf)
        {
            leftSignal.gameObject.SetActive(false);
        }

        if (!rightSignal.gameObject.activeSelf)
        {
            rightSignal.gameObject.SetActive(true);
        }

    }
    public void LeftSignal()
    {
        if (!leftSignal.gameObject.activeSelf)
        {
            leftSignal.gameObject.SetActive(true);
        }

        if (rightSignal.gameObject.activeSelf)
        {
            rightSignal.gameObject.SetActive(false);
        }
    }
    public void BrakeSign()
    {
        if (!brakeSign.gameObject.activeSelf)
        {
            brakeSign.gameObject.SetActive(true);
        }

        if (kmhText.gameObject.activeSelf)
        {
            kmhText.SetActive(false);
        }
    }

    public void ResetSignals()
    {
        if (leftSignal.gameObject.activeSelf)
        {
            leftSignal.gameObject.SetActive(false);
        }

        if (rightSignal.gameObject.activeSelf)
        {
            rightSignal.gameObject.SetActive(false);
        }

    }
    public void ResetBrakeSign()
    {
        if (brakeSign.gameObject.activeSelf)
        {
            brakeSign.gameObject.SetActive(false);
        }

        if (!kmhText.gameObject.activeSelf)
        {
            kmhText.SetActive(true);
        }
    }

    private void OnGameOver()
    {
        this.enabled = false;
    }
}
