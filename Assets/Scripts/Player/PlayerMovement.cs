using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
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

        [Header("Grounding Detection")]
        [SerializeField]
        private Transform lowerContact;
        
        [SerializeField]
        private LayerMask detectionLayer;
        
        private static readonly int JumpingAnimation = Animator.StringToHash("isJumping");
        private static readonly int RunningAnimation = Animator.StringToHash("isRunning");
        private static readonly int IdleAnimation = Animator.StringToHash("isIdle");

        private const float GroundCheckCircle = 0.2f;
        private const float JumpTimer = 0.3f;
        private new Rigidbody2D rigidbody2D;
        private Animator characterAnimator;
        private float horizontalMovement;
        private float jumpCountdown;
        private float characterSpeed;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
            characterAnimator = GetComponent<Animator>();
        }

        private void OnDestroy()
        {
            ResetFlags();
        }

        private void OnDisable()
        {
            ResetFlags();
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
            // var rotation = horizontalMovement >= 0f ? Vector2.zero : new Vector2(0, -180);
            // gameObject.transform.Rotate(rotation);
            
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
            characterAnimator.SetBool(JumpingAnimation, isJumping);
            characterAnimator.SetBool(RunningAnimation, isWalking);

            if (!isJumping && !isWalking)
            {
                characterAnimator.SetBool(IdleAnimation, true);
            }
            else
            {
                characterAnimator.SetBool(IdleAnimation, false);
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
                jumpCountdown = JumpTimer;
            }

            if (Input.GetButton("Jump") && jumpCountdown > 0f)
            {
                jumpCountdown -= Time.deltaTime;
                rigidbody2D.velocity = Vector2.up * jumpHeight;
            }
        }

        private bool IsGrounded()
        {
          return Physics2D.OverlapCircle(lowerContact.position, GroundCheckCircle, detectionLayer);
        }

        private void ResetFlags()
        {
            characterAnimator.SetBool(JumpingAnimation, false);
            characterAnimator.SetBool(RunningAnimation, false);
            characterAnimator.SetBool(IdleAnimation, false);
        }
    }
}