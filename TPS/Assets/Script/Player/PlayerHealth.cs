using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Destructible {

	[SerializeField] SpawnPoint[] spawnPoints;
	[SerializeField] float respawnTime;
	[SerializeField] Ragdoll ragdoll;







	public override void Die ()	{
		base.Die ();


		ragdoll.EnableRagdoll (true);

		GameManager.Instance.EventBus.RaiseEvent ("OnPlayerDeath");
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.MENU);


		// if no spawn
		if (spawnPoints.Length != 0) {
			GameManager.Instance.Timer.Add (PlayerRespawn, respawnTime);
			return;
		}

		GameManager.Instance.Timer.Add (DisplayDeathScreen, respawnTime);


	}


	void PlayerRespawn(){
		ResetHP ();
		SpawnAtNewSpawnpoint ();
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.CHARACTER);

	}


	void SpawnAtNewSpawnpoint() {
		int spawnIndex = Random.Range (0, spawnPoints.Length);
		transform.position = spawnPoints [spawnIndex].transform.position;
		transform.rotation = spawnPoints [spawnIndex].transform.rotation;
	}


	void DisplayDeathScreen(){


	}


	[ContextMenu("Test Die!")]
	void TestDie(){
		Die ();
	}


}
