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

	private bool grounded = false;
	
	private PlayerMotor motor;
	private BoxCollider boxCollider;
	
	private void Start() {
		motor = GetComponent<PlayerMotor>();
		boxCollider = this.gameObject.transform.GetChild(2).GetComponent<BoxCollider>();
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
		
		if (Input.GetButtonDown("Jump") && IsGrounded()) {
			_jumpForce = Vector3.up * jumpForce;
		}

		//Apply Jump Force
		motor.ApplyJump(_jumpForce);
	}

	private bool IsGrounded() {
		float distance_to_ground = boxCollider.bounds.extents.y;
		return Physics.Raycast(transform.position, -Vector3.up, distance_to_ground + 0.25f);
	}
}
