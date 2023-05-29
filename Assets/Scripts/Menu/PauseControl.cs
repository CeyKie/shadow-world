using Dialogues;
using Enemy;
using Player;
using UnityEngine;
using World;

namespace Menu
{
    public class PauseControl : MonoBehaviour
    {
        private PlayerMovement playerMovement;
        private PlayerBattle playerBattle;
        private EnemyBattle enemyBattle;
        private EnemyMovement enemyMovement;
        private WorldState worldState;
        private DialogueManager dialogueManager;

        private void Awake()
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
            playerBattle = FindObjectOfType<PlayerBattle>();
            enemyBattle = FindObjectOfType<EnemyBattle>();
            enemyMovement = FindObjectOfType<EnemyMovement>();
            worldState = FindObjectOfType<WorldState>();
            dialogueManager = FindObjectOfType<DialogueManager>();
        }

        private void OnEnable()
        {
            ToggleMovement(false);
        }

        private void OnDisable()
        {
            ToggleMovement(true);
        }

        private void ToggleMovement(bool active)
        {
            if (playerMovement != null)
            {
                playerMovement.enabled = active;
            }
            if (playerBattle != null)
            {
                playerBattle.enabled = active;
            }
            if (enemyBattle != null)
            {
                enemyBattle.enabled = active;
            }
            if (enemyMovement != null)
            {
                enemyMovement.enabled = active;
            }
            if (worldState != null)
            {
                worldState.enabled = active;
            }
            if (dialogueManager != null)
            {
                dialogueManager.enabled = active;
            }
        }
    }
}
