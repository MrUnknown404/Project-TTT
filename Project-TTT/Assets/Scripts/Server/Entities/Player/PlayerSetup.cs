using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlayerManager))]
public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	private string remoteLayerName = "RemotePlayer";
	
	private Camera offlineCamera;

	private void Start() {
		if (!isLocalPlayer) {
			DisableComponents();
			AssignRemoteLater();
		} else {
			offlineCamera = Camera.main;
			if (offlineCamera != null) {
				offlineCamera.gameObject.SetActive(false);
			}
		}
	}

	public override void OnStartClient() {
		base.OnStartClient();

		string _netID = GetComponent<NetworkIdentity>().netId.ToString();
		PlayerManager _player = GetComponent<PlayerManager>();

		GameManager.RegisterPlayer(_netID, _player);
	}

	private void AssignRemoteLater() {
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
		GameManager.UnRegisterPlayer(transform.name);
	}
}
