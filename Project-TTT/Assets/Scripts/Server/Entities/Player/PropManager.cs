using UnityEngine;
using UnityEngine.Networking;

public class PropManager : NetworkBehaviour {

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
	private int maxHealth = 25;

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

		Rigidbody rb = this.GetComponent<Rigidbody>();
		rb.useGravity = false;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath[i].enabled = false;
		}

		Collider _col = GetComponent<Collider>();
		if (_col != null) {
			_col.enabled = false;
		}
	}
	
	private void SetDefaults() {
		isDead = false;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath[i].enabled = wasEnabled[i];
		}

		Collider _col = GetComponent<Collider>();
		if (_col != null) {
			_col.enabled = true;
		}

		currentHealth = maxHealth;
	}
}
