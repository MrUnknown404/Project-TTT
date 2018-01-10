using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GunController :NetworkBehaviour {

	private GameObject mainGun;
	private GameObject mainGunAim;
	private GameObject secondGun;
	private GameObject secondGunAim;

	private GameObject holderMainGun;
	private GameObject holderSecondGun;

	private bool didDoubleTap1 = false;
	private bool didDoubleTap2 = false;

	private float timeLeft = 0.1f;
	private float timeLeftMax = 0.01f;
	public bool startTimer = false;

	[Client]
	public void Shoot(int weaponAmountOfBullets, float weaponRange, string PLAYER_TAG, string PROP_TAG, Camera cam, LayerMask mask) {
		RaycastHit _hit;
		//if (currentGunAmmoMag != 0 && currentGunAmmoMag2 != 0) {
			for (int i = weaponAmountOfBullets; i < weaponAmountOfBullets * 2; i++) {
				if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weaponRange, mask)) {
					if (_hit.collider.tag == PLAYER_TAG | _hit.collider.tag == PROP_TAG) {
						CmdPlayerShot(_hit.collider.name);
					}
				}
			}
		//}
	}

	[Client]
	private void Update() {
		//Get Objects
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
		if (holderMainGun == null) {
			holderMainGun = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
		}
		if (holderSecondGun == null) {
			holderSecondGun = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
		}

		//Get Aim Key
		if (Input.GetButtonDown("Mouse_Aim")) {
			if (mainGun != null | mainGunAim != null) {
				mainGun.SetActive(false);
				mainGunAim.SetActive(true);
			}
		} else if (Input.GetButtonUp("Mouse_Aim")) {
			if (mainGun != null | mainGunAim != null) {
				mainGun.SetActive(true);
				mainGunAim.SetActive(false);
			}
		}

		if (Input.GetButtonDown("Mouse_Aim")) {
			if (secondGun != null | secondGunAim != null) {
				secondGun.SetActive(false);
				secondGunAim.SetActive(true);
			}
		} else if (Input.GetButtonUp("Mouse_Aim")) {
			if (secondGun != null | secondGunAim != null) {
				secondGun.SetActive(true);
				secondGunAim.SetActive(false);
			}
		}

		//Switch Weapons
		if (Input.GetButtonDown("Key_1")) {
			if (didDoubleTap1 == true) {
				holderMainGun.SetActive(true);
				holderSecondGun.SetActive(false);
			}
			StartCoroutine(CheckIfDoubleTap1(1.0f));
		} else if (Input.GetButtonDown("Key_2")) {
			if (didDoubleTap2 == true) {
				holderMainGun.SetActive(false);
				holderSecondGun.SetActive(true);
			}
			StartCoroutine(CheckIfDoubleTap2(1.0f));
		}

		//Reload
		//do later

		//Timer
		if (startTimer == true) {
			timeLeft -= Time.fixedDeltaTime;
			timeLeft = Mathf.Clamp(timeLeft, 0, timeLeftMax);
			if (timeLeft == 0) {
				timeLeft = timeLeftMax;
				startTimer = false;
			}
		}
	}

	[Client]
	private IEnumerator CheckIfDoubleTap1(float time) {
		didDoubleTap1 = true;
		yield return new WaitForSecondsRealtime(time);
		didDoubleTap1 = false;
	}
	[Client]
	private IEnumerator CheckIfDoubleTap2(float time) {
		didDoubleTap2 = true;
		yield return new WaitForSecondsRealtime(time);
		didDoubleTap2 = false;
	}

	[Command]
	private void CmdPlayerShot(string _ID) {
		Debug.Log(_ID + " has been shot");
	}
}
