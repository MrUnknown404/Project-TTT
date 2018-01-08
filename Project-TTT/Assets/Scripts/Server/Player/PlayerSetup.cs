using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	private Camera offlineCamera;

	private void Start() {
		if (!isLocalPlayer) {
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable[i].enabled = false;
			}
		} else {
			offlineCamera = Camera.main;
			if (offlineCamera != null) {
				offlineCamera.gameObject.SetActive(false);
			}
		}
	}

	private void OnDisable() {
		if (offlineCamera != null) {
			offlineCamera.gameObject.SetActive(true);
		}
	}
}
