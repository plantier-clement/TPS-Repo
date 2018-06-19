using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour {


	void OnTriggerEnter (Collider collider) {
		if (collider.tag != "Player")
			return;

		PickUp (collider.transform);
	}


	void PickUp(Transform playerTransform) {
		OnPickup (playerTransform);
	}


	public virtual void OnPickup (Transform playerTransform) {

	}
}
