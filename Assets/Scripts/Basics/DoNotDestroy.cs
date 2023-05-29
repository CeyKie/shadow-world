using UnityEngine;
using UnityEngine.SceneManagement;

namespace Basics
{
    public class DoNotDestroy : MonoBehaviour
    {
        private void Awake()
        {
            var objs = FindObjectsOfType<DoNotDestroy>();

            if (objs.Length > 1)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
