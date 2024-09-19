using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] menus;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenMenu(string menuName)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName) 
            {
                menus[i].Opne();
            }
            else if (menus[i].opne)
            {
                CloseMenu(menus[i]);
            }

        }

    }


    public void OpenMenu(Menu menu)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].opne)
            {
                CloseMenu(menus[i]);
            }

        }
        menu.Opne();

    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();

    }
}

