using Basics;
using Player;
using UnityEngine;
using World.Models;

namespace Enemy
{
    public class EnemyBattle : MonoBehaviour
    {
        [SerializeField]
        private bool isShadowEnemy;

        [SerializeField]
        private int initialHealth = 1;

        [SerializeField]
        private int damage = 1;
        public float Damage => damage;

        [SerializeField]
        private AudioSource hitSfx;

        private float health;


        private WorldModel worldModel;

        private void Awake()
        {
            worldModel = FindObjectOfType<WorldModel>();
            health = initialHealth;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            TakeDamage(col);
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            TakeDamage(col);
        }

        private void TakeDamage(Collider2D col)
        {
            var swordCollisionDetection = col.gameObject.GetComponent<SwordCollisionDetection>();

            if (swordCollisionDetection == null)
            {
                return;
            }

            var playerBattle = swordCollisionDetection.gameObject.GetComponentInParent<PlayerBattle>();
            if (playerBattle == null || !playerBattle.gameObject.CompareTag("Player") ||
                worldModel.isShadowWorld != isShadowEnemy)
            {
                return;
            }

            AudioController.PlayOneTimeSfx(hitSfx);
            health = (int) Mathf.Clamp(health - playerBattle.Damage, 0, initialHealth);

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}