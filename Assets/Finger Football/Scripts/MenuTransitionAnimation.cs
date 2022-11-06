using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FingerFootball
{
    //Used to make a fade in - fade out animation when user replays the game or exits to the main menu
    public class MenuTransitionAnimation : MonoBehaviour
    {
        public int menu = 0;
        public Image image;
        private bool up = true;
        private float alpha = 0;

        void Update()
        {
            if (up)
            {
                image.enabled = true;
                alpha += Time.deltaTime * 3;
                if (alpha >= 1f)
                {
                    up = false;

                    if (menu == 0) //Checks which menu should be shown after the fade in-fade out effect
                    {
                        GetComponent<Menus>().ShowLevelSelectMenu();
                    }
                    else if (menu == 1)
                    {
                        GetComponent<Menus>().HideLevelSelectMenu();
                    }
                    else if (menu == 2)
                    {
                        GetComponent<Menus>().LoadLevel();
                    }
                    else if (menu == 3)
                    {
                        GetComponent<Menus>().RestartLevel();
                    }
                    else if (menu == 4)
                    {
                        GetComponent<Menus>().ExitToMainMenu();
                    }
                    else if (menu == 5)
                    {
                        GetComponent<Menus>().NextLevel();
                    }
                }
            }
            else
            {
                alpha -= Time.deltaTime * 3;
                if (alpha <= 0)
                {
                    up = true;
                    alpha = 0;
                    image.color = new Color(0, 0, 0, 0);
                    image.enabled = false;
                    GetComponent<MenuTransitionAnimation>().enabled = false;
                }
            }
            image.color = new Color(0, 0, 0, alpha);
        }
    }
}
