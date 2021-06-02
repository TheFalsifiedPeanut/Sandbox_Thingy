namespace GeorgeProject
{
    using UnityEngine;

    /// <summary>
    /// A class to handle player movement.
    /// </summary>
    public class PlayerMove : MonoBehaviour
    {
        // A layer mask for checking if the player is touching the ground. This works like a tag but is designed around physics. It is more performant and flexible. More on this next lesson.
        [SerializeField]
        LayerMask ground;

        // A bool to check if we are on the ground or not.
        bool onGround;
        // A reference to the player stats class.
        PlayerStats playerStats;
        // A reference to the player inputs class.
        PlayerInput playerInput;
        // A reference to the player rigidbody.
        Rigidbody rigid;

        void Start()
        {
            // Assign the player stats class.
            playerStats = GetComponent<PlayerStats>();
            // Assign the player inputs class.
            playerInput = GetComponent<PlayerInput>();
            // Assign the player rigidbody.
            rigid = GetComponent<Rigidbody>();

            // This is where we subscribe to the Jump Action. We pass it the Jump function from this clas, with no brackets. It will then be called from the Jump action when the Space key is pressed.
            playerInput.SubscribeToJump(Jump);
        }

        void Update()
        {
            // Call the movement.
            Move();
        }

        /// <summary>
        /// Moves the player relative to the players forward facing direction.
        /// </summary>
        void Move()
        {
            // This rotates the movement direction from world space to local space based on players rotation. The player input positions are multiplied by the players up rotation. More on the math behind this next lesson.
            Vector3 targetPosition = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * new Vector3(playerInput.GetXPosition(), 0, playerInput.GetZPosition());
            // Set the player's position to be the current position plus the target position. Move this at the current move speed and scale it with delta time.
            transform.position = Vector3.MoveTowards(transform.position, transform.position + targetPosition, playerStats.GetCurrentMoveSpeed() * Time.deltaTime);
        }

        /// <summary>
        /// Jumps the player using physics. Currently the player can strafe in the air. If we like we can modify that next lesson.
        /// </summary>
        void Jump()
        {
            // Checks if the player is on the ground before jumping.
            if (onGround)
            {
                // Using the rigidbody we add force to the player in the up direction relative to the player. This is multiplied by the jump height. We use force mode impulse to have it pulse the once.
                rigid.AddForce(transform.up * playerStats.GetCurrentJumpHeight(), ForceMode.Impulse);
            }
        }

        /// <summary>
        /// Checks if the ground was hit.
        /// </summary>
        /// <param name="collision"> The collider we hit. </param>
        void OnCollisionEnter(Collision collision)
        {
            // Here we check against the layermask we set earlier. This uses a bitwise operator. More on this next lesson.
            if ((1 << collision.transform.gameObject.layer & ground.value) != 0)
            {
                // If we did hit the ground set the bool to true.
                onGround = true;
            }
        }


        /// <summary>
        /// Checks if the ground was left.
        /// </summary>
        /// <param name="collision"> The collider we left. </param>
        void OnCollisionExit(Collision collision)
        {
            // Here we check against the layermask we set earlier again like above.
            if ((1 << collision.transform.gameObject.layer & ground.value) != 0)
            {
                // If we did leave the ground set the bool to false.
                onGround = false;
            }
        }
    }
}