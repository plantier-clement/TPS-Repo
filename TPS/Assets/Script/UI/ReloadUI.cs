using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadUI : MonoBehaviour {


	[SerializeField] GameObject reloadUI;
	[SerializeField] GameObject reticle;

	Player localPlayer;
	WeaponReloader weaponReloader;
	PlayerShoot playerShoot;


	void Awake (){
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;

	}


	void HandleOnLocalPlayerJoined (Player player) {
		localPlayer = player;
		playerShoot = localPlayer.PlayerShoot;
		playerShoot.OnWeaponSwitched += HandleOnWeaponSwitched;

	}


	void HandleOnWeaponSwitched (Shooter activeWeapon)	{

		weaponReloader = activeWeapon.Reloader;
		weaponReloader.OnReloadStart += HandleOnReloadStart;
		weaponReloader.OnReloadEnd += HandleOnReloadEnd;

	}


	void HandleOnReloadStart ()	{
		ToggleVisibility ();

	}


	void HandleOnReloadEnd ()	{
		ToggleVisibility ();
	}


	void ToggleVisibility (){
		reloadUI.SetActive (!reloadUI.activeSelf);
	}


}
