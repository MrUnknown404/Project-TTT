using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	private Camera cam;

	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private Vector3 jumpForce = Vector3.zero;

	private float cameraRotationX = 0f;
	private float cameraRotationLimit = 85f;
	private float currentCameraRotationX = 0f;

	private Rigidbody rb;

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

		private void FixedUpdate() {
		PerformMovement();
		PerformRotation();
	}

	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}

	public void Rotate(Vector3 _rot) {
		rotation = _rot;
	}

	public void RotateCamera(float _cameraRot) {
		cameraRotationX = _cameraRot;
	}

	public void Jump(Vector3 _jumpForce) {
		jumpForce = _jumpForce;
	}
	
	private void PerformMovement() {
		if (velocity != Vector3.zero) {
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}
		if (jumpForce != Vector3.zero) {
			rb.AddForce(jumpForce * Time.fixedDeltaTime, ForceMode.Acceleration);
		}
	}

	private void PerformRotation() {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if (cam != null) {
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
			
			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
		}
	}
}
