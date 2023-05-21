using Player;
using UnityEngine;
using World.Models;

namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField]
        private bool isShadowEnemy;

        [SerializeField]
        private float initialHealth = 1f;

        [SerializeField]
        private float damage = 0.5f;
        public float Damage => damage;
        
        private float health;


        private WorldModel worldModel;

        private void Awake()
        {
            worldModel = FindObjectOfType<WorldModel>();
            health = initialHealth;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            TakeDamage(col);
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            TakeDamage(col);
        }

        private void TakeDamage(Collision2D col)
        {
            var playerBattle = col.gameObject.GetComponent<PlayerBattle>();
            if (!col.gameObject.CompareTag("Player") || worldModel.isShadowWorld != isShadowEnemy ||
                playerBattle == null)
            {
                return;
            }

            // TODO: Always round to full numbers / .5 numbers
            var newHealth = Mathf.Clamp(health - playerBattle.Damage, 0, initialHealth);

            if (health <= 0f || newHealth <= 0f)
            {
                Destroy(gameObject);
                return;
            }

            health = newHealth;
        }
    }
}