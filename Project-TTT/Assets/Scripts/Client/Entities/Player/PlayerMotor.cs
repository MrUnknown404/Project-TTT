using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

	[Header("Camera Settings:")]
	[SerializeField]
	private Camera cam;
	[SerializeField]
	private float cameraRotatonLimit = 85f;
	private float cameraRotationX = 0f;
	private float currentCameraRotationX = 0f;

	private Vector3 jumpForce = Vector3.zero;
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;

	private Rigidbody rb;

	

	private void Start() {
		rb = GetComponent<Rigidbody>();
	}

	//Get Movement
	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}

	//Get Rotation
	public void Rotate(Vector3 _rotation) {
		rotation = _rotation;
	}

	//Get Camera Rotation
	public void RotateCamera(float _cameraRotationX) {
		cameraRotationX = _cameraRotationX;
	}

	//Get Jump
	public void ApplyJump(Vector3 _jumpForce) {
		jumpForce = _jumpForce;
	}

	//Run Physics
	private void FixedUpdate() {
		PerformMovement();
		PerformRotation();
	}

	//Perform Movement
	private void PerformMovement() {
		if (velocity != Vector3.zero) {
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}
		if (jumpForce != Vector3.zero) {
			rb.AddForce(jumpForce * Time.fixedDeltaTime, ForceMode.Acceleration);
		}
	}

	//Perform Rotation
	private void PerformRotation() {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if (cam != null) {
			currentCameraRotationX -= cameraRotationX;
			currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotatonLimit, cameraRotatonLimit);

			//Apply
			cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
		}
	}
}
