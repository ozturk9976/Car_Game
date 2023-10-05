using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;


[System.Serializable]
public class ButtonGroup
{
    public string playerprefData;
    public string defaultPrefData;
    public List<Button> buttons = new List<Button>();
}



public class ToggleManager : MonoBehaviour
{
    [SerializeField] private List<ButtonGroup> buttonGroups = new List<ButtonGroup>();

    [Header("Button Group 1 Settings")]
    [SerializeField] private List<Button> button_Group_1 = new List<Button>();

    [Space(15)]

    [Header("Button Group 2 Settings")]
    [SerializeField] private List<Button> button_Group_2 = new List<Button>();


    [Header("Button Group3 Settings")]
    [SerializeField] private List<Button> button_Group_3 = new List<Button>();
    [Space(15)]
    private string whichShadowQuality;

    private void OnEnable()
    {

        foreach (var item in buttonGroups)
        {
            item.playerprefData = PlayerPrefs.GetString(item.playerprefData, item.defaultPrefData);
            for (int i = 0; i < item.buttons.Count; i++)
            {
                if (item.buttons[i].name != item.playerprefData)
                {
                    item.buttons[i].GetComponent<ToggleButtonHelper>().SetDisabledSprite();
                }
                else
                {
                    item.buttons[i].GetComponent<ToggleButtonHelper>().SetActiveSprite();
                }
            }
        }
    }

    public void Clicked()
    {
        var which_Button_Pressed = EventSystem.current.currentSelectedGameObject;
        Button pressed_button_component = which_Button_Pressed.GetComponent<Button>();
        OnButtonSelect(pressed_button_component);
        AudioManager.instance.PlayButton1_Sfx();
    }

    public void OnButtonSelect(Button selectedButton)
    {

        foreach (var item in buttonGroups)
        {
            if (item.buttons.Contains(selectedButton))
            {
                for (int i = 0; i < item.buttons.Count; i++)
                {
                    if (item.buttons[i] != selectedButton)
                    {
                        item.buttons[i].GetComponent<ToggleButtonHelper>().SetDisabledSprite();
                    }
                    else
                    {
                        item.buttons[i].GetComponent<ToggleButtonHelper>().SetActiveSprite();
                    }
                }
            }


        }
    }
}
