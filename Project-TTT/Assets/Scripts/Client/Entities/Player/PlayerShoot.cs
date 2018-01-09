using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

	public PlayerWeapon weaponSettings;
	
	[Header("Camera Settings")]
	[SerializeField]
	private Camera cam;

	[Header("Misc")]
	[SerializeField]
	private LayerMask mask;

	private const string PLAYER_TAG = "Player";
	private const string PROP_TAG = "Prop";

	private float timeLeft;
	private bool canFire = true;
	private bool startTimer = false;

	private void Start() {
		if (cam == null) {
			Debug.LogError("PlayerShoot: Camera Not Found!");
			this.enabled = false;
		}
		timeLeft = weaponSettings.weaponFireSpeed;
	}

	private void Update() {
		//Fire
		if (weaponSettings.isAuto == true) {
			if (Input.GetButton("Mouse_Fire") && canFire == true) {
				Shoot();
				canFire = false;
				startTimer = true;
				
			}
		} else if (weaponSettings.isAuto == false) {
			if (Input.GetButtonDown("Mouse_Fire")) {
				Shoot();
			}
		}
	}

	private void FixedUpdate() {
		//Timer
		if (startTimer == true) {
			timeLeft -= Time.deltaTime;
			if (timeLeft < 0) {
				timeLeft = weaponSettings.weaponFireSpeed;
				startTimer = false;
				canFire = true;
			}
		}
	}

	[Client]
	private void Shoot() {
		RaycastHit _hit;
		for (int i = weaponSettings.amountOfBullets; i < weaponSettings.amountOfBullets * 2; i++) {
			if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weaponSettings.weaponRange, mask)) {
				if (_hit.collider.tag == PLAYER_TAG | _hit.collider.tag == PROP_TAG) {
					CmdPlayerShot(_hit.collider.name);
				}
			}
		}
	}

	[Command]
	private void CmdPlayerShot(string _ID) {
		Debug.Log(_ID + " has been shot");
	}
}
