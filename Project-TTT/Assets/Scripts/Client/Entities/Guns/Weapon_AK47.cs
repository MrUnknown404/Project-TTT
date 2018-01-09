using UnityEngine;

public class Weapon_AK47 : MonoBehaviour {
	
	//private string weaponName = "AK-47";
	//private float weaponDamage = 20f;
	private float weaponRange = 200f;
	private float weaponFireSpeed = 1.0f;
	private int weaponAmountOfBullets = 1;
	private bool isAuto = true;

	[Header("Camera Settings")]
	[SerializeField]
	private Camera cam;

	[Header("Misc")]
	[SerializeField]
	private LayerMask mask;

	private const string PLAYER_TAG = "Player";
	private const string PROP_TAG = "Prop";

	private float timeLeft;
	private bool startTimer = false;

	private GunController gunCtrl;

	private void Start() {
		if (cam == null) {
			Debug.LogError("Weapon_AK47: Camera Not Found!");
			this.enabled = false;
		}
		gunCtrl = gameObject.GetComponentInParent<GunController>();
	}

	private void Update() {
		//Fire
		if (isAuto == true) {
			if (Input.GetButton("Mouse_Fire") && startTimer == false) {
				startTimer = true;
				gunCtrl.Shoot(weaponAmountOfBullets, weaponRange, PLAYER_TAG, PROP_TAG, cam, mask);
				Debug.Log("1");
			}
		} else if (isAuto == false) {
			if (Input.GetButtonDown("Mouse_Fire")) {
				gunCtrl.Shoot(weaponAmountOfBullets, weaponRange, PLAYER_TAG, PROP_TAG, cam, mask);
			}
		}

		//Timer
		if (startTimer == true) {
			timeLeft -= Time.fixedDeltaTime;
			if (timeLeft < 0) {
				Debug.Log("2");
				timeLeft = weaponFireSpeed;
				startTimer = false;
			}
		}
	}
}
