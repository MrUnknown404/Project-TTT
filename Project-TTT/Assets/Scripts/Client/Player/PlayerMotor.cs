using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {
	
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private Vector3 cameraRotation = Vector3.zero;
	private Rigidbody rb;
	[SerializeField]
	private Camera cam;

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
	public void RotateCamera(Vector3 _cameraRotation) {
		cameraRotation = _cameraRotation;
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
	}

	//Perform Rotation
	private void PerformRotation() {
		rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
		if (cam != null) {
			cam.transform.Rotate(cameraRotation);
		}
	}
}
