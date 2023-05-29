using Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basics
{
    public class BaseFunctionalities : MonoBehaviour
    {
        [SerializeField]
        private PauseMenu pauseMenu;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (!Input.GetKeyDown(KeyCode.Escape))
            {
                return;
            }

            if (pauseMenu == null)
            {
                Application.Quit();
                return;
            }

            pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
        }
    }
}