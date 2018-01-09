using UnityEngine;

public class Weapon_Pistol : MonoBehaviour {

	//private static string weaponName = "Pistol";
	//private static float weaponDamage = 10f;
	private static float weaponRange = 100f;
	private static float weaponFirerate = 0.5f;
	private static int weaponAmountOfBullets = 1;
	private static bool isAuto = false;

	[Header("Camera Settings")]
	[SerializeField]
	private Camera cam;

	[Header("Misc")]
	[SerializeField]
	private LayerMask mask;

	private const string PLAYER_TAG = "Player";
	private const string PROP_TAG = "Prop";

	private float timeLeft = weaponFirerate;
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
			}
		} else if (isAuto == false) {
			if (Input.GetButtonDown("Mouse_Fire") && startTimer == false) {
				startTimer = true;
				gunCtrl.Shoot(weaponAmountOfBullets, weaponRange, PLAYER_TAG, PROP_TAG, cam, mask);
			}
		}

		//Timer
		if (startTimer == true) {
			timeLeft -= Time.fixedDeltaTime;
			timeLeft = Mathf.Clamp(timeLeft, 0, weaponFirerate);
			if (timeLeft == 0) {
				timeLeft = weaponFirerate;
				startTimer = false;
			}
		}
	}
}
