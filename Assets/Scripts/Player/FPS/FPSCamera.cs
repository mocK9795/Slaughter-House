using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCamera : MonoBehaviour
{
	public FPSPlayer player;

	[Header("Look")]
	public float sensitivity;
	public float smoothing;
	public Ease smoothingCurve;

	[Header("View Bobbing")]
    public float viewBobbing = 0.1f;
    public float viewBobbingSpeed = 1;
	float bobbingTime;

	private void Update()
	{
		if (player.movementVector.magnitude != 0)
		{
			bobbingTime = (bobbingTime + Time.deltaTime * viewBobbingSpeed) % 360;
			float bobbing = Mathf.Sin(bobbingTime * Mathf.Deg2Rad) * viewBobbing;
			transform.position += Vector3.up * bobbing;
		}
	}

	public void OnCameraLook(InputAction.CallbackContext context) {
		Vector2 value = context.ReadValue<Vector2>() * sensitivity;
		Vector3 rotation = new Vector3(-value.y, value.x) + transform.eulerAngles;
		if (rotation.x > 90 && rotation.x < 180) rotation.x = 90;
		if (rotation.x > 180 && rotation.x < 270) rotation.x = 270;

		transform.DORotate(rotation, smoothing).SetEase(smoothingCurve);
	}
}
