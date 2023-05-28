using System;
using System.Globalization;
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
            // TODO: Always round to full numbers / .5 numbers
            health = (int) Math.Clamp(health - hitPoints, 0, initialHealth);
            var healthPoint = healthPoints.GetPoolObject(health.ToString());
            healthPoint.LooseLife();
            
            return health;
        }
    }
}
