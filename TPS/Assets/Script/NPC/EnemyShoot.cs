using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SharedExtensions;

[RequireComponent(typeof(EnemyPlayer))]
public class EnemyShoot : WeaponController {

	[SerializeField] float delayBetweenBursts;
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
		CrouchState ();
		shouldFire = true;

		GameManager.Instance.Timer.Add (EndBurst, Random.Range (burstDurationMin, burstDurationMax));
	}


	void EndBurst(){
		shouldFire = false;
		if (!enemyPlayer.EnemyHealth.IsAlive || !CanSeeTarget ())
			return;

		CheckReload ();
		if(CanSeeTarget ())
			GameManager.Instance.Timer.Add (StartBurst, delayBetweenBursts);
	}


	void CheckReload(){
		if (ActiveWeapon.Reloader.RoundsRemainingInClip == 0) //ways to expand this: if < 3 reload, but if enemy close skip reload
			ActiveWeapon.Reload ();
	}


	bool CanSeeTarget(){
		if (!transform.IsInLineOfSight (ActiveWeapon.AimTarget.position, 90, enemyPlayer.playerScanner.mask, Vector3.up)) {
			// target
			enemyPlayer.ClearTargetAndScan ();
			return false;
		}
		return true;
	}


	void CrouchState(){
		bool takeCover = Random.Range (0, 3) == 0; // 25% chance

		if (!takeCover)
			return;

		float distanceToTarget = Vector3.Distance (transform.position, ActiveWeapon.AimTarget.position);
		if (distanceToTarget > 15)
			enemyPlayer.EnemyState.CurrentMoveState = EnemyState.EMoveState.CROUCHING;

	}



}
