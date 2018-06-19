using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] float timeToLive;
	[SerializeField] float damage;
	[SerializeField] Transform bulletHole;

	Vector3 destination;


	void Start () {
		Destroy (gameObject, timeToLive);
	}


	void Update () {

		if (isDestinationReached ()) {
			Destroy (gameObject);
			return;
		}
			

		transform.Translate (Vector3.forward * speed * Time.deltaTime);

		if (destination != Vector3.zero) // after moving if destination reached, no need to calcultate raycast
			return;

		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, 5f)) {
			CheckDestructible (hit);
		}
	}


	void CheckDestructible (RaycastHit hitInfo){
		var destructible = hitInfo.transform.GetComponent <Destructible> ();

		destination = hitInfo.point + hitInfo.normal * 0.0015f;

		Transform hole = Instantiate (bulletHole, destination, Quaternion.LookRotation (hitInfo.normal) * Quaternion.Euler (0, 180f, 0));
		hole.SetParent (hitInfo.transform);

		if (destructible == null)
			return;
		
		destructible.TakeDamage (damage);
	}


	bool isDestinationReached(){

		if (destination == Vector3.zero)
			return false;

		Vector3 directionToDestination = destination - transform.position;
		float dot = Vector3.Dot (directionToDestination, transform.forward); //dot product: dot < 0, we have past the destination

		if (dot < 0)
			return true;

		return false;
	}

}
