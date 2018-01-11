using UnityEngine;
using UnityEngine.Networking;

public class DebugHud : NetworkBehaviour {

	private bool debug = true;

	private Weapon_AK47 ak;
	private Weapon_Pistol pistol;
	
	private void Awake() {
		ak = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Weapon_AK47>();
		pistol = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Weapon_Pistol>();
	}
	
	void OnGUI() {
		if (debug == true && (ak != null | pistol != null)) {
			GUI.Label(new Rect(10, Screen.height - 20, 500, 100), "Ak47 Ammo: " + ak.weaponAmmo + " / " + ak.weaponMaxAmmo);
			GUI.Label(new Rect(10, Screen.height - 40, 500, 100), "Ak47 Mag: " + ak.weaponAmmoMag + " / " + ak.weaponMaxAmmoMag);
			GUI.Label(new Rect(10, Screen.height - 60, 500, 100), "Pistol Ammo: " + pistol.weaponAmmo + " / " + pistol.weaponMaxAmmo);
			GUI.Label(new Rect(10, Screen.height - 80, 500, 100), "Pistol Mag: " + pistol.weaponAmmoMag + " / " + pistol.weaponMaxAmmoMag);
		}
	}
}
