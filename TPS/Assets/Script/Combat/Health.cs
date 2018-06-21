using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : Destructible {


	[SerializeField] float respawnTime;

	public override void Die () {
		base.Die ();

		GameManager.Instance.Respawner.Despawn (gameObject, respawnTime);
	}

	void OnEnable(){
		ResetHP ();
	}


	public override void TakeDamage (float amount) {
		base.TakeDamage (amount);
	}
}
