using UnityEngine;
using UnityEngine.Networking;

public class PropManager : NetworkBehaviour {

	private int maxHealth = 25;

	[SyncVar]
	private int currentHealth;

	private void Awake() {
		SetDefaults();
	}

	public void TakeDamage(int _amount) {
		currentHealth -= _amount;
		Debug.Log(System.Math.Round(Time.time, 2) + ": " + transform.name + " was shot by " + this.name + " for " + _amount + " of damage and now has " + currentHealth + " health");
	}

	public void SetDefaults() {
		currentHealth = maxHealth;
	}
}
