using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyState))]
public class EnemyPlayer : MonoBehaviour {

	[SerializeField] SwatSoldier enemySettings;
	[SerializeField] public Scanner playerScanner;

	Pathfinder pathfinder;

	Player priorityTarget;
	List<Player> myTargets;


	EnemyState m_EnemyState;
	public EnemyState EnemyState{
		get {
			if (m_EnemyState == null)
				m_EnemyState = GetComponent <EnemyState> ();
			return m_EnemyState;
		}
	}
		
	EnemyHealth m_EnemyHealth;
	public EnemyHealth EnemyHealth {
		get {
			if (m_EnemyHealth == null)
				m_EnemyHealth = GetComponent <EnemyHealth> ();
			return m_EnemyHealth;
		}
	}

	EnemyAnimation m_EnemyAnimation;
	public EnemyAnimation EnemyAnimation {
		get {
			if (m_EnemyAnimation == null)
				m_EnemyAnimation = GetComponent <EnemyAnimation> ();
			return m_EnemyAnimation;
		}
	}

	EnemyShoot m_EnemyShoot;
	public EnemyShoot EnemyShoot {
		get {
			if (m_EnemyShoot == null)
				m_EnemyShoot = GetComponent <EnemyShoot> ();
			return m_EnemyShoot;
		}
	}


	public event System.Action <Player> OnTargetSelected;


	void Awake (){
		pathfinder = GetComponent <Pathfinder> ();
		EnemyState.OnModeChanged += EnemyState_OnModeChanged;

	}

	void Start () {
		pathfinder.Agent.speed = enemySettings.WalkSpeed;

		playerScanner.OnScanReady += Scanner_OnScanReady;
		Scanner_OnScanReady ();

		EnemyHealth.OnDeath += EnemyHealth_OnDeath;
	}


	void Update(){
		
		if (EnemyHealth.IsAlive)
			return;
		
		if (priorityTarget == null)
			return;

		transform.LookAt (priorityTarget.transform.position);
	}


	void EnemyHealth_OnDeath ()	{
		//
	}


	void EnemyState_OnModeChanged (EnemyState.EMode state)	{

		pathfinder.Agent.speed = enemySettings.WalkSpeed;

		if (state == EnemyState.EMode.AWARE)
			pathfinder.Agent.speed = enemySettings.RunSpeed;
	}


	void Scanner_OnScanReady ()	{

		if (priorityTarget != null)
			return;

		myTargets = playerScanner.ScanForTargets<Player> ();

		if (myTargets.Count == 1)
			priorityTarget = myTargets [0];
		else
			SelectClosestTarget ();

		if (priorityTarget != null) {
			if (OnTargetSelected != null) {
				this.EnemyState.CurrentMode = EnemyState.EMode.AWARE;
				OnTargetSelected (priorityTarget);
			}
		}
	}



	void SelectClosestTarget() {
		float closestTarget = playerScanner.ScanRange;

		foreach (var possibleTarget in myTargets) {
			if (Vector3.Distance (transform.position, possibleTarget.transform.position) < closestTarget)
				priorityTarget = possibleTarget;
		}
	}


	internal void ClearTargetAndScan (){
		priorityTarget = null;

		GameManager.Instance.Timer.Add (CheckCrouch, 3);
		GameManager.Instance.Timer.Add (CheckStopAiming, 5);
		GameManager.Instance.Timer.Add (CheckContinuePatrol, 7);

		Scanner_OnScanReady ();
	}


	void CheckStopAiming (){
		if(priorityTarget != null)
			return;

		this.EnemyState.CurrentMode = EnemyState.EMode.UNAWARE;
	}


	void CheckContinuePatrol (){
		if (priorityTarget != null)
			return;
		pathfinder.Agent.isStopped = false;
	}

	void CheckCrouch (){
		if(priorityTarget != null)
			return;

		this.EnemyState.CurrentMoveState = EnemyState.EMoveState.STANDING;
	}


}
