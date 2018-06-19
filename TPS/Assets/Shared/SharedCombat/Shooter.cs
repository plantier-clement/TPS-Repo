using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {


	[SerializeField] float rateOfFire;
	[SerializeField] Projectile projectile;
	[SerializeField] Transform hand;

	float nextFireAllowed;
	Transform muzzle;

	Transform audioFire;
	AudioController audioControllerFire;
	Transform audioReload;
	AudioController audioControllerReload;
	ParticleSystem muzzlePartSystem;

	WeaponRecoil m_WeaponRecoil;
	WeaponRecoil weaponRecoil {
		get {
			if (m_WeaponRecoil == null)
				m_WeaponRecoil = GetComponent <WeaponRecoil> ();
			return m_WeaponRecoil;
		}
	}

	public Transform AimTarget;
	public Vector3 AimTargetOffset;
	[HideInInspector] public bool canFire;
	[HideInInspector] public WeaponReloader Reloader;


	void Awake () {

		muzzle = transform.Find ("Model/Muzzle");
		Reloader = GetComponent <WeaponReloader> ();
		audioFire = transform.Find ("Audio/Fire");
		audioControllerFire = audioFire.GetComponentInChildren <AudioController> ();
		audioReload = transform.Find ("Audio/Reload");
		audioControllerReload = audioReload.GetComponentInChildren <AudioController> ();
		muzzlePartSystem = muzzle.GetComponent <ParticleSystem> ();
	
	}
	

	public virtual void Fire(){

		if (Time.time < nextFireAllowed)
			return;

		if (Reloader != null) {
			if (Reloader.IsReloading)
				return;
			if (Reloader.RoundsRemainingInClip == 0) {
				Reload ();
				return;
			}

			ExecuteFire ();
		}

	}
		

	private void ExecuteFire(){

		canFire = false;
		nextFireAllowed = Time.time + rateOfFire;
		bool isLocalPlayerControlled = AimTarget == null;

		if(!isLocalPlayerControlled)
			muzzle.LookAt (AimTarget.position + AimTargetOffset);


		Projectile newBullet = (Projectile) Instantiate (projectile, muzzle.position, muzzle.rotation);

		if (isLocalPlayerControlled) {
			Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
			RaycastHit hit;
			Vector3 targetPosition = ray.GetPoint (500);

			if (Physics.Raycast (ray, out hit))
				targetPosition = hit.point;


			newBullet.transform.LookAt (targetPosition + AimTargetOffset);
		}

		if (this.weaponRecoil)
			this.weaponRecoil.Activate ();

		Reloader.TakeFromClip (1);
		PlayFireEfects ();

		canFire = true;

	}


	public void Reload (){
		if (Reloader == null)
			return;

		if (Reloader.shotsFiredInClip == 0)
			return;

		Reloader.Reload ();
		audioControllerReload.Play ();

	}


	public void Equip(){
		transform.SetParent (hand);
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}


	public virtual void PlayFireEfects(){
		audioControllerFire.Play ();
		PlayMuzzleFlash ();
	}


	void PlayMuzzleFlash(){
		if (muzzlePartSystem == null)
			return;

		muzzlePartSystem.Play ();
	}
}