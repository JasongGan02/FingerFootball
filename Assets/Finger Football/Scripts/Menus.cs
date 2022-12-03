using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FingerFootball
{
    //Used for navigation through the different menus
    public class Menus : MonoBehaviour
    {

        public GameObject mainMenuUI;
        public GameObject mainMenuScreen;
        public GameObject settingsMenu;
        public GameObject statsMenu;
        public GameObject levelSelectUI;
        public GameObject gameplayUI;
        public GameObject pauseMenuUI;
        public GameObject pauseButton;
        public GameObject aimButton;
        public InstantiateBall instantiateBall;
        public Toggle invertControls;
        public Toggle vibration;
        public Slider audioSlider;
        private AudioSource buttonSound;

        public Boolean aim = false;
        void Start()
        {
            buttonSound = GameObject.Find("ButtonSound").GetComponent<AudioSource>();
        }

        public void ShowSettingsMenu()
        {
            buttonSound.Play();
            settingsMenu.SetActive(true);
            if (PlayerPrefs.GetInt("InvertControls") == 0)
            {
                invertControls.isOn = false;
            }
            else
            {
                invertControls.isOn = true;
            }

            if (PlayerPrefs.GetInt("Vibration") == 0)
            {
                vibration.isOn = false;
            }
            else
            {
                vibration.isOn = true;
            }
        }
        
        public void setAim()
        {
            aim = !aim;
            Debug.Log(aim);
        }

        public void Volume()
        {
            AudioListener.volume = audioSlider.value;
        }

        public void InvertControls()
        {
            if (invertControls.isOn)
            {
                PlayerPrefs.SetInt("InvertControls", 1);
            }
            else
            {
                PlayerPrefs.SetInt("InvertControls", 0);
            }
        }

        public void Vibration()
        {
            if (vibration.isOn)
            {
                PlayerPrefs.SetInt("Vibration", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Vibration", 0);
            }
        }

        public void HideSettingsMenu()
        {
            buttonSound.Play();
            settingsMenu.SetActive(false);
        }

        public void ShowStatsMenu()
        {
            buttonSound.Play();
            statsMenu.SetActive(true);
        }

        public void HideStatsMenu()
        {
            buttonSound.Play();
            statsMenu.SetActive(false);
        }

        public void ShowLevelSelectMenuAnimation()
        {
            buttonSound.Play();
            GetComponent<MenuTransitionAnimation>().menu = 0;
            GetComponent<MenuTransitionAnimation>().enabled = true;
        }

        public void HideLevelSelectMenuAnimation()
        {
            buttonSound.Play();
            GetComponent<MenuTransitionAnimation>().menu = 1;
            GetComponent<MenuTransitionAnimation>().enabled = true;
        }

        public void ShowLevelSelectMenu()
        {
            mainMenuUI.SetActive(false);
            levelSelectUI.SetActive(true);
            mainMenuScreen.SetActive(false);
        }

        public void HideLevelSelectMenu()
        {
            mainMenuUI.SetActive(true);
            levelSelectUI.SetActive(false);
            mainMenuScreen.SetActive(true);
        }

        public void BackToTheMainMenu()
        {

        }

        public void LevelLoadAnimation()
        {
            buttonSound.Play();
            Vars.currentLevel = EventSystem.current.currentSelectedGameObject.name;
            GetComponent<MenuTransitionAnimation>().menu = 2;
            GetComponent<MenuTransitionAnimation>().enabled = true;
        }

        public void LoadLevel()
        {
            GameObject level = Instantiate(Resources.Load("Levels/Level" + Vars.currentLevel, typeof(GameObject))) as GameObject;
            level.name = "Level";
            instantiateBall.enabled = true;
            instantiateBall.ballPosition = GameObject.Find("Ball").transform.position;
            instantiateBall.hitPos = instantiateBall.ballPosition;
            Debug.Log(instantiateBall.hitPos);
            mainMenuUI.SetActive(false);
            mainMenuScreen.SetActive(false);
            levelSelectUI.SetActive(false);
            gameplayUI.SetActive(true);
        }

        public void ShowPauseMenu()
        {
            buttonSound.Play();
            Time.timeScale = 0;
            pauseMenuUI.SetActive(true);
            pauseButton.SetActive(false);
            if (GameObject.Find("Ball") != null)
                GameObject.Find("Ball").GetComponent<BallRotationAndShooting>().enabled = false;
            instantiateBall.enabled = false;
        }

        public void HidePauseMenu()
        {
            buttonSound.Play();
            Time.timeScale = 1;
            pauseMenuUI.SetActive(false);
            pauseButton.SetActive(true);
            Invoke("EnableBallRotationAndShootingScript", 1f);
        }

        private void EnableBallRotationAndShootingScript()
        {
            if (GameObject.Find("Ball") != null)
                GameObject.Find("Ball").GetComponent<BallRotationAndShooting>().enabled = true;
            instantiateBall.enabled = true;
        }

        public void RestartLevelAnimation()
        {
            buttonSound.Play();
            Time.timeScale = 1;
            GetComponent<MenuTransitionAnimation>().menu = 3;
            GetComponent<MenuTransitionAnimation>().enabled = true;
            CancelInvoke("ShowLevelCompleteMenu");
            if (GameObject.Find("Ball") != null) Destroy(GameObject.Find("Ball"));
        }

        public void RestartLevel()
        {
            Time.timeScale = 1;
            pauseMenuUI.SetActive(false);
            pauseButton.SetActive(true);
            if (GameObject.Find("Level") != null)
            {
                Destroy(GameObject.Find("Level"));
            }
            LoadLevel();
            if (GameObject.Find("Ball") != null) Destroy(GameObject.Find("Ball"));
            instantiateBall.InstantiateNewBall();
        }

        public void ExitToMainMenuAnimation()
        {
            buttonSound.Play();
            Time.timeScale = 1;
            GetComponent<MenuTransitionAnimation>().menu = 4;
            GetComponent<MenuTransitionAnimation>().enabled = true;
            CancelInvoke("ShowLevelCompleteMenu");
            if (GameObject.Find("Ball") != null)
            {
                Destroy(GameObject.Find("Ball"));
            }
        }

        public void ExitToMainMenu()
        {
            HideLevelSelectMenu();
            pauseMenuUI.SetActive(false);
            pauseButton.SetActive(true);
            gameplayUI.SetActive(false);
            if (GameObject.Find("Ball") != null)
            {
                Destroy(GameObject.Find("Ball"));
            }
            if (GameObject.Find("Level") != null)
            {
                Destroy(GameObject.Find("Level"));
            }
            instantiateBall.enabled = false;
        }

  
        public void LevelComplete()
        {
            int currentLevel = Int32.Parse(Vars.currentLevel);
            if (PlayerPrefs.GetInt("LevelUnlock") < currentLevel + 1)
            {
                PlayerPrefs.SetInt("LevelUnlock", currentLevel + 1);
            }
            GameObject.Find("SuccessSound").GetComponent<AudioSource>().Play();
            instantiateBall.enabled = false;
            Invoke("NextLevelAnimation", 1f);
        }

        public void NextLevelAnimation()
        {
            GetComponent<MenuTransitionAnimation>().menu = 5;
            GetComponent<MenuTransitionAnimation>().enabled = true;
        }

        public void NextLevel()
        {
            Destroy(GameObject.Find("Ball"));
            Vars.currentLevel = "" + (Int32.Parse(Vars.currentLevel) + 1);
            if (GameObject.Find("Level") != null) Destroy(GameObject.Find("Level"));
            GameObject level = Instantiate(Resources.Load("Levels/Level" + Vars.currentLevel, typeof(GameObject))) as GameObject;
            instantiateBall.ballPosition = level.transform.Find("Ball").transform.position;
            instantiateBall.hitPos = instantiateBall.ballPosition;
            instantiateBall.enabled = true;
            level.name = "Level";
            pauseButton.SetActive(true);
        }

        public void ExitTheGame()
        {
            buttonSound.Play();
            Application.Quit();
        }
    }
}