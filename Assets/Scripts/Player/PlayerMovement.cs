using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Character Settings")]
        [SerializeField]
        private SpriteRenderer characterSprite;
        
        [SerializeField]
        private float walkSpeed = 5f;
        
        [SerializeField]
        private float runSpeed = 1.5f;
        
        [SerializeField]
        private float jumpHeight = 5f;

        [Header("Animation & Sounds")]
        [SerializeField]
        private Animator characterAnimator;

        [Header("Grounding Detection")]
        [SerializeField]
        private Transform lowerContact;
        
        [SerializeField]
        private LayerMask detectionLayer;

        private readonly float groundCheckCircle = 0.2f;
        private readonly float jumpTimer = 0.3f;

        private readonly string jumpingAnimationFlag = "isJumping";
        private readonly string runningAnimationFlag = "isRunning";
        private readonly string idleAnimationFlag = "isIdle";

        private Rigidbody2D rigidbody2D;
        private float horizontalMovement;
        private float jumpCountdown;
        private float characterSpeed;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            SetMovementOfCharacter();
        }

        private void FixedUpdate()
        {
            MoveCharacter();
        }

        private void MoveCharacter()
        {
            horizontalMovement *= characterSpeed;
            rigidbody2D.velocity = new Vector2(horizontalMovement, rigidbody2D.velocity.y);
        }

        private void SetMovementOfCharacter()
        {
            horizontalMovement = Input.GetAxis("Horizontal");
            characterSprite.flipX = horizontalMovement < 0;
            characterSpeed = walkSpeed;

            // When the player presses the run (left shift) button
            // it affects the movement speed of the character and multiples the base speed
            // by the run speed
            if (Input.GetButton("Run"))
            {
                characterSpeed *= runSpeed;
            }

            Jump();
            SetupAnimations();
        }

        private void SetupAnimations()
        {
            var isGrounded = IsGrounded();
            var isJumping = !isGrounded;
            var isWalking = !isJumping && Input.GetButton("Horizontal");
            characterAnimator.SetBool(jumpingAnimationFlag, isJumping);
            characterAnimator.SetBool(runningAnimationFlag, isWalking);

            if (!isJumping && !isWalking)
            {
                characterAnimator.SetBool(idleAnimationFlag, true);
            }
            else
            {
                characterAnimator.SetBool(idleAnimationFlag, false);
            }
        }

        /// <summary>
        /// Jump controlling of the character.
        /// When the player presses the jump button longer, it lets
        /// the character jump higher (until the max time of longer jump is reached)
        /// </summary>
        private void Jump()
        {
            if (IsGrounded() && Input.GetButtonDown("Jump"))
            {
                rigidbody2D.velocity = Vector2.up * jumpHeight;
                jumpCountdown = jumpTimer;
            }

            if (Input.GetButton("Jump") && jumpCountdown > 0f)
            {
                jumpCountdown -= Time.deltaTime;
                rigidbody2D.velocity = Vector2.up * jumpHeight;
            }
        }

        private bool IsGrounded()
        {
          return Physics2D.OverlapCircle(lowerContact.position, groundCheckCircle, detectionLayer);
        }
    }
}