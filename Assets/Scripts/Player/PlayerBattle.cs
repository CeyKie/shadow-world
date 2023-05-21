using System;
using System.Collections;
using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerMovement))]
    public class PlayerBattle : MonoBehaviour
    {
        [SerializeField]
        private float initialHealth = 2f;
        
        [SerializeField]
        private float damage = 1f;
        public float Damage => damage;

        private static readonly int Attack = Animator.StringToHash("attack");
        private static readonly int Death = Animator.StringToHash("death");
        private static readonly int Hit = Animator.StringToHash("hit");
        
        private Animator characterAnimator;
        private PlayerMovement playerMovement;
        private float health;

        private void Awake()
        {
            characterAnimator = GetComponent<Animator>();
            playerMovement = GetComponent<PlayerMovement>();
            health = initialHealth;
            playerMovement.enabled = true;
        }

        private void OnDestroy()
        {
            ResetFlags();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                characterAnimator.SetTrigger(Attack);
            }
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            var enemyBehavior = col.gameObject.GetComponent<EnemyBehavior>();
            if (!col.gameObject.CompareTag("Enemy") || enemyBehavior == null)
            {
                return;
            }
            
            characterAnimator.SetTrigger(Hit);
            TakeDamage(enemyBehavior.Damage);
        }

        private void TakeDamage(float hitPoints)
        {
            // TODO: Always round to full numbers / .5 numbers
            var newHealth = Mathf.Clamp(health - hitPoints, 0, initialHealth);

            if (health <= 0 || newHealth <= 0)
            {
                StartCoroutine(Die());
                return;
            }

            health = newHealth;
        }

        private IEnumerator Die()
        {
            playerMovement.enabled = false;
            characterAnimator.SetTrigger(Death);
            yield return new WaitForSecondsRealtime(1.1f);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ResetFlags()
        {
            characterAnimator.ResetTrigger(Death);
            characterAnimator.ResetTrigger(Hit);
            characterAnimator.ResetTrigger(Attack);
        }
    }
}
