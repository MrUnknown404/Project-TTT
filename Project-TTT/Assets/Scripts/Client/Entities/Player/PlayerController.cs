using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : NetworkBehaviour {
	
	private float speed = 5f;
	private float lookSensitivity = 1.5f;
	private float jumpForce = 12500f;

	private bool lockCursor = true;

	private PlayerMotor motor;
	private BoxCollider boxCollider;

	private void Start() {
		motor = GetComponent<PlayerMotor>();
		boxCollider = this.gameObject.transform.GetComponentInChildren<BoxCollider>();
	}

	private void Update() {
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKeyDown(KeyCode.F) && lockCursor == true) {
			lockCursor = false;
		} else if (Input.GetKeyDown(KeyCode.F) && lockCursor == false) {
			lockCursor = true;
		}

		if (lockCursor == true) {
			Cursor.lockState = CursorLockMode.Locked;
		} else {
			Cursor.lockState = CursorLockMode.None;
		}

		float _xMov = Input.GetAxisRaw("Key_Horizontal");
		float _zMov = Input.GetAxisRaw("Key_Vertical");

		Vector3 _movHorz = transform.right * _xMov;
		Vector3 _movVert = transform.forward * _zMov;
		Vector3 _jumpForce = Vector3.zero;

		if (Input.GetButtonDown("Key_Jump") && IsGrounded()) {
			_jumpForce = Vector3.up * jumpForce;
		}

		Vector3 _velocity = (_movHorz + _movVert).normalized * speed;

		motor.Move(_velocity);
		motor.Jump(_jumpForce);

		if (lockCursor == true) {
			float _yRot = Input.GetAxisRaw("Mouse_X");
			float _xRot = Input.GetAxisRaw("Mouse_Y");

			Vector3 _rot = new Vector3(0f, _yRot, 0f) * lookSensitivity;
			float _cameraRotX = _xRot * lookSensitivity;

			motor.Rotate(_rot);
			motor.RotateCamera(_cameraRotX);
		} else {
			motor.Rotate(Vector3.zero);
			motor.RotateCamera(0f);
		}
	}

	private bool IsGrounded() {
		float distance_to_ground = boxCollider.bounds.extents.y;
		return Physics.Raycast(transform.position, -Vector3.up, distance_to_ground + 0.25f);
	}
}
