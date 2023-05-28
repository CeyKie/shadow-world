using UnityEngine;

namespace Player
{
    public class HealthPoint : MonoBehaviour
    {
        [SerializeField]
        private GameObject heartFilling;

        private void Awake()
        {
            AddLife();
        }

        public void LooseLife()
        {
            heartFilling.SetActive(false);
        }

        private void AddLife()
        {
            heartFilling.SetActive(true);
        }
    }
}