using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPerson : MonoBehaviour
{
	public static bool IsActive = true;
	private CharacterController controller = null;
	private Vector3 direction = Vector3.zero;
	private float rotation = 0f;
	private const float GRAVITY = -9.81f;
	private float velocity = 0;
	private bool isJumping = false;
	private float jumpTime = 0;
	private int jumpCount = 0;
	private int jumpCount1 = 0;

	[SerializeField] private Transform cameraTransform;
	[SerializeField] private Animator animator;
	[SerializeField] private float speed = 5f;
	[SerializeField] private float sprintSpeed = 10f;
	[SerializeField] private float rotateSpeed = 12f;
	[SerializeField] private float jumpPower = 5f;
	[SerializeField] private float gravityMultiplier = 1f;


	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		controller = this.GetComponent<CharacterController>();
	}

	void Update()
	{
		if (!IsActive)
			return;
		GetDirection();
		Gravity();
		Rotate();
		Move();
		CheckJumping();
		Jump();
		SetGrounded();
	}
	private void GetDirection()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		//Debug.Log("horizontal = " + horizontal);
		//Debug.Log("vertical = " + vertical);
		//direction = new Vector3(horizontal, 0, vertical);
		direction = transform.forward * vertical;
		direction += transform.right * horizontal;

		if (animator != null)
		{
			bool isRunning = horizontal != 0 || vertical != 0;
			animator.SetBool("IsRunning", isRunning);
			animator.SetBool("IsRunBack", vertical < 0);
		}

		//if (horizontal != 0 || vertical != 0)
		//	animator.SetBool("IsRunning", true);
		//else
		//	animator.SetBool("IsRunning", false);
	}

	private void Gravity()
	{
		if (controller.isGrounded && velocity < 0)
		{
			velocity = -1;
		}
		else
		{
			velocity += GRAVITY * gravityMultiplier * Time.deltaTime;
		}
		
		direction.y = velocity;
	}

	private void Rotate()
	{
		float mouseX = Input.GetAxis("Mouse X");
		float mouseY = Input.GetAxis("Mouse Y");
		transform.Rotate(0f, mouseX * rotateSpeed, 0);
		rotation += mouseY * rotateSpeed;
		rotation = Mathf.Clamp(rotation, -60f, 60f);
		cameraTransform.localEulerAngles = new Vector3(-rotation, 0, 0);
	}

	private void Move()
	{
		float curSpeed = speed;
		if (Input.GetButton("Sprint"))
			curSpeed = sprintSpeed;
		controller.Move(direction * curSpeed * Time.deltaTime);
	}
	private void Jump()
	{
		if (!Input.GetButtonDown("Jump"))
			return;
#if hide
		if (!controller.isGrounded) //double jump
		{
			jumpCount++;
			if (jumpCount >= 2)
			{
				return;
			}
		}
		else
		{
			jumpCount = 0;
		}
#endif
		if (!controller.isGrounded)
			return;
		velocity += jumpPower;
		isJumping = true;
		if (animator != null)
		{
			animator.SetBool("IsJumping", true);
			animator.SetTrigger("IsJumpTrigger");
		}
	}

	private void SetGrounded()
	{
		if (animator != null)
		{
			animator.SetBool("IsGrounded", controller.isGrounded);
		}
	}

	private void CheckJumping()
	{
		if (isJumping)
		{
			//Debug.Log("!controller.isGrounded = " + !controller.isGrounded);
			//Debug.Log("jumpTime = " + (jumpTime > 1.0f));
			bool endJump = controller.isGrounded || jumpTime > 1.0f;
			if (endJump)
			{
				if (animator != null)
					animator.SetBool("IsJumping", false);
				isJumping = false;
				jumpTime = 0;
			}
			jumpTime += Time.deltaTime;
			//Debug.Log("jumpTime = " + jumpTime);
		}
	}

}