using Menu;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basics
{
    public class BaseFunctionalities : MonoBehaviour
    {
        [SerializeField]
        private PauseControl pauseControl;

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

            if (pauseControl == null)
            {
                Application.Quit();
                return;
            }

            pauseControl.gameObject.SetActive(!pauseControl.gameObject.activeSelf);
        }
    }
}