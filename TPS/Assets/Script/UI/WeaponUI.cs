using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {

	Player localPlayer;
	PlayerShoot playerShoot;
	WeaponReloader weaponReloader;
	[SerializeField] GameObject weaponSwitch;
	[SerializeField] GameObject reload;

	void Awake () {
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
	}


	void HandleOnLocalPlayerJoined (Player player) {
		localPlayer = player;

		playerShoot = localPlayer.PlayerShoot;
		playerShoot.OnWeaponStartSwitch += HandleOnWeaponStartSwitch;
		weaponSwitch.SetActive (false);



	//	weaponReloader = localPlayer.PlayerShoot.ActiveWeapon.Reloader;
	//	weaponReloader.OnReloadStart += HandleOnReloadStart;

	//	reload.SetActive (false);

	
	}


	void HandleOnWeaponStartSwitch () {
		weaponSwitch.SetActive (true);
		playerShoot.OnWeaponSwitched += HandleOnWeaponSwitched;

		//
	//	weaponReloader.OnReloadStart -= HandleOnReloadStart ();
	//	weaponReloader.OnReloadEnd -= HandleOnReloadEnd ();

	}


	void HandleOnWeaponSwitched (Shooter activeWeapon) {
		weaponSwitch.SetActive (false);

		//weaponReloader = activeWeapon.Reloader;
		//weaponReloader.OnReloadStart += HandleOnReloadStart;

	}


	void HandleOnReloadStart (Shooter activeWeapon)	{
		reload.SetActive (true);
		weaponReloader.OnReloadEnd += HandleOnReloadEnd;

	}


	void HandleOnReloadEnd (){
		reload.SetActive (false);

	}




}
