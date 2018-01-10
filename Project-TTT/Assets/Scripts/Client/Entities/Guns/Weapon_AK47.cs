using UnityEngine;

public class Weapon_AK47 : MonoBehaviour {
	
	//private static string weaponName = "Ak-47";
	//private static float weaponDamage = 20f;
	private static float weaponRange = 200f;
	private static float weaponFirerate = 0.3f;
	private static int weaponAmountOfBullets = 1;
	private static bool isAuto = true;

	private int weaponMaxAmmo = 120;
	private int weaponMaxAmmoMag = 30;
	public int weaponAmmo = 0;
	public int weaponAmmoMag = 0;
	private int shotsFires;

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

	private void Awake() {
		weaponAmmoMag = weaponMaxAmmoMag;
		weaponAmmo = weaponMaxAmmo;
	}

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
				if (weaponAmmoMag != 0) {
					weaponAmmoMag -= weaponAmountOfBullets;
					weaponAmmoMag = Mathf.Clamp(weaponAmmoMag, 0, weaponMaxAmmoMag);
					shotsFires += 1;
					startTimer = true;
					gunCtrl.Shoot(weaponAmountOfBullets, weaponRange, PLAYER_TAG, PROP_TAG, cam, mask);
				}
			}
		} else if (isAuto == false) {
			if (Input.GetButtonDown("Mouse_Fire") && startTimer == false) {
				if (weaponAmmoMag != 0) {
					weaponAmmoMag -= weaponAmountOfBullets;
					weaponAmmoMag = Mathf.Clamp(weaponAmmoMag, 0, weaponMaxAmmoMag);
					shotsFires += 1;
					Debug.Log(weaponAmmoMag);
					startTimer = true;
					gunCtrl.Shoot(weaponAmountOfBullets, weaponRange, PLAYER_TAG, PROP_TAG, cam, mask);
				}
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
