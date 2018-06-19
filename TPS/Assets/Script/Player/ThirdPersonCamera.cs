using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerState))]

public class ThirdPersonCamera : MonoBehaviour {

	[System.Serializable]
	public class CameraRig {
		public Vector3 CameraOffset;
		public float Damping;
		public float CrouchHeight;
	}
		
	[SerializeField] float camCollisionOffsetX;
	[SerializeField] float camCollisionOffsetZ;
	[SerializeField] CameraRig defaultCamera;
	[SerializeField] CameraRig aimCamera;
	[SerializeField] CameraRig coverCamera;


	Transform cameraLookTarget;
	Player localPlayer;
	CameraRig cameraRig;
	Vector3 targetPosition;
	Quaternion targetRotation;
	float targetHeight;

	void Awake () {
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
		cameraRig = defaultCamera;
	}


	void LateUpdate () {

		if (localPlayer == null)
			return;

		SelectCameraRig ();

		DefineTargetLocationAndRotation ();

		Vector3 collisionDestination = cameraLookTarget.position + localPlayer.transform.up * targetHeight - localPlayer.transform.forward * 0.5f;
		DetectCollision (collisionDestination, ref targetPosition);


		transform.position = Vector3.Lerp (transform.position, targetPosition, cameraRig.Damping * Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, cameraRig.Damping * Time.deltaTime);
 	}


	void HandleOnLocalPlayerJoined (Player player)	{
		localPlayer = player;
		cameraLookTarget = localPlayer.transform.Find ("AimingPivot");
	
		if (cameraLookTarget == null)
			cameraLookTarget = localPlayer.transform;
	}


	void SelectCameraRig(){
		cameraRig = defaultCamera;

		if (localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING || localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING)
			cameraRig = aimCamera;
	}


	void DefineTargetLocationAndRotation(){
		targetHeight = cameraRig.CameraOffset.y + (localPlayer.PlayerState.MoveState == PlayerState.EMoveState.CROUCHING ? cameraRig.CrouchHeight : 0);

		targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraRig.CameraOffset.z +
						localPlayer.transform.up * targetHeight +
						localPlayer.transform.right * cameraRig.CameraOffset.x;


		// camera is facing same direction as target but rotate if too close;
		// Quaternion targetRotation = Quaternion.LookRotation (cameraLookTarget.position - targetPosition, Vector3.up);

		// camera is always facing the same direction as player;
		// Quaternion targetRotation = localPlayer.transform.localRotation;


		targetRotation = cameraLookTarget.rotation;
	}


	void DetectCollision (Vector3 toTarget, ref Vector3 fromTarget){
		
		Debug.DrawLine (targetPosition, toTarget, Color.magenta);

		RaycastHit hit;
		if (Physics.Linecast (toTarget, fromTarget, out hit)) {

			if (hit.collider.gameObject.layer != 8) { // layer 8 = player
				Vector3 hitPoint = new Vector3 (hit.point.x + hit.normal.x * camCollisionOffsetX, hit.point.y, hit.point.z + hit.normal.z * camCollisionOffsetZ);
				fromTarget = new Vector3 (hitPoint.x, fromTarget.y, hitPoint.z);
			}
		}
			
	}

}
