using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShoot : WeaponController {

	bool isPlayerAlive;


	void Start(){
		GetComponent <Player> ().PlayerHealth.OnDeath += PlayerHealth_OnDeath;
		isPlayerAlive = true;
	}

	void Update () {


		if (!isPlayerAlive)
			return;

		if (GameManager.Instance.LocalPlayer.PlayerState.MoveState == PlayerState.EMoveState.SPRINTING) {
			CanFire = false;
		} else {
			CanFire = true;
		}

		if (GameManager.Instance.InputController.Fire1 && CanFire)
			ActiveWeapon.Fire ();

		if (GameManager.Instance.InputController.MouseWheelUp)
			SwitchWeapon (1);

		if (GameManager.Instance.InputController.MouseWheelDown)
			SwitchWeapon (-1);

		if (GameManager.Instance.InputController.Reload)
			ActiveWeapon.Reload ();
	}


	void PlayerHealth_OnDeath(){
		isPlayerAlive = false;
	}


}
