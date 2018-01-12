using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot:NetworkBehaviour {
	
	public PlayerWeapon weapon;

	[Header("Settings")]
	[SerializeField]
	private Camera cam;
	[SerializeField]
	private LayerMask mask;

	private const string PLAYER_TAG = "Player";
	private const string PROP_TAG = "Prop";

	private void Start() {
		if (cam == null) {
			Debug.Log("PlayerShoot: No Camera Found");
			this.enabled = false;
		}
	}

	private void Update() { //Need to redo with isAuto
		if (Input.GetButtonDown("Mouse_Fire")) {
			Shoot();
		}
	}

	[Client]
	private void Shoot() {
		RaycastHit _hit;
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask)) {
			if (_hit.collider.tag == PLAYER_TAG) {
				CmdPlayerShot(_hit.collider.transform.parent.name, weapon.damage, HitType.Player);
			} else if (_hit.collider.tag == PROP_TAG) {
				CmdPlayerShot(_hit.collider.name, weapon.damage, HitType.Prop);
			}
		}
	}

	private enum HitType {
		Prop,
		Player
	};

	private HitType hitType;

	[Command]
	private void CmdPlayerShot(string _hitID, int _damage, HitType hitType) {
		if (hitType == HitType.Player) {
			PlayerManager _player = GameManager.GetPlayer(_hitID);
			_player.TakeDamage(_damage);
		} else if (hitType == HitType.Prop) {
			PropManager _prop = GameManager.GetProp(_hitID);
			_prop.TakeDamage(_damage);
		} else {
			Debug.Log("PlayerShoot/CmdPlayerShot: Error ?else was triggered?");
		}
	}
}
