using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reference video https://www.youtube.com/watch?v=MmsbM89SVbM

//MainMenuManager class, with no mono behaviour
//Static which allows it to persist and doesn't need to be called
public static class MainMenuManager
{

    public static bool IsInitialized {get; private set;}


    public static GameObject mainMenu, levelSelect, settingsMenu, credits;
    //Admittedly hardcoded variables incoming
    public static void Init()
    {
        GameObject canvas = GameObject.Find("Canvas");
        mainMenu = canvas.transform.Find("MainMenu").gameObject;
        settingsMenu = canvas.transform.Find("settingsMenu").gameObject;
        credits = canvas.transform.Find("credits").gameObject;

        IsInitialized = true;
    }

    // "openedMenu" parameter is the menu that's being opened, "callingMenu" is the one who's about to get closed
    public static void OpenMenu(MainMenu openedMenu, GameObject callingMenu)
    {

        if(!IsInitialized)
            Init();

        switch(openedMenu)
        {
            case MainMenu.MAIN_MENU:
                mainMenu.SetActive(true);
                break;
            case MainMenu.LEVEL_SELECT:
                levelSelect.SetActive(true);
                break;
            case MainMenu.SETTINGS:
                settingsMenu.SetActive(true);
                break;
            case MainMenu.CREDITS:
                credits.SetActive(true);
                break;
        }
        callingMenu.SetActive(false);
    }

}
