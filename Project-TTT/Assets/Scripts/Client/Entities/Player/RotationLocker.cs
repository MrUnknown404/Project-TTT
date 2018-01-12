using UnityEngine;

public class RotationLocker : MonoBehaviour {
	
	void LateUpdate() {
		if (transform.rotation != Quaternion.Euler(0f, 0f, 0f)) {
			transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		} else {
		}
	}
}
