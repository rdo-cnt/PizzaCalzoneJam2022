using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectSubmenu : MonoBehaviour
{
    public void OnClick_Back()
    {
        MainMenuManager.OpenMenu(MainMenu.MAIN_MENU,gameObject);
    }
}
