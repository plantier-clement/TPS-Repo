using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {


	public enum EMoveState{
		WALKING,
		RUNNING,
		CROUCHING,
		SPRINTING
	}

	public enum EWeaponState {
		IDLE,
		FIRING,
		AIMING,
		AIMEDFIRING
	}

	public EMoveState MoveState;
	public EWeaponState WeaponState;


	private InputController m_InputController;
	public InputController InputController {
		get {
			if (m_InputController == null)
				m_InputController = GameManager.Instance.InputController;
			return m_InputController;
		}
	}


	void Update (){
		SetMoveState ();
		SetWeaponState ();

	}

	void SetMoveState() {
		MoveState = EMoveState.RUNNING;

		if (InputController.IsSprinting)
			MoveState = EMoveState.SPRINTING;

		if (InputController.IsWalking)
			MoveState = EMoveState.WALKING;

		if (InputController.IsCrouched)
			MoveState = EMoveState.CROUCHING;
	}

	void SetWeaponState(){
		WeaponState = EWeaponState.IDLE;

		if (InputController.Fire1)
			WeaponState = EWeaponState.FIRING;

		if (InputController.IsAiming)
			WeaponState = EWeaponState.AIMING;

		if (InputController.IsAiming && InputController.Fire1)
			WeaponState = EWeaponState.AIMEDFIRING;
	}

}
