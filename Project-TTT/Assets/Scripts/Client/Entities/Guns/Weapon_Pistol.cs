using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

	[Header("Pistol Settings")]
	public string weaponName = "Pistol";
	public float weaponDamage = 10f;
	public float weaponRange = 100f;
	public float weaponFireSpeed = 0.5f;
	public int amountOfBullets = 1;
	public bool isAuto = false;
}
