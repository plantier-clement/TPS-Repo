using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadUI : MonoBehaviour {

	Player localPlayer;

	[SerializeField] GameObject reload;

	void Awake(){
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;

	}



	void HandleOnLocalPlayerJoined (Player player) {
		localPlayer = player;

	}
}
