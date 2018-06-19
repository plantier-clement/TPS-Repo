using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GmKillList : Gamemode {


	[SerializeField] Destructible[] targets;

	int targetsDestroyedCounter;






	protected override void Start () {
		base.Start ();

		for (int i = 0; i < targets.Length; i++) {
			targets [i].OnDeath += Gameplay_OnDeath;

		}

	}
	

	private void Gameplay_OnDeath (){
		targetsDestroyedCounter++;
		if (targetsDestroyedCounter == targets.Length) {
			isVictory = true;
			TriggerEndGame (isVictory);
		
		}
	}
}
