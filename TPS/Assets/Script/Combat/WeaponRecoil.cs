using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class WeaponRecoil : MonoBehaviour {

	[System.Serializable] public struct Layer {
	
		public AnimationCurve curve;
		public Vector3 direction;
	
	}

	[SerializeField] Layer[] layers;
	[SerializeField] float recoilCooldown;

	[SerializeField] float speedToMaxSpread;
	[SerializeField] float speedToMinSpread;
	[SerializeField] float maxSpread;
	[SerializeField] float recoilCamOffset;


	float nextRecoilCooldown;
	float recoilActiveTime;
	bool isRecoilActive = false;


	Shooter m_Shooter;
	Shooter shooter {
		get {
			if (m_Shooter == null)
				m_Shooter = GetComponent <Shooter> ();
			return m_Shooter;
		}
	}

	Crosshair m_Crosshair;
	Crosshair crosshair {
		get {
			if (m_Crosshair == null)
				m_Crosshair = GameManager.Instance.LocalPlayer.PlayerAim.GetComponentInChildren <Crosshair> ();
			return m_Crosshair;
		}
	}

	PlayerAim m_PlayerAim;
	PlayerAim playerAim {
		get {
			if (m_PlayerAim == null)
				m_PlayerAim = GameManager.Instance.LocalPlayer.PlayerAim;
			return m_PlayerAim;
		}
	}


	void Update(){
	




		if (isRecoilActive) {

			if (nextRecoilCooldown > Time.time) {
				// firing
				recoilActiveTime += Time.deltaTime;
				recoilActiveTime = Mathf.Clamp (recoilActiveTime, 0f, 0.8f);

				Vector3 recoilAmount = Vector3.zero;

				for (int i = 0; i < layers.Length; i++)
					recoilAmount += layers [i].direction * layers [i].curve.Evaluate (getPercentage (speedToMaxSpread));
			

				this.shooter.AimTargetOffset = Vector3.Lerp (shooter.AimTargetOffset, shooter.AimTargetOffset + recoilAmount, maxSpread * Time.deltaTime);
				this.crosshair.ApplyScale (getPercentage (speedToMaxSpread) * maxSpread);
				this.playerAim.SetRotation (recoilCamOffset);

			} else {
				// not firing
				recoilActiveTime -= Time.deltaTime;
				this.crosshair.ApplyScale (getPercentage (speedToMinSpread) * maxSpread);

				if (recoilActiveTime < 0) {
					recoilActiveTime = 0;
				}

				if (recoilActiveTime == 0) {
					this.shooter.AimTargetOffset = Vector3.zero;
					this.crosshair.ApplyScale (0);
					isRecoilActive = false;
				}
			}
		}
	}



	public void Activate(){
		nextRecoilCooldown = Time.time + recoilCooldown;
		isRecoilActive = true;
	}


	float getPercentage(float speed){
		float percentage = recoilActiveTime / speed;
		return Mathf.Clamp01 (percentage);
	}

}
