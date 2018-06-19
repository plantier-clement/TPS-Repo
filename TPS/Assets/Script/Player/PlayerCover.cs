using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCover : MonoBehaviour {


	[SerializeField] LayerMask coverMask;

	int numberOfRays = 8;
	bool canTakeCover;
	bool isInCover;
	RaycastHit closestHit;

	bool isAiming {
		get {
			return GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING ||
			GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING;
		}
	}



	void Update () {

		if (!canTakeCover)
			return;

		if (isInCover) {
			if (GameManager.Instance.InputController.Cover || isAiming || GameManager.Instance.InputController.Vertical < 0) {
				ExitCover ();
				return;
			}
		}
	
		if (GameManager.Instance.InputController.Cover && !isInCover)
			EnterCover ();

	}


	void EnterCover(){
		FindCoverAroundPlayer ();
		if (closestHit.distance == 0)
			return;

		GameManager.Instance.EventBus.RaiseEvent ("CoverToggle");
		transform.rotation = Quaternion.LookRotation (closestHit.normal) * Quaternion.Euler (0, 180f, 0);
		isInCover = true;
	}


	void ExitCover(){
		GameManager.Instance.EventBus.RaiseEvent ("CoverToggle");
		isInCover = false;
	}


	internal void SetPlayerCoverAllowed(bool value){
		canTakeCover = value;

		if (!canTakeCover && isInCover) {
			ExitCover ();
		
			print ("setplayer allowed & exit cover");
		}
	}


	void FindCoverAroundPlayer(){
		closestHit = new RaycastHit ();

		float angleStep = 360 / numberOfRays;

		for (int i = 0; i < numberOfRays; i++) {
			Quaternion angle = Quaternion.AngleAxis (i * angleStep, transform.up);
			CheckClosestPoint (angle);

		}
		Debug.DrawLine (transform.position + Vector3.up * 0.3f, closestHit.point, Color.magenta, 0.5f);

	}


	void CheckClosestPoint (Quaternion angle) {
		RaycastHit hit;

		if (Physics.Raycast (transform.position + Vector3.up * 0.3f, angle * Vector3.forward, out hit, 5f, coverMask)) {
			if (closestHit.distance == 0 || hit.distance < closestHit.distance)
				closestHit = hit;
		}

	}


}
