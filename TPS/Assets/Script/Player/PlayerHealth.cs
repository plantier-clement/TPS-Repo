using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Destructible {

	[SerializeField] SpawnPoint[] spawnPoints;
	[SerializeField] float respawnTime;
	[SerializeField] Ragdoll ragdoll;


	void SpawnAtNewSpawnpoint() {
		int spawnIndex = Random.Range (0, spawnPoints.Length);
		transform.position = spawnPoints [spawnIndex].transform.position;
		transform.rotation = spawnPoints [spawnIndex].transform.rotation;
	}

	public override void Die ()	{
		base.Die ();
		ragdoll.EnableRagdoll (true);
		GameManager.Instance.Timer.Add (SpawnAtNewSpawnpoint, respawnTime);

	}

	[ContextMenu("Test Die!")]
	void TestDie(){
		Die ();
	}
}
