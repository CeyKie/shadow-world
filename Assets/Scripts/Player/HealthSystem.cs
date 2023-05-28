using System;
using Basics;
using UnityEngine;

namespace Player
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField]
        private int initialHealth = 3;

        [SerializeField]
        private HealthPoint healthPoint;

        private PoolObject<HealthPoint> healthPoints;
        private int health;

        private void Awake()
        {
            health = initialHealth;
            healthPoints = new PoolObject<HealthPoint>(initialHealth, healthPoint, transform);
        }

        private void OnDestroy()
        {
            healthPoints.Clear();
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
