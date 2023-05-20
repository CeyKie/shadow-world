using UnityEngine;

namespace Player
{
    public class PlayerHit : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("Hit");
            }
        }
    }
}
