using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour {

	private int maxHealth = 100;

	[SyncVar]
	private int currentHealth;

	private void Awake() {
		SetDefaults();
	}

	public void TakeDamage(int _amount, string _name) {
		currentHealth -= _amount;
		Debug.Log(System.Math.Round(Time.time, 2) + ": " + transform.name + " was shot by " + _name + " for " + _amount + " damage and now has " + currentHealth + " health");
	}

	public void SetDefaults() {
		currentHealth = maxHealth;
	}
}
