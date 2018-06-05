using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
public class EnemyPatrol : MonoBehaviour {


	[SerializeField] WaypointController waypointController;
	[SerializeField] float waitTimeMin;
	[SerializeField] float waitTimeMax;

	Pathfinder pathfinder;
	EnemyPlayer m_EnemyPlayer;
	EnemyPlayer EnemyPlayer {
		get {
			if (m_EnemyPlayer == null)
				m_EnemyPlayer = GetComponent <EnemyPlayer> ();
			return m_EnemyPlayer;
		}
	}


	void Awake(){
		pathfinder = GetComponent <Pathfinder> ();

		EnemyPlayer.EnemyHealth.OnDeath += EnemyHealth_OnDeath;
		EnemyPlayer.OnTargetSelected += EnemyPlayer_OnTargetSelected;

	}


	void Start () {
		waypointController.SetNextWaypoint ();

	}


	void EnemyPlayer_OnTargetSelected (Player target){
		pathfinder.Agent.isStopped = true;
	}


	void EnemyHealth_OnDeath ()	{
		pathfinder.Agent.isStopped = true;
	}


	void WaypointController_OnWaypointChanged (Waypoint waypoint) {
		pathfinder.SetTarget (waypoint.transform.position);
	}


	void Pathfinder_OnDestinationReached ()	{
		// assume is patrolling
		GameManager.Instance.Timer.Add (waypointController.SetNextWaypoint, Random.Range (waitTimeMin, waitTimeMax));

	}


	void OnEnable(){
		pathfinder.OnDestinationReached += Pathfinder_OnDestinationReached;
		waypointController.OnWaypointChanged += WaypointController_OnWaypointChanged;
	}


	void OnDisable(){
		pathfinder.OnDestinationReached -= Pathfinder_OnDestinationReached;
		waypointController.OnWaypointChanged -= WaypointController_OnWaypointChanged;
	}
}
