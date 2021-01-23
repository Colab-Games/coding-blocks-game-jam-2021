using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool drawDebugRaycasts = true;   // Should the environment checks be visualized

	[Header("Movement Properties")]
	public float speed = 1f;                // Player speed
	public float coyoteDuration = .05f;     // How long the player can jump after falling
	public float maxFallSpeed = -5f;		// Max speed player can fall

	[Header("Jump Properties")]
	public float jumpForce = 1f;            // Initial force of jump
	public float jumpHoldForce = .3f;	    // Incremental force when jump is held
	public float jumpHoldDuration = .1f;    // How long the jump key can be held

	[Header("Environment Check Properties")]
    public float footOffset = .4f;			// X Offset of feet raycast
    public float groundDistance = .2f;      // Distance player is considered to be on the ground
	public LayerMask groundLayer;           // Layer of the ground

	[HideInInspector] public float horizontalMovement;
	[HideInInspector] public bool jump;
	[HideInInspector] public bool boostJump;

	Rigidbody2D rigidBody;
	Animator animator;

	bool isOnGround;
	bool isJumping;

	float jumpTime;                         // Variable to hold jump duration
	float coyoteTime;                       // Variable to hold coyote duration

	float originalXScale;                   // Original scale on X axis
	int direction = 1;                      // Direction player is facing

    void Start()
    {
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();

		originalXScale = transform.localScale.x;
	}

    void FixedUpdate()
    {
		PhysicsCheck();

		GroundMovement();
		MidAirMovement();

		UpdateAnimation();
    }

    void PhysicsCheck()
    {
        // Start assuming the player isn't on the ground
        isOnGround = false;

		// Cast rays for the left and right foot
		RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0f), Vector2.down, groundDistance);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0f), Vector2.down, groundDistance);

		// If either ray hit the ground, the player is on the ground
		if (leftCheck || rightCheck)
			isOnGround = true;
	}

	void GroundMovement()
    {
		float xVelocity = speed * horizontalMovement;

		// If the sign of the velocity and direction don't match, flip the character
		if (xVelocity * direction < 0f)
			FlipCharacterDirection();

		rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);

		// If the player is on the ground, extend the coyote time window
		if (isOnGround)
			coyoteTime = Time.time + coyoteDuration;
	}

	void MidAirMovement()
    {
		// If the player should jump AND the player isn't already jumping AND EITHER
		// the player is on the ground or within the coyote time window...
		if (jump && !isJumping && (isOnGround || coyoteTime > Time.time))
        {
			isOnGround = false;
			isJumping = true;

			// Record the time the player will stop being able to boost their jump
			jumpTime = Time.time + jumpHoldDuration;

			rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
		}
		else if (isJumping)
        {
			// Apply mid air boost
			if (boostJump)
				rigidBody.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);

			if (jumpTime <= Time.time)
				isJumping = false;
		}

		// If player is falling to fast, reduce the Y velocity to the max
		if (rigidBody.velocity.y < maxFallSpeed)
			rigidBody.velocity = new Vector2(rigidBody.velocity.x, maxFallSpeed);
	}

	void UpdateAnimation()
    {
		animator.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
		animator.SetBool("IsJumping", isJumping || (rigidBody.velocity.y > 0f));
		animator.SetBool("IsOnGround", isOnGround);
	}

	void FlipCharacterDirection()
	{
		// Turn the character by flipping the direction
		direction *= -1;

		// Record the current scale
		Vector3 scale = transform.localScale;

		// Set the X scale to be the original times the direction
		scale.x = originalXScale * direction;

		// Apply the new scale
		transform.localScale = scale;
	}


	// These two Raycast methods wrap the Physics2D.Raycast() and provide some extra
	// functionality
	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
	{
		// Call the overloaded Raycast() method using the ground layermask and return 
		// the results
		return Raycast(offset, rayDirection, length, groundLayer);
	}

	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
	{
		// Record the player's position
		Vector2 pos = transform.position;

		// Send out the desired raycasr and record the result
		RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

		// If we want to show debug raycasts in the scene...
		if (drawDebugRaycasts)
		{
			//...determine the color based on if the raycast hit...
			Color color = hit ? Color.red : Color.green;
			//...and draw the ray in the scene view
			Debug.DrawRay(pos + offset, rayDirection * length, color);
		}

		// Return the results of the raycast
		return hit;
	}
}
