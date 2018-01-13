using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PropManager))]
public class RegisterProps : NetworkBehaviour {
	private void Start() {
		string _netID = GetComponent<NetworkIdentity>().netId.ToString();
		PropManager _prop = GetComponent<PropManager>();

		GameManager.RegisterProp(_netID, _prop);

		this.GetComponent<PropManager>().Setup();
	}
}
