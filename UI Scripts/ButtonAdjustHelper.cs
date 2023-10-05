using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;


public class ButtonAdjustHelper : MonoBehaviour
{
    [SerializeField] private Button mainButton;
    [SerializeField] private GameObject adjustMode;
    [SerializeField] private GameObject defaultMode;
    public ControlButtonHelper controlButtonHelper;

    public void AdjustMode()
    {

        adjustMode.SetActive(true);

        if (mainButton.TryGetComponent(out OnScreenButton onScreenButton))
        {
            mainButton.GetComponent<OnScreenButton>().enabled = false;
        }

        mainButton.GetComponent<EventTrigger>().enabled = false;


    }
    public void NoAdjustMode()
    {

        if (mainButton.TryGetComponent(out OnScreenButton onScreenButton))
        {
            mainButton.GetComponent<OnScreenButton>().enabled = true;
        }

        mainButton.GetComponent<EventTrigger>().enabled = true;
        adjustMode.SetActive(false);

    }
}
