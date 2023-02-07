using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] private float runSpeed = 40f;

	private CharacterController2D controller;
	private PlayerHealth playerHealth;
	private Animator animator;
	private float horizontalMove = 0f;
	private bool jump = false;
	private bool crouch = false;

	private void Start()
	{
		controller = GetComponent<CharacterController2D>();
		animator = GetComponentInChildren<Animator>();
		playerHealth = GetComponent<PlayerHealth>();
	}

	// Update is called once per frame
	void Update () {
		if (!playerHealth.IsDead)
		{
			horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

			animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

			if (Input.GetButtonDown("Jump"))
			{
				jump = true;
				animator.SetBool("IsJumping", true);
			}

			if (Input.GetButtonDown("Crouch"))
			{
				crouch = true;
			}
			else if (Input.GetButtonUp("Crouch"))
			{
				crouch = false;
			}
		}
	}

	public void OnLanding ()
	{
		animator.SetBool("IsJumping", false);
		
	}

	public void OnCrouching (bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate ()
	{
		// Move our character
		if (gameObject.GetComponent<PlayerHealth>().IsDead)
			return;
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
