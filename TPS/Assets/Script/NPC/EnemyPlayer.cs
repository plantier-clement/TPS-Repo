using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyState))]
public class EnemyPlayer : MonoBehaviour {

	[SerializeField] SwatSoldier enemySettings;
	[SerializeField] Scanner playerScanner;

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
				OnTargetSelected (priorityTarget);
			}
		}
	}



	void SelectClosestTarget(){
		float closestTarget = playerScanner.ScanRange;

		foreach (var possibleTarget in myTargets) {
			if (Vector3.Distance (transform.position, possibleTarget.transform.position) < closestTarget)
				priorityTarget = possibleTarget;
		}
	}

}
