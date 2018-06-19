using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

public class SpawnPoint : MonoBehaviour {

	public string CustomName;

	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireCube (Vector3.zero + Vector3.up * 1, Vector3.one + Vector3.up * 1);
		Gizmos.color = Color.red;
		Gizmos.DrawLine (Vector3.zero + Vector3.up * 1, Vector3.zero + new Vector3 (0,1,1) * 1);

		#if (UNITY_EDITOR)	
		HandleNames ();
		#endif
	}


	#if (UNITY_EDITOR)	

	void HandleNames() {

		if (CustomName == string.Empty) {
			Handles.Label (transform.position + Vector3.up * 0.5f, name);
		} else {
			Handles.Label (transform.position + Vector3.up * 0.5f, CustomName);
		}
	}
	#endif
}
