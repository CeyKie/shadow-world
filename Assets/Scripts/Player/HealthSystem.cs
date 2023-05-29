using System;
using System.Linq;
using Basics;
using Tutorial;
using UnityEngine;

namespace Player
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField]
        private int initialHealth = 3;

        [SerializeField]
        private HealthPoint healthPoint;

        [SerializeField]
        private Transform contentContainer;

        private TutorialObject[] tutorialObjects;
        
        private PoolObject<HealthPoint> healthPoints;
        private int health;

        private void Awake()
        {
            tutorialObjects = FindObjectsOfType<TutorialObject>();
            
            health = initialHealth;
            healthPoints = new PoolObject<HealthPoint>(initialHealth, healthPoint, contentContainer);
        }

        private void OnDestroy()
        {
            healthPoints.Clear();
        }

        private void Update()
        {
            if (tutorialObjects != null)
            {
                contentContainer.gameObject.SetActive(!tutorialObjects.Any(tutorialObject => tutorialObject.IsChildActive));
            }
        }

        public float TakeDamage(float hitPoints)
        {
            health = (int) Math.Clamp(health - hitPoints, 0, initialHealth);
            var healthPoint = healthPoints.GetPoolObject(health.ToString());
            healthPoint.LooseLife();
            
            return health;
        }
    }
}
