using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinder))]
[RequireComponent(typeof(EnemyPlayer))]
public class EnemyAnimation : MonoBehaviour {


	[SerializeField] Animator animator;

	Vector3 lastPosition;
	Pathfinder pathfinder;
	EnemyPlayer enemyPlayer;


	void Awake () {
		pathfinder = GetComponent <Pathfinder> ();
		enemyPlayer = GetComponent <EnemyPlayer> ();

	}


	void Update () {
		float velocity = ((transform.position - lastPosition).magnitude) / Time.deltaTime;
		lastPosition = transform.position;

		animator.SetFloat ("Vertical", velocity / pathfinder.Agent.speed);
		animator.SetBool ("IsWalking", enemyPlayer.EnemyState.CurrentMode == EnemyState.EMode.AWARE);
	}

}
