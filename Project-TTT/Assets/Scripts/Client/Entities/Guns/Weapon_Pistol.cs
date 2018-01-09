using UnityEngine;

public class Weapon_Pistol : MonoBehaviour {
	
	//private string weaponName = "Pistol";
	//private float weaponDamage = 10f;
	private float weaponRange = 100f;
	private float weaponFireSpeed = 0.5f;
	private int weaponAmountOfBullets = 1;
	private bool isAuto = false;

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

	private GunController gunCtrl;

	private void Start() {
		if (cam == null) {
			Debug.LogError("Weapon_Pistol: Camera Not Found!");
			this.enabled = false;
		}
		gunCtrl = gameObject.GetComponentInParent<GunController>();
	}

	private void Update() {
		//Fire
		if (isAuto == true) {
			if (Input.GetButton("Mouse_Fire") && canFire == true) {
				gunCtrl.Shoot(weaponAmountOfBullets, weaponRange, PLAYER_TAG, PROP_TAG, cam, mask);
				canFire = false;
				startTimer = true;

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
				timeLeft = weaponFireSpeed;
				startTimer = false;
				canFire = true;
			}
		}
	}
}
