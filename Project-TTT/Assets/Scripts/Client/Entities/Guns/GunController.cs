using UnityEngine;
using UnityEngine.Networking;

public class GunController :NetworkBehaviour {

	private GameObject mainGun;
	private GameObject mainGunAim;
	private GameObject secondGun;
	private GameObject secondGunAim;

	[Client]
	public void Shoot(int weaponAmountOfBullets, float weaponRange, string PLAYER_TAG, string PROP_TAG, Camera cam, LayerMask mask) {
		RaycastHit _hit;
		for (int i = weaponAmountOfBullets; i < weaponAmountOfBullets * 2; i++) {
			if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weaponRange, mask)) {
				if (_hit.collider.tag == PLAYER_TAG | _hit.collider.tag == PROP_TAG) {
					CmdPlayerShot(_hit.collider.name);
				}
			}
		}
	}

	[Client]
	private void Update() {
		if (mainGun == null) {
			mainGun = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
		}
		if (mainGunAim == null) {
			mainGunAim = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject;
		}
		if (secondGun == null) {
			secondGun = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject;
		}
		if (secondGunAim == null) {
			secondGunAim = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).gameObject;
		}

		if (Input.GetButtonDown("Mouse_Aim")) {
			mainGun.SetActive(false);
			mainGunAim.SetActive(true);
		} else if (Input.GetButtonUp("Mouse_Aim")) {
			mainGun.SetActive(true);
			mainGunAim.SetActive(false);
		}
	}

	[Command]
	private void CmdPlayerShot(string _ID) {
		Debug.Log(_ID + " has been shot");
	}
}
