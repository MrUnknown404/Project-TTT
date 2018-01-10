using UnityEngine;

public class DebugHud:MonoBehaviour {

	public bool debug;

	private Weapon_AK47 ak;
	private Weapon_Pistol pistol;

	private void Awake() {
		ak = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Weapon_AK47>();
		pistol = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Weapon_Pistol>();
	}

	void OnGUI() {
		if (debug == true) {
			GUI.Label(new Rect(10, Screen.height - 20, 200, 100), "Ak47 Ammo: " + ak.weaponAmmo);
			GUI.Label(new Rect(10, Screen.height - 40, 200, 100), "Ak47 Mag: " + ak.weaponAmmoMag);
			GUI.Label(new Rect(10, Screen.height - 60, 200, 100), "Pistol Ammo: " + pistol.weaponAmmo);
			GUI.Label(new Rect(10, Screen.height - 80, 200, 100), "Pistol Mag: " + pistol.weaponAmmoMag);
		}
	}
}
