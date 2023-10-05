using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PageManager : MonoBehaviour
{
    public static PageManager instance;
    public MENU_ENUMS.PAGES currentPage;

    void Awake()
    {
        instance = this;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ChangeCurrentPage(currentPage);
        }
    }
    public void ChangeCurrentPage(MENU_ENUMS.PAGES page)
    {
        switch (page)
        {
            case MENU_ENUMS.PAGES.MAIN_MENU:
                MainMenuUIManager.instance.AskForQuit();
                break;
            case MENU_ENUMS.PAGES.OPTIONS_MENU:
                MainMenuUIManager.instance.CloseSettingsMenu();
                break;
        }
    }
}
