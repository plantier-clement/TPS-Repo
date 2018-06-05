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
		

	[SerializeField] CameraRig defaultCamera;
	[SerializeField] CameraRig aimCamera;


	Transform cameraLookTarget;
	Player localPlayer;
	CameraRig cameraRig;


	void Awake () {
		
		GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
		cameraRig = defaultCamera;
	}


	void LateUpdate () {

		if (localPlayer == null)
			return;
		
		cameraRig = defaultCamera;

		if (localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING || localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING)
			cameraRig = aimCamera;

		float targetHeight = cameraRig.CameraOffset.y + (localPlayer.PlayerState.MoveState == PlayerState.EMoveState.CROUCHING ? cameraRig.CrouchHeight : 0);

		Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraRig.CameraOffset.z +
								localPlayer.transform.up * targetHeight +
								localPlayer.transform.right * cameraRig.CameraOffset.x;


		// camera is facing same direction as target but rotate if too close;
		// Quaternion targetRotation = Quaternion.LookRotation (cameraLookTarget.position - targetPosition, Vector3.up);

		// camera is always facing the same direction as player;
 		// Quaternion targetRotation = localPlayer.transform.localRotation;


		Quaternion targetRotation = cameraLookTarget.rotation;

		transform.position = Vector3.Lerp (transform.position, targetPosition, cameraRig.Damping * Time.deltaTime);
		transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, cameraRig.Damping * Time.deltaTime);
 	}


	void HandleOnLocalPlayerJoined (Player player)	{
		localPlayer = player;
		cameraLookTarget = localPlayer.transform.Find ("cameraLookTarget");
	
		if (cameraLookTarget == null)
			cameraLookTarget = localPlayer.transform;
	}
}
