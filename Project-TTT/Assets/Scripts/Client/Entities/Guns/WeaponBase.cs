using UnityEngine;

public class WeaponBase : MonoBehaviour {

	[Header("Weapon Settings")]
	[SerializeField]
	private int weaponDamage;
	[SerializeField]
	private int weaponRange;
	[SerializeField]
	private float weaponFireRate;
	[SerializeField]
	private string weaponName;
	[SerializeField]
	private int ammo;
	[SerializeField]
	private int ammoMag;
	[SerializeField]
	private int maxAmmo;
	[SerializeField]
	private int maxAmmoMag;

	[Header("Misc Settings")]
	[SerializeField]
	private LayerMask mask;
	[SerializeField]
	private Camera cam;
	
	private WeaponController sCtrl;

	private GameObject mainWeapon;
	private GameObject secondaryWeapon;
	private GameObject grenade;
	private GameObject unarmed;

	private void Start() {
		sCtrl = GetComponentInParent<WeaponController>();
	}

	private void Update() {
		if (Input.GetButtonDown("Mouse_Fire")) {
			sCtrl.Shoot(weaponRange, weaponDamage, cam, mask);
		}
	}
}
