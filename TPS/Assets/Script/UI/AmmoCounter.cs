using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour {


	[SerializeField] Text text;
	Player localPlayer;
	PlayerShoot playerShoot;
	WeaponReloader reloader;

	void Awake () {
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLoalPlayerJoined;

	}
		

	void HandleOnLoalPlayerJoined(Player player) {
		localPlayer = player;
		playerShoot = localPlayer.PlayerShoot;
		playerShoot.OnWeaponSwitched += HandleOnWeaponSwitched;
	
	}


	void HandleOnWeaponSwitched (Shooter activeWeapon) {
		reloader = activeWeapon.Reloader;
		reloader.OnAmmoChanged += HandleOnAmmoChanged;
		HandleOnAmmoChanged ();

	}


	void HandleOnAmmoChanged ()	{
		int amountInInventory = reloader.RoundsRemainingInInventory;
		int amountInClip = reloader.RoundsRemainingInClip;
		text.text = string.Format ("{0} / {1}", amountInClip, amountInInventory);

	}
		
}
