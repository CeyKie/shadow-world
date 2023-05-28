using System.Collections;
using Basics;
using Enemy;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    [RequireComponent(typeof(Animator), typeof(PlayerMovement))]
    public class PlayerBattle : MonoBehaviour
    {
        [SerializeField]
        private float damage = 1f;
        public float Damage => damage;

        [SerializeField]
        private AudioSource swordSwingSfx;

        [SerializeField]
        private AudioSource hitSfx;

        [SerializeField]
        private AudioSource deathSfx;

        [SerializeField]
        private SwordCollisionDetection swordCollisionDetection;

        private static readonly int Attack = Animator.StringToHash("attack");
        private static readonly int Death = Animator.StringToHash("death");
        private static readonly int Hit = Animator.StringToHash("hit");
        
        private Animator characterAnimator;
        private PlayerMovement playerMovement;
        private HealthSystem healthSystem;

        private void Awake()
        {
            characterAnimator = GetComponent<Animator>();
            playerMovement = GetComponent<PlayerMovement>();
            healthSystem = FindObjectOfType<HealthSystem>();
            playerMovement.enabled = true;
        }

        private void OnDestroy()
        {
            ResetFlags(false);
        }

        private void Update()
        {
            if (!Input.GetButtonDown("Fire1"))
            {
                return;
            }

            characterAnimator.SetTrigger(Attack);
            StartCoroutine(TriggerSwordCollision());
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            var enemyBehavior = col.gameObject.GetComponent<EnemyBattle>();
            if (!col.gameObject.CompareTag("Enemy") || enemyBehavior == null)
            {
                return;
            }
            
            TakeDamage(enemyBehavior.Damage);
        }

        private IEnumerator TriggerSwordCollision()
        {
            yield return new WaitForSeconds(0.2f);
            AudioController.PlayOneTimeSfx(swordSwingSfx);
            yield return new WaitForSeconds(0.35f);
            swordCollisionDetection.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            swordCollisionDetection.gameObject.SetActive(false);
        }

        private void TakeDamage(float hitPoints)
        {
            var health  = healthSystem.TakeDamage(hitPoints);
            if (health <= 0)
            {
                StartCoroutine(Die());
                return;
            }

            characterAnimator.SetTrigger(Hit);
            AudioController.PlayOneTimeSfx(hitSfx);
        }

        private IEnumerator Die()
        {
            playerMovement.enabled = false;
            characterAnimator.SetTrigger(Death);
            AudioController.PlayOneTimeSfx(deathSfx);
            ResetFlags(true);
            yield return new WaitForSecondsRealtime(1.1f);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void ResetFlags(bool isDead)
        {
            if (!isDead)
            {
                characterAnimator.ResetTrigger(Death);
            }

            characterAnimator.ResetTrigger(Hit);
            characterAnimator.ResetTrigger(Attack);
        }
    }
}
