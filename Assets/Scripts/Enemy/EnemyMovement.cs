using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField]
        private float[] positions;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private List<LayerMask> ignoredLayers;

        private const float MoveSpeed = 1.5f;

        private int index;
        private bool countUpwards;
        private float initPosition;
        private Vector2 nextPosition;
        private float objectYPosition;

        private void Awake()
        {
            index = 0;
            initPosition = transform.position.x;
            objectYPosition = transform.position.y;

            if (positions.Length > 0)
            {
                nextPosition = new Vector2(positions[0], objectYPosition);
            }
        }

        private void Update()
        {
            // Move();
        }

        private void Move()
        {
            if (positions.Length <= 0)
            {
                return;
            }

            var currentPosition = new Vector2(transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, nextPosition,
                Time.deltaTime * MoveSpeed);

            if (currentPosition != nextPosition)
            {
                return;
            }

            SetNextPosition();
            spriteRenderer.flipX = nextPosition.x > currentPosition.x;
        }

        private void SetNextPosition()
        {
            if (positions.Length - 1 == index)
            {
                countUpwards = false;
            }

            if (index <= 0)
            {
                countUpwards = true;
            }

            if (countUpwards)
            {
                index++;
            }
            else
            {
                index--;
            }

            nextPosition = new Vector2(initPosition + positions[index], objectYPosition);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player") || col.gameObject.layer == 7)
            {
                return;
            }

            SetNextPosition();
        }
    }
}