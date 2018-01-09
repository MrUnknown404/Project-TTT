using UnityEngine;

public class RotationFix : MonoBehaviour {

	private int RotationLockedAt = 0;

	void LateUpdate() {
		if (transform.rotation != Quaternion.Euler(RotationLockedAt, RotationLockedAt, RotationLockedAt)) {
			transform.rotation = Quaternion.Euler(RotationLockedAt, RotationLockedAt, RotationLockedAt);
		} else {
		}
	}
}
