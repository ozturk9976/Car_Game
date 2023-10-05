using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CarUIContainer : MonoBehaviour
{
    public List<RectTransform> controsUI_List = new List<RectTransform>();


    public static CarUIContainer instance;

    private void Awake()
    {
        instance = this;
    }
    public void ChangeUI_Panel(string whichPanel)
    {
        InputManager.API.ResetAllAxis();
        AudioManager.instance.PlayButton1_Sfx();

        foreach (var item in controsUI_List)
        {
            if (item.name != whichPanel)
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                item.gameObject.SetActive(true);
                InputManager.API.currentControls = item.name.ToString();
                PlayerPrefs.SetString("WhichControls", item.name.ToString());
                UIManager.instance.whichControlsActive = item.name.ToString();
                UIManager.instance.ChangeCurrentCarUIHelperTarget();
                ManageDeviceSensors(item.name);
            }
        }
    }

    public void ManageDeviceSensors(string WhichPanel)
    {

        switch (WhichPanel)
        {
            case "CarUI_1":

                if (UnityEngine.InputSystem.Accelerometer.current != null)
                {
                    InputSystem.DisableDevice(UnityEngine.InputSystem.Accelerometer.current);
                }
                break;

            case "CarUI_2":

                if (UnityEngine.InputSystem.Accelerometer.current != null)
                {
                    InputSystem.DisableDevice(UnityEngine.InputSystem.Accelerometer.current);
                }
                break;

            case "CarUI_3":

                if (UnityEngine.InputSystem.Accelerometer.current != null)
                {
                    InputSystem.EnableDevice(UnityEngine.InputSystem.Accelerometer.current);
                }
                break;

        }
    }
}


