using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CarUIHelper : MonoBehaviour
{
    public List<GameObject> controlButtons = new List<GameObject>();
    public int listNumber;

    private void OnEnable()
    {
        LoadAdjusted();

        foreach (var item in controlButtons)
        {
            item.GetComponent<ButtonAdjustHelper>().controlButtonHelper.DisableObjectAfterInit();
        }
    }
    public void SaveAdjusted()
    {
        string currentUI = PlayerPrefs.GetString("WhichControls", "CarUI_1");


        foreach (var item in controlButtons)
        {
            PlayerPrefs.SetFloat(currentUI + "_" + item.gameObject.name + "ItemPositionX", item.GetComponent<RectTransform>().anchoredPosition.x);
            PlayerPrefs.SetFloat(currentUI + "_" + item.gameObject.name + "ItemPositionY", item.GetComponent<RectTransform>().anchoredPosition.y);
            PlayerPrefs.SetFloat(currentUI + "_" + item.gameObject.name + "ItemScaleX", item.GetComponent<RectTransform>().gameObject.transform.localScale.x);
            PlayerPrefs.SetFloat(currentUI + "_" + item.gameObject.name + "ItemScaleY", item.GetComponent<RectTransform>().gameObject.transform.localScale.y);
        }

        foreach (var item in controlButtons)
        {
            var adjustHelper = item.GetComponent<ButtonAdjustHelper>();
            adjustHelper.NoAdjustMode();
        }
    }

    public void LoadAdjusted()
    {
        string currentUI = PlayerPrefs.GetString("WhichControls", "CarUI_1");

        foreach (var item in controlButtons)
        {
            //item.GetComponent<ControlButtonHelper>();

            ControlButtonHelper cbh = item.GetComponent<ButtonAdjustHelper>().controlButtonHelper;

            float getTransformX = PlayerPrefs.GetFloat(currentUI + "_" + item.gameObject.name + "ItemPositionX", cbh.Get_TransformX);
            float getTransformY = PlayerPrefs.GetFloat(currentUI + "_" + item.gameObject.name + "ItemPositionY", cbh.Get_TransformY);
            float getScaleX = PlayerPrefs.GetFloat(currentUI + "_" + item.gameObject.name + "ItemScaleX", cbh.Get_ScaleX);
            float getScaleY = PlayerPrefs.GetFloat(currentUI + "_" + item.gameObject.name + "ItemScaleY", cbh.Get_ScaleY);
            cbh.ChangePositionAndScale(getTransformX, getTransformY, getScaleX, getScaleY);
        }

    }
    public void DefaultPosScale()
    {
        foreach (var item in controlButtons)
        {
            var adjustHelper = item.GetComponent<ButtonAdjustHelper>();
            var cbh = item.GetComponent<ButtonAdjustHelper>().controlButtonHelper;
            RectTransform rect = item.GetComponent<ButtonAdjustHelper>().controlButtonHelper.rect;


            rect.anchoredPosition = cbh.defaultPosition;
            rect.transform.localScale = cbh.defaultScale;
            adjustHelper.NoAdjustMode();

        }
    }
    public void AdjustMode()
    {
        foreach (var item in controlButtons)
        {
            var adjustHelper = item.GetComponent<ButtonAdjustHelper>();
            adjustHelper.AdjustMode();
        }
    }
}
