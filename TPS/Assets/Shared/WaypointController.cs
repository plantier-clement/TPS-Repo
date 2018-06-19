using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (UNITY_EDITOR)
using UnityEditor;
#endif


public class WaypointController : MonoBehaviour {

	Waypoint[] waypoints;
	int currentWaypointIndex = -1;

	public event System.Action <Waypoint> OnWaypointChanged;


	void Awake () {
		waypoints = GetComponentsInChildren <Waypoint> ();

	}
	
	void Update () {
		
	}


	public void SetNextWaypoint() {
		currentWaypointIndex++;

		// assuming the waypoint controller loops
		if (currentWaypointIndex == waypoints.Length)
			currentWaypointIndex = 0;

		if (OnWaypointChanged != null)
			OnWaypointChanged (waypoints [currentWaypointIndex]);
	
	}

	// Debug

	Waypoint[] GetWaypoints() {
		return  GetComponentsInChildren <Waypoint> ();
	}


	void OnDrawGizmos(){
	
		Gizmos.color = Color.blue;

		Vector3 previousWaypoint = Vector3.zero;

		foreach (var waypoint in GetWaypoints ()) {
			Vector3 waypointPosition = waypoint.transform.position;
			Gizmos.DrawSphere (waypointPosition, 0.2f);

			#if (UNITY_EDITOR)
			HandleNames (waypoint);
			#endif

			if (previousWaypoint != Vector3.zero) {
				Gizmos.DrawLine (previousWaypoint, waypointPosition);
			}
			previousWaypoint = waypointPosition;
		}

	}


	#if (UNITY_EDITOR)

	void HandleNames(Waypoint waypoint) {

		if (waypoint.CustomName == string.Empty) {
			Handles.Label (waypoint.transform.position + Vector3.up * 0.5f, waypoint.name);
		} else {
			Handles.Label (waypoint.transform.position + Vector3.up * 0.5f, waypoint.CustomName);
		}
	}

	#endif

}
