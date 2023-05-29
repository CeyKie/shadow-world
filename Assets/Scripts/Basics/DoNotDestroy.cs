using UnityEngine;

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
    }
}
