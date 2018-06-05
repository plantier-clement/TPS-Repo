using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Pathfinder : MonoBehaviour {

	[SerializeField] float distanceRemainingThreshold;

	[HideInInspector] public NavMeshAgent Agent;
	public event System.Action OnDestinationReached;

	bool m_DestinationReached;
	bool destinationReached {
		get { return m_DestinationReached; }
		set {
			m_DestinationReached = value;
			if(m_DestinationReached) {
				if(OnDestinationReached != null)
					OnDestinationReached ();
			}
		}
	}



	void Awake () {
		Agent = GetComponent <NavMeshAgent> ();

	}


	void Update () {
		if (destinationReached || !Agent.hasPath)
			return;

		if (Agent.remainingDistance < distanceRemainingThreshold)
			destinationReached = true;
	}


	public void SetTarget(Vector3 targetPosition) {
		Agent.SetDestination (targetPosition);
		destinationReached = false;

	}
}
