using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShoot : WeaponController {

	bool isPlayerAlive;
	Player player;

	void Start(){
		GetComponent <Player> ().PlayerHealth.OnDeath += PlayerHealth_OnDeath;
		isPlayerAlive = true;
		player = GameManager.Instance.LocalPlayer;
	}

	void Update () {


		if (!isPlayerAlive)
			return;

		if (player.PlayerState.MoveState == PlayerState.EMoveState.SPRINTING ||
			player.PlayerState.MoveState == PlayerState.EMoveState.COVER) {
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

		if (GameManager.Instance.InputController.Reload &&
			player.PlayerState.MoveState != PlayerState.EMoveState.SPRINTING)
			ActiveWeapon.Reload ();
	}


	void PlayerHealth_OnDeath(){
		isPlayerAlive = false;
	}


}
