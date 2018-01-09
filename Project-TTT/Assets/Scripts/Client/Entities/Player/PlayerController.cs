using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[Header("Movement Settings:")]
	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 2f;
	[SerializeField]
	private float jumpForce = 500f;

	private PlayerMotor motor;
	private CapsuleCollider capsuleCollider;
	
	private void Start() {
		motor = GetComponent<PlayerMotor>();
		capsuleCollider = GetComponent<CapsuleCollider>();
	}

	private void Update() {
		//Get Values
		float _xMov = Input.GetAxisRaw("Key_Horizontal");
		float _zMov = Input.GetAxisRaw("Key_Vertical");
		float _yRot = Input.GetAxisRaw("Mouse_X");
		float _xRot = Input.GetAxisRaw("Mouse_Y");
		float _cameraRotationX = _xRot * lookSensitivity;

		Vector3 _jumpForce = Vector3.zero;
		Vector3 _movHorz = transform.right * _xMov;
		Vector3 _moveVet = transform.forward * _zMov;
		Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
		Vector3 _velocity = (_movHorz + _moveVet).normalized * speed;

		//Apply Values
		motor.Move(_velocity);
		motor.Rotate(_rotation);
		motor.RotateCamera(_cameraRotationX);

		if (Input.GetButtonDown("Jump") && is_grounded()) {
			_jumpForce = Vector3.up * jumpForce;
		}

		//Apply Jump Force
		motor.ApplyJump(_jumpForce);
	}

	private bool is_grounded() {
		float distance_to_ground = capsuleCollider.bounds.extents.y;
		return Physics.Raycast(transform.position, -Vector3.up, distance_to_ground + 0.25f);
	}
}
