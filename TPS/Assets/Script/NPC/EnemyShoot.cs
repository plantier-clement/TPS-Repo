using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPlayer))]
public class EnemyShoot : WeaponController {

	[SerializeField] float shootingSpeed;
	[SerializeField] float burstDurationMin;
	[SerializeField] float burstDurationMax;

	EnemyPlayer enemyPlayer;

	bool shouldFire;



	void Start(){
		enemyPlayer = GetComponent <EnemyPlayer> ();
		enemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;

	}


	void EnemyPlayer_OnTargetSelected (Player target) {
		
		ActiveWeapon.AimTarget = target.transform;
		ActiveWeapon.AimTargetOffset = Vector3.up * 1.25f;
		StartBurst ();
	}


	void Update(){
		if (!shouldFire || !CanFire || !enemyPlayer.EnemyHealth.IsAlive)
			return;
	
		ActiveWeapon.Fire ();
	}


	void StartBurst(){
		if (!enemyPlayer.EnemyHealth.IsAlive)
			return;

		CheckReload ();
		shouldFire = true;

		GameManager.Instance.Timer.Add (EndBurst, Random.Range (burstDurationMin, burstDurationMax));
	}

	void EndBurst(){
		shouldFire = false;
		if (!enemyPlayer.EnemyHealth.IsAlive)
			return;

		CheckReload ();
		GameManager.Instance.Timer.Add (StartBurst, shootingSpeed);
	}

	void CheckReload(){
		if (ActiveWeapon.Reloader.RoundsRemainingInClip == 0) //ways to expand this: if < 3 reload, but if enemy close skip reload
			ActiveWeapon.Reload ();
	}
}
