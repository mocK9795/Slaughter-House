using static Functions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSPlayer : MonoBehaviour {

	#region Motion Parameters
	[Header("Gravity")]
	public float gravity = 10;
	public Vector3 gravityDirection = new Vector3(0, -1, 0);

	[Header("Movement")]
	public float movementSpeed;
	public float movementSpeedRight = 3;
	[Tooltip("Approximatly The Time Duration In Which The Player Stops")]
	public float friction;

	[Header("Jumping")]
	public float jumpForce;

	[Header("Rotation")]
	[Tooltip("Measured From The Origin Of The Player")]
	public float groundedBuffer = 1.1f;
	[Range(0f, 1f), Tooltip("0-90 Degree")]
	public float maxFloorAngle;
	public float normalAngleChangeTime = 0.1f;
	public LayerMask playerMask;
	#endregion

	[Header("Components")]
	public Rigidbody body;
	public FPSCamera camera;

	bool isGrounded;
	RaycastHit floor;
	Vector3 velocity;
	[Tooltip("Z Component Is Ignored")]
	public Vector3 movementVector { get; private set; }

	void Start () {
	
	}

	private void Update() {
		UpdateIsGrounded();

		SetTransformUp();
		if (movementVector.magnitude == 0) LerpMovementVelocity0();
		else ApplyMovementVector();
		ApplyGravity();

		body.linearVelocity = velocity;
	}

	#region Velocity Funcitons
	void ApplyGravity()
	{
		if (isGrounded) {
			float downVelocity = Vector3.Dot(transform.up * -1, velocity);
			if (downVelocity < 0) return; 
			velocity += downVelocity * transform.up; return;
		}

		velocity += gravityDirection * gravity * Time.deltaTime;
	}
	void LerpMovementVelocity0()
	{
		Vector3 lerpTarget = RemovePlaneComponent(velocity, transform.up);
		velocity = Vector3.Lerp(velocity, lerpTarget, Mathf.Clamp01(friction * Time.deltaTime));
	}
	void ApplyMovementVector()
	{
		velocity = RemovePlaneComponent(velocity, transform.up);
		Vector3 forwardValue = ProjectOntoPlane(camera.transform.forward, transform.up).normalized;
		Vector3 rightValue = ProjectOntoPlane(camera.transform.right, transform.up).normalized;
		velocity += (movementSpeed * forwardValue * movementVector.y + movementSpeedRight * rightValue * movementVector.x);
	}
	#endregion
	#region Floor & Normal
	void SetTransformUp()
	{
		bool validFloorNormal = floor.collider && Vector3.Dot(floor.normal, gravityDirection * -1) < maxFloorAngle;
		
		var rotation = new Quaternion();

		if (validFloorNormal)
			rotation.SetLookRotation(transform.forward, floor.normal);
		else if (!floor.collider) 
			rotation.SetLookRotation(transform.forward, gravityDirection * -1);
		
		transform.DORotateQuaternion(rotation, normalAngleChangeTime);
	}
	void UpdateIsGrounded() {
		Ray ray = new Ray(transform.position, transform.up * -1);
		Physics.Raycast(ray, out floor, groundedBuffer, playerMask);
		isGrounded = floor.collider;
	}
	#endregion
	#region Input Functions
	public void OnJump(InputAction.CallbackContext context) {
		if (isGrounded && context.canceled)
		velocity += transform.up * jumpForce;
	}

	public void OnMove(InputAction.CallbackContext context) {
		movementVector = context.ReadValue<Vector2>();
	}
	#endregion
}