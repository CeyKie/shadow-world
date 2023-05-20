using UnityEngine;
using World.Models;

namespace Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField]
        private bool isShadowEnemy;

        private WorldModel worldModel;

        private void Awake()
        {
            worldModel = FindObjectOfType<WorldModel>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player") && worldModel.isShadowWorld == isShadowEnemy)
            {
                Destroy(gameObject);
            }
        }
    }
}
