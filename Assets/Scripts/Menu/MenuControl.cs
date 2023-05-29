using System;
using Basics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuControl : MonoBehaviour
    {
        private PauseMenu pauseMenu;

        private void Awake()
        {
            pauseMenu = FindObjectOfType<PauseMenu>();
        }

        public void Continue()
        {
            pauseMenu.gameObject.SetActive(false);
        }
        
        public void RestartLevel()
        {
            SceneHelper.LoadSpecificScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void RestartGame()
        {
            SceneHelper.LoadSpecificScene(0);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}