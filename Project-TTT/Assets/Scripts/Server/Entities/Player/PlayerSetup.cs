using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	public string playerName = "Player: ";

	[SerializeField]
	Behaviour[] componentsToDisable;
	[SerializeField]
	string remoteLayerName = "RemotePlayer";

	private Camera offlineCamera;

	private void Start() {
		if (!isLocalPlayer) {
			DisableComponents();
			AssignRemoteLayer();
		} else {
			offlineCamera = Camera.main;
			if (offlineCamera != null) {
				offlineCamera.gameObject.SetActive(false);
			}
		}
		RegisterPlayer();
	}

	private void RegisterPlayer() {
		playerName = playerName + GetComponent<NetworkIdentity>().netId; //Remove line later
		string _ID = playerName;
		transform.name = _ID;
	}

	private void AssignRemoteLayer() {
		gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
	}

	private void DisableComponents() {
		for (int i = 0; i < componentsToDisable.Length; i++) {
			componentsToDisable[i].enabled = false;
		}
	}

	private void OnDisable() {
		if (offlineCamera != null) {
			offlineCamera.gameObject.SetActive(true);
		}
	}
}
