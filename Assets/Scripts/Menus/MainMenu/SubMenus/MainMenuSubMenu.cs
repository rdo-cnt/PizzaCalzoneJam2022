using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSubMenu : MonoBehaviour
{

    public void OnClick_Settings()
    {
        MainMenuManager.OpenMenu(MainMenu.SETTINGS,gameObject);
    }

    public void OnClick_LevelSelect()
    {
        MainMenuManager.OpenMenu(MainMenu.LEVEL_SELECT,gameObject);
    }
    
    public void OnClick_Credits()
    {
        MainMenuManager.OpenMenu(MainMenu.CREDITS,gameObject);
    }


}
