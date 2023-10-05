using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseTheme : MonoBehaviour
{

    public void SelectTheme(GameObject whichTheme)
    {
        MainMenuUIManager.instance.OpenLevelContainer(whichTheme);
    }
}
