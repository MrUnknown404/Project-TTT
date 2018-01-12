using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	private const string PLAYER_ID_PREFIX = "Player: ";
	private const string PROP_ID_PREFIX = "Prop: ";

	private static Dictionary<string, PlayerManager> players = new Dictionary<string, PlayerManager>();
	private static Dictionary<string, PropManager> props = new Dictionary<string, PropManager>();

	public static void RegisterPlayer(string _netID, PlayerManager _player) {
		string _playerID = PLAYER_ID_PREFIX + _netID;
		players.Add(_playerID, _player);
		_player.transform.name = _playerID;
	}

	public static void RegisterProp(string _netID, PropManager _prop) {
		string _propID = PROP_ID_PREFIX + _netID;
		props.Add(_propID, _prop);
		_prop.transform.name = _propID;
	}

	public static void UnRegisterPlayer(string _playerID) {
		players.Remove(_playerID);
	}

	public static PlayerManager GetPlayer(string _playerID) {
		return players[_playerID];
	}

	public static PropManager GetProp(string _propID) {
		return props[_propID];
	}

	private void OnGUI() {
		GUILayout.BeginArea(new Rect(10, 110, 200, 500));
		GUILayout.BeginVertical();

		foreach (string _playerID in GameManager.players.Keys) {
			GUILayout.Label(_playerID + " - " + GameManager.players[_playerID].transform.name);
		}

		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
