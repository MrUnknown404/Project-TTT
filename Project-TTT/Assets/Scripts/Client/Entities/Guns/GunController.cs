using UnityEngine;
using UnityEngine.Networking;

public class GunController:NetworkBehaviour {
	/*
	 * Most of the code here i need to redo so ignore how messy it is
	 * when i redo it it should be a lot shorter aswell
	 */
	private GameObject mainGun;
	private GameObject mainGunAim;
	private GameObject secondGun;
	private GameObject secondGunAim;
	//private GameObject grenade;
	//private GameObject unarmed;

	private GameObject holderMainGun;
	private GameObject holderSecondGun;
	private GameObject holderGrenade;
	private GameObject holderUnarmed;
	
	//Weapon Enums
	private enum EquipedWeapon {
		MainWeapon,
		SecondaryWeapon,
		Grenades,
		Unarmed
	}

	private enum InSlotMain {
		AK47,
		nil
	}

	private enum InSlotSecondary {
		Pistol,
		nil
	}

	private enum InSlotGrenade {
		nil
	}

	private enum InputSlot {
		Main,
		Secondary,
		Grenade,
		Unarmed
	}

	private InputSlot inputSlot;

	private EquipedWeapon equipedWeapon;
	private InSlotMain inSlotMain;
	private InSlotSecondary inSlotSecondary;
	private InSlotGrenade inSlotGrenade;

	private float timeLeft = 0.1f;
	private float timeLeftMax = 0.01f;
	private bool startTimer = false;

	private WeaponSwitchingController wsc;

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

	private void Start() {
		if (!isLocalPlayer) {
			return;
		}
		WeaponChange(InputSlot.Main);
		WeaponChange(InputSlot.Secondary);
		WeaponChange(InputSlot.Grenade);
		WeaponChange(InputSlot.Unarmed);
		wsc = this.GetComponentInChildren<WeaponSwitchingController>();
		wsc.SelectWeapon();
	}
	
	private void EquipSelected(InputSlot _slot) {
		if (_slot == InputSlot.Main) {
			if (holderMainGun != null) {
				holderMainGun.SetActive(true);
				equipedWeapon = EquipedWeapon.MainWeapon;
			}
			if (holderSecondGun != null) {
				holderSecondGun.SetActive(false);
			}
			if (holderGrenade != null) {
				holderGrenade.SetActive(false);
			}
			if (holderUnarmed != null) {
				holderUnarmed.SetActive(false);
			}
		} else if (_slot == InputSlot.Secondary) {
			if (holderMainGun != null) {
				holderMainGun.SetActive(false);
			}
			if (holderSecondGun != null) {
				holderSecondGun.SetActive(true);
				equipedWeapon = EquipedWeapon.SecondaryWeapon;
			}
			if (holderGrenade != null) {
				holderGrenade.SetActive(false);
			}
			if (holderUnarmed != null) {
				holderUnarmed.SetActive(false);
			}
		} else if (_slot == InputSlot.Grenade) {
			if (holderMainGun != null) {
				holderMainGun.SetActive(false);
			}
			if (holderSecondGun != null) {
				holderSecondGun.SetActive(false);
			}
			if (holderGrenade != null) {
				holderGrenade.SetActive(true);
				equipedWeapon = EquipedWeapon.Grenades;
			}
			if (holderUnarmed != null) {
				holderUnarmed.SetActive(false);
			}
		} else if (_slot == InputSlot.Unarmed) {
			if (holderMainGun != null) {
				holderMainGun.SetActive(false);
			}
			if (holderSecondGun != null) {
				holderSecondGun.SetActive(false);
			}
			if (holderGrenade != null) {
				holderGrenade.SetActive(false);
			}
			if (holderUnarmed != null) {
				equipedWeapon = EquipedWeapon.Unarmed;
				holderUnarmed.SetActive(true);
			}
		}
	}
	
	private void DropSelected(EquipedWeapon _equipedWeapon) {
		if (_equipedWeapon == EquipedWeapon.MainWeapon) {
			Destroy(holderMainGun);
			//Spawn Pickup
			WeaponChange(InputSlot.Main);
		} else if (_equipedWeapon == EquipedWeapon.SecondaryWeapon) {
			Destroy(holderSecondGun);
			//Spawn Pickup
			WeaponChange(InputSlot.Secondary);
		} else if (_equipedWeapon == EquipedWeapon.Grenades) {
			Destroy(holderGrenade);
			//Spawn Pickup
			WeaponChange(InputSlot.Grenade);
		}
	}
	
