using UnityEngine;

public class WeaponBase : MonoBehaviour {

	[Header("Weapon Settings")]
	[SerializeField]
	private float weaponFireRate;
	[SerializeField]
	private string weaponName;
	[SerializeField]
	private int weaponDamage;
	[SerializeField]
	private int weaponRange;
	[SerializeField]
	private int weaponAmmo;
	[SerializeField]
	private int weaponAmmoMag;
	[SerializeField]
	private int weaponMaxAmmo;
	[SerializeField]
	private int weaponMaxAmmoMag;
	[SerializeField]
	private int weaponAmmountOfBullets;

	[Header("Misc Settings")]
	[SerializeField]
	private LayerMask mask;
	[SerializeField]
	private Camera cam;
	
	private WeaponController sCtrl;
	
	private void Start() {
		sCtrl = GetComponentInParent<WeaponController>();
	}

	private void Update() {
		if (Input.GetButtonDown("Mouse_Fire")) {
			sCtrl.Shoot(weaponRange, weaponDamage, weaponAmmountOfBullets, cam, mask);
		}
	}
}
