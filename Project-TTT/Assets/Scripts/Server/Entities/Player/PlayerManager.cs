using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class PlayerManager : NetworkBehaviour {

	[SyncVar]
	private bool _isDead = false;
	public bool isDead {
		get {
			return _isDead;
		}
		protected set {
			_isDead = value;
		}
	}

	[SyncVar]
	private int currentHealth;
	private int maxHealth = 100;

	[SerializeField]
	private Behaviour[] disableOnDeath;
	private bool[] wasEnabled;

	public void Setup() {

		wasEnabled = new bool[disableOnDeath.Length];
		for (int i = 0; i < wasEnabled.Length; i++) {
			wasEnabled[i] = disableOnDeath[i].enabled;
		}

		SetDefaults();
	}

	private void Update() {
		if (!isLocalPlayer) {
			return;
		}

		//Kill Self
		if (Input.GetKeyDown(KeyCode.K)) {
			RpcTakeDamage(9999, "Debug");
		}
	}

	[ClientRpc]
	public void RpcTakeDamage(int _amount, string _name) {
		if (isDead == true) {
			return;
		}

		currentHealth -= _amount;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
		Debug.Log(System.Math.Round(Time.time, 2) + ": " + transform.name + " was shot by " + _name + " for " + _amount + " damage and now has " + currentHealth + " health");

		if (currentHealth <= 0) {
			Die(_name);
		}
	}

	private void Die(string _name) {
		isDead = true;
		Debug.Log(System.Math.Round(Time.time, 2) + ": " + transform.name + " was killed by " + _name + "!");
		Cursor.lockState = CursorLockMode.None;

		Rigidbody rb = this.GetComponent<Rigidbody>();
		rb.useGravity = false;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath[i].enabled = false;
		}

		Collider _col = GetComponentInChildren<Collider>();
		if (_col != null) {
			_col.enabled = false;
		}

		//Call Respawn
		StartCoroutine(Respawn(_name));
	}

	private IEnumerator Respawn(string _name) {
		yield return new WaitForSeconds(GameManager.instance.roundSettings.respawnTime);

		SetDefaults();
		Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
		transform.position = _spawnPoint.position;
		transform.rotation = _spawnPoint.rotation;

		Debug.Log(_name + " has respawned at " + NetworkManager.singleton.GetStartPosition());
	}

	public void SetDefaults() {
		isDead = false;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath[i].enabled = wasEnabled[i];
		}

		Collider _col = GetComponentInChildren<Collider>();
		if (_col != null) {
			_col.enabled = true;
		}

		currentHealth = maxHealth;
	}
}
