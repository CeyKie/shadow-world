using UnityEngine.SceneManagement;

namespace Basics
{
    public static class SceneHelper
    {
        public static void LoadNextOrFirstScene()
        {
            
            var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            var hasNextScene = SceneManager.sceneCountInBuildSettings > nextSceneIndex;

            SceneManager.LoadScene(hasNextScene ? nextSceneIndex : 0);
        }
        public static void LoadSpecificScene(int sceneIndex)
        {
            var hasNextScene = SceneManager.sceneCountInBuildSettings > sceneIndex;

            SceneManager.LoadScene(hasNextScene ? sceneIndex : 0);
        }
    }
}