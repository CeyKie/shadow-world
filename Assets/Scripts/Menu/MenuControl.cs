using System;
using Basics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuControl : MonoBehaviour
    {
        private PauseControl pauseControl;

        private void Awake()
        {
            pauseControl = FindObjectOfType<PauseControl>();
        }

        public void Continue()
        {
            pauseControl.gameObject.SetActive(false);
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