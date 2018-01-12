using UnityEngine;
using UnityEngine.Networking;

public class WeaponController : NetworkBehaviour {
	private bool once = false;

	private const string PLAYER_TAG = "Player";
	private const string PROP_TAG = "Prop";
	
	public GameObject weaponMain;
	public GameObject weaponSecondary;
	public GameObject weaponGrenade;
	public GameObject weaponUnarmed;

	private enum HitType {
		Prop,
		Player
	};

	private enum EquipedWeaponType {
		MainWeapon,
		SecondaryWeapon,
		Grenade,
		Unarmed,
		nil
	}

	private enum EquipedWeaponTypeRender {
		MainWeapon,
		SecondaryWeapon,
		Grenade,
		Unarmed,
		nil
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
	private EquipedWeaponType equipedWeaponType;
	private EquipedWeaponTypeRender equipedWeaponTypeRender;

	private InputSlot inputSlot;
	private InSlotMain inSlotMain;
	private InSlotSecondary inSlotSecondary;
	private InSlotGrenade inSlotGrenade;

	private HitType hitType;
	
	private void Start() {
		weaponMain = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
		weaponSecondary = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(1).gameObject;
		weaponGrenade = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
		weaponUnarmed = this.gameObject.transform.GetChild(0).GetChild(0).GetChild(3).gameObject;
		equipedWeaponTypeRender = EquipedWeaponTypeRender.nil;
		CmdSetWeapons(EquipedWeaponType.MainWeapon);
		CmdSetWeapons(EquipedWeaponType.SecondaryWeapon);
		CmdSetWeapons(EquipedWeaponType.Grenade);
		CmdSetWeapons(EquipedWeaponType.Unarmed);
	}

	[Command]
	private void CmdSetWeapons(EquipedWeaponType _slotType) {
		if (_slotType == EquipedWeaponType.MainWeapon) {
			equipedWeaponType = EquipedWeaponType.MainWeapon;
			equipedWeaponTypeRender = EquipedWeaponTypeRender.MainWeapon;
		} else if (_slotType == EquipedWeaponType.SecondaryWeapon) {
			equipedWeaponType = EquipedWeaponType.SecondaryWeapon;
			equipedWeaponTypeRender = EquipedWeaponTypeRender.SecondaryWeapon;
		} else if (_slotType == EquipedWeaponType.Grenade) {
			equipedWeaponType = EquipedWeaponType.Grenade;
			equipedWeaponTypeRender = EquipedWeaponTypeRender.Grenade;
		} else if (_slotType == EquipedWeaponType.Unarmed) {
			equipedWeaponType = EquipedWeaponType.Unarmed;
			equipedWeaponTypeRender = EquipedWeaponTypeRender.Unarmed;
		}
		UpdateWeaponRender();
	}
	
	[Client]
	private void UpdateWeaponRender() {
		if (equipedWeaponTypeRender == EquipedWeaponTypeRender.MainWeapon && weaponMain != null) {
			RpcSetActive(true, false, false, false);
		} else if (equipedWeaponTypeRender == EquipedWeaponTypeRender.SecondaryWeapon && weaponSecondary != null) {
			RpcSetActive(false, true, false, false);
		} else if (equipedWeaponTypeRender == EquipedWeaponTypeRender.Grenade && weaponGrenade != null) {
			RpcSetActive(false, false, true, false);
		} else if (equipedWeaponTypeRender == EquipedWeaponTypeRender.Unarmed && weaponUnarmed != null) {
			RpcSetActive(false, false, false, true);
		}
	}

	[ClientRpc]
	private void RpcSetActive(bool _1, bool _2, bool _3, bool _4) {
		weaponMain.transform.GetChild(0).gameObject.SetActive(_1);
		weaponSecondary.transform.GetChild(0).gameObject.SetActive(_2);
		weaponGrenade.transform.GetChild(0).gameObject.SetActive(_3);
		weaponUnarmed.transform.GetChild(0).gameObject.SetActive(_4);
	}

	private void Update() {
		if (!isLocalPlayer) {
			return;
		}
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			CmdSetWeapons(EquipedWeaponType.MainWeapon);
			UpdateWeaponRender();
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			CmdSetWeapons(EquipedWeaponType.SecondaryWeapon);
			UpdateWeaponRender();
		} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			CmdSetWeapons(EquipedWeaponType.Grenade);
			UpdateWeaponRender();
		} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			CmdSetWeapons(EquipedWeaponType.Unarmed);
			UpdateWeaponRender();
		}
		if (weaponUnarmed != null && once == false) {
			once = true;
			UpdateWeaponRender();
		}
	}

	[Client]
	public void Shoot(int _range, int _damage, Camera _cam, LayerMask _mask) {
		RaycastHit _hit;
		if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out _hit, _range, _mask)) {
			if (_hit.collider.tag == PLAYER_TAG) {
				CmdPlayerShot(_hit.collider.transform.parent.name, _damage, HitType.Player);
			} else if (_hit.collider.tag == PROP_TAG) {
				CmdPlayerShot(_hit.collider.name, _damage, HitType.Prop);
			}
		}
	}
	
	[Command]
	private void CmdPlayerShot(string _hitID, int _damage, HitType hitType) {
		if (hitType == HitType.Player) {
			PlayerManager _player = GameManager.GetPlayer(_hitID);
			_player.TakeDamage(_damage, this.gameObject.name);
		} else if (hitType == HitType.Prop) {
			PropManager _prop = GameManager.GetProp(_hitID);
			_prop.TakeDamage(_damage, this.gameObject.name);
		} else {
			Debug.Log("PlayerShoot/CmdPlayerShot: Error ?else was triggered?");
		}
	}
}
