using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerState))]
public class Player : MonoBehaviour {

	[SerializeField] SwatSoldier playerSettings;
	[SerializeField] MouseInput MouseControl;
	[SerializeField] AudioController footsteps;
	[SerializeField] float minMoveThreshold;

	float test;

	[System.Serializable]
	public class MouseInput {
		public Vector2 Damping;
		public Vector2 Sensitivity;
		public bool LockMouse;
	}


	InputController playerInput;
	Vector2 mouseInput;
	float moveSpeed;
	Vector3 previousPostion;

	public PlayerAim PlayerAim;


	private CharacterController m_MoveController;
	public CharacterController MoveController {
		get {
			if (m_MoveController == null)
				m_MoveController = GetComponent <CharacterController> ();
			return m_MoveController;
		}
	}

	private PlayerShoot m_PlayerShoot;
	public PlayerShoot PlayerShoot {
		get {
			if (m_PlayerShoot == null)
				m_PlayerShoot = GetComponent <PlayerShoot> ();
			return m_PlayerShoot;
		}
	}

	private PlayerState m_PlayerState;
	public PlayerState PlayerState {
		get {
			if (m_PlayerState == null)
				m_PlayerState = GetComponent <PlayerState> ();
			return m_PlayerState;
		}
	}
		
	private PlayerHealth m_PlayerHealth;
	public PlayerHealth PlayerHealth {
		get {
			if (m_PlayerHealth== null)
				m_PlayerHealth= GetComponent <PlayerHealth> ();
			return m_PlayerHealth;
		}
	}

	void Awake () {
		playerInput = GameManager.Instance.InputController;
		GameManager.Instance.LocalPlayer = this;

		LockMouse ();

	}


	void Update () {
		if (!PlayerHealth.IsAlive)
			return;
		
		Move ();
		LookAround ();

	}


	void Move(){

		HandleMoveSpeed ();

		Vector2 direction = new Vector2 (playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);
		MoveController.SimpleMove (transform.forward * direction.x + transform.right * direction.y);

		// footsteps
		if (Vector3.Distance (transform.position, previousPostion) > minMoveThreshold)
			footsteps.Play ();
		previousPostion = transform.position;

	}


	void LookAround(){
		mouseInput.x = Mathf.Lerp (mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
		mouseInput.y = Mathf.Lerp (mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);

		transform.Rotate (Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);
		PlayerAim.SetRotation (mouseInput.y * MouseControl.Sensitivity.y);
	}
		

	void HandleMoveSpeed(){

		moveSpeed = playerSettings.RunSpeed;

		if (playerInput.IsSprinting)
			moveSpeed = playerSettings.SprintSpeed;
		
		if (playerInput.IsWalking)
			moveSpeed = playerSettings.WalkSpeed;
		
		if (playerInput.IsCrouched)
			moveSpeed = playerSettings.CrouchSpeed;

		// find a way to handle multiple inputs?
	
	}


	void LockMouse(){
		if (MouseControl.LockMouse) {
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}


}