	private void WeaponChange(InputSlot _slot) {
		if (_slot == InputSlot.Main) {
			if (mainGun == null) {
				mainGun = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
			}
			if (mainGunAim == null) {
				mainGunAim = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(1).gameObject;
			}
			if (holderMainGun == null) {
				holderMainGun = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
			} else {
				holderMainGun = null;
			}
		} else if (_slot == InputSlot.Secondary) {
			if (secondGun == null) {
				secondGun = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).gameObject;
			}
			if (secondGunAim == null) {
				secondGunAim = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(1).gameObject;
			}
			if (holderSecondGun == null) {
				holderSecondGun = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
			} else {
				holderSecondGun = null;
			}
		} else if (_slot == InputSlot.Grenade) {
			if (holderGrenade == null) {
				holderGrenade = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
			} else {
				holderGrenade = null;
			}
			Debug.Log("GunCtrl: InputSlot.Grenade has not been setup yet");
		} else if (_slot == InputSlot.Unarmed) {
			if (holderUnarmed == null) {
				holderUnarmed = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
			} else {
				holderUnarmed = null;
			}
			Debug.Log("GunCtrl: InputSlot.Unarmed has not been setup yet");
		}
	}

	private void Aim(EquipedWeapon _equipedWeapon, bool _resetAim) {
		if (!isLocalPlayer) {
			return;
		}
		if (_equipedWeapon == EquipedWeapon.MainWeapon && _resetAim == false) {
			mainGun.SetActive(false);
			mainGunAim.SetActive(true);
		} else if (_equipedWeapon == EquipedWeapon.SecondaryWeapon && _resetAim == false) {
			secondGun.SetActive(false);
			secondGunAim.SetActive(true);
		}
		if (_equipedWeapon == EquipedWeapon.MainWeapon && _resetAim == true) {
			mainGun.SetActive(true);
			mainGunAim.SetActive(false);
		} else if (_equipedWeapon == EquipedWeapon.SecondaryWeapon && _resetAim == true) {
			secondGun.SetActive(true);
			secondGunAim.SetActive(false);
		}
	}
	
	private void Update() {
		if (!isLocalPlayer) {
			return;
		}
		wsc.DoUpdate();
		//Get Aim Key
		if (Input.GetButtonDown("Mouse_Aim")) {
			if (mainGunAim != null) {
				Aim(EquipedWeapon.MainWeapon, false);
			}
		} else if (Input.GetButtonUp("Mouse_Aim")) {
			if (mainGun != null) {
				Aim(EquipedWeapon.MainWeapon, true);
			}
		}
		if (Input.GetButtonDown("Mouse_Aim")) {
			if (secondGunAim != null) {
				Aim(EquipedWeapon.SecondaryWeapon, false);
			}
		} else if (Input.GetButtonUp("Mouse_Aim")) {
			if (secondGun != null) {
				Aim(EquipedWeapon.SecondaryWeapon, true);
			}
		}

		//Switch Weapons
		/*
		if (Input.GetButtonDown("Key_1")) {
			EquipSelected(InputSlot.Main); //StartCoroutine(CheckIfDoubleTap(1.0f));
		} else if (Input.GetButtonDown("Key_2")) {
			EquipSelected(InputSlot.Secondary);
		} else if (Input.GetButtonDown("Key_3")) {
			EquipSelected(InputSlot.Grenade);
		} else if (Input.GetButtonDown("Key_4")) {
			EquipSelected(InputSlot.Unarmed);
		}*/

		//Drop
		if (Input.GetButtonDown("Key_Drop")) {
			DropSelected(equipedWeapon);
		}

		//Reload (will do later)
		//if (Input.GetButtonDown("Key_Reload")) {
		//do stuff
		//}

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

	//i might need this for later so im saving it here!
	//[Client]
	//private IEnumerator CheckIfDoubleTap(float time) {
	//	didDoubleTap1 = true;
	//	yield return new WaitForSecondsRealtime(time);
	//	didDoubleTap1 = false;
	//}

	[Command]
	private void CmdPlayerShot(string _ID) {
		Debug.Log(_ID + " has been shot");
	}
}
