using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour {

	[SerializeField] Slider lifebar;
	[SerializeField] Text text;

	Player localPlayer;
	PlayerHealth playerHealth;


	void Awake () {
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;

	}


	void HandleOnLocalPlayerJoined (Player player)	{
		localPlayer = player;
		playerHealth = localPlayer.PlayerHealth;

		playerHealth.OnDamageReceived += HandleOnDamageReceived;
		HandleOnDamageReceived ();
	}


	void HandleOnDamageReceived ()	{
		lifebar.value = playerHealth.HitPointsRemaining;
		text.text = playerHealth.HitPointsRemaining.ToString ();
	
	}
	

}
