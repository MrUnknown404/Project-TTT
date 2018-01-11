using UnityEngine;

public class WeaponSwitchingController : MonoBehaviour {
	
	public int selectedWeapon = 0;

	public void DoUpdate() {
		int _previousSelectedWeapon = selectedWeapon;

		if (Input.GetAxis("Mouse_Scroll") > 0f) {
			if (selectedWeapon >= transform.childCount - 1) {
				selectedWeapon = 0;
			} else {
				selectedWeapon++;
			}
		} else if (Input.GetAxis("Mouse_Scroll") < 0f) {
			if (selectedWeapon <= 0) {
				selectedWeapon = transform.childCount - 1;
			} else {
				selectedWeapon--;
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			selectedWeapon = 0;
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			selectedWeapon = 1;
		} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			selectedWeapon = 2;
		} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			selectedWeapon = 3;
		}

		if (_previousSelectedWeapon != selectedWeapon) {
			SelectWeapon();
		}
	}

	public void SelectWeapon() {
		int i = 0;
		foreach (Transform weapon in transform) {
			if (i == selectedWeapon) {
				weapon.gameObject.SetActive(true);
			} else {
				weapon.gameObject.SetActive(false);
			}
			i++;
		}
	}
}
