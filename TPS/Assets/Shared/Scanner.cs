using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour {

	public event System.Action OnScanReady; 

	[SerializeField] float scanSpeed;
	[SerializeField] [Range(0, 360)] float fieldOfView;
	[SerializeField] LayerMask mask;

	SphereCollider rangeTrigger;
	public float ScanRange {
		get {
			if (rangeTrigger == null)
				rangeTrigger = GetComponent <SphereCollider> ();
			return rangeTrigger.radius;
		}
	}


	void PrepareScan(){

		GameManager.Instance.Timer.Add (() => {
			if(OnScanReady != null)
				OnScanReady();
		}, scanSpeed);

	}


	public List<T> ScanForTargets<T>() {

		List<T> targets = new List<T> ();

		Collider[] results = Physics.OverlapSphere (transform.position, ScanRange);

		for (int i = 0; i < results.Length; i++) {
			var player = results [i].transform.GetComponent <T> ();

			if (player == null)
				continue;

			if (!IsInLineOfSight (Vector3.up, results[i].transform.position))
				continue;

			targets.Add (player);
		}
			
		PrepareScan ();
		return targets;
	}


	bool IsInLineOfSight (Vector3 eyeHeight, Vector3 targetPosition) {
		Vector3 direction = targetPosition - transform.position;

		if (Vector3.Angle (transform.forward, direction.normalized) < (fieldOfView / 2)) {
			float distanceToTarget = Vector3.Distance (transform.position, targetPosition);

			//something blocked the view
			RaycastHit hit;
			if (Physics.Raycast (transform.position + eyeHeight + transform.forward * 0.3f, direction.normalized, out hit, distanceToTarget, mask)) {
				// print ("raycast hit: " + hit);
				Debug.DrawLine (transform.position + eyeHeight, direction.normalized + transform.forward * distanceToTarget);
				return false;
			}
			return true;
		}
		return false;
	}



	// Viewport stuff

	void OnDrawGizmos (){
		Gizmos.color = Color.green;
		Gizmos.DrawLine (transform.position, transform.position + GetViewAngle (fieldOfView / 2) * GetComponent <SphereCollider>().radius);
		Gizmos.DrawLine (transform.position, transform.position + GetViewAngle (- fieldOfView / 2) * GetComponent <SphereCollider>().radius);

	}


	Vector3 GetViewAngle (float angle){
		float radian = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
		return new Vector3 (Mathf.Sin (radian), 0, Mathf.Cos (radian));
	}


}
