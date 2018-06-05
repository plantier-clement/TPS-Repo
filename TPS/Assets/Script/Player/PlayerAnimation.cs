﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {


	Animator animator;
	private PlayerAim m_PlayerAim;
	private PlayerAim PlayerAim {
		get {
			if (m_PlayerAim == null)
				m_PlayerAim = GameManager.Instance.LocalPlayer.PlayerAim;
			return m_PlayerAim;
		}
	}

	void Awake () {
		animator = GetComponentInChildren <Animator> ();

	}
	
	void Update () {

		animator.SetFloat ("Vertical", GameManager.Instance.InputController.Vertical);
		animator.SetFloat ("Horizontal", GameManager.Instance.InputController.Horizontal);


		animator.SetBool ("IsWalking", GameManager.Instance.InputController.IsWalking);
		animator.SetBool ("IsSprinting", GameManager.Instance.InputController.IsSprinting);
		animator.SetBool ("IsCrouching", GameManager.Instance.InputController.IsCrouched);

		animator.SetFloat ("AimAngle", PlayerAim.GetAngle ());
		animator.SetBool ("IsAiming", GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING ||
									GameManager.Instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING);
		
	}
}
