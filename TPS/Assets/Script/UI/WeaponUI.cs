using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {

	Player localPlayer;
	PlayerShoot playerShoot;
	WeaponReloader weaponReloader;
	[SerializeField] GameObject weaponSwitch;


	void Awake () {
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
	}


	void HandleOnLocalPlayerJoined (Player player) {
		localPlayer = player;

		playerShoot = localPlayer.PlayerShoot;
		playerShoot.OnWeaponStartSwitch += HandleOnWeaponStartSwitch;
		weaponSwitch.SetActive (false);

	
	}


	void HandleOnWeaponStartSwitch () {
		weaponSwitch.SetActive (true);
		playerShoot.OnWeaponSwitched += HandleOnWeaponSwitched;

	}


	void HandleOnWeaponSwitched (Shooter activeWeapon) {
		weaponSwitch.SetActive (false);

	}




}
