using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;
	[SerializeField]
	private float lookSensitivity = 2f;
	private PlayerMotor motor;

	private void Start() {
		motor = GetComponent<PlayerMotor>();
	}

	private void Update() {
		//Get Movement
		float _xMov = Input.GetAxisRaw("Key_Horizontal");
		float _zMov = Input.GetAxisRaw("Key_Vertical");

		Vector3 _movHorz = transform.right * _xMov;
		Vector3 _moveVet = transform.forward * _zMov;

		//Final Movement
		Vector3 _velocity = (_movHorz + _moveVet).normalized * speed;

		//Apply Movement
		motor.Move(_velocity);

		//Calculate Rotation
		float _yRot = Input.GetAxisRaw("Mouse X");

		Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

		//Apply Rotation
		motor.Rotate(_rotation);

		//Calculate Camera Rotation
		float _xRot = Input.GetAxisRaw("Mouse Y");

		Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;

		//Apply Rotation
		motor.RotateCamera(_cameraRotation);
	}
}
