using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Destructible {

	[SerializeField] SpawnPoint[] spawnPoints;
	[SerializeField] float respawnTime;
	[SerializeField] Ragdoll ragdoll;

	private PlayerState m_PlayerState;
	private PlayerState PlayerState{
		get {
			if (m_PlayerState == null)
				m_PlayerState = GameManager.Instance.LocalPlayer.PlayerState;
			return m_PlayerState;
		}
	}



	void SpawnAtNewSpawnpoint() {
		int spawnIndex = Random.Range (0, spawnPoints.Length);
		transform.position = spawnPoints [spawnIndex].transform.position;
		transform.rotation = spawnPoints [spawnIndex].transform.rotation;
	}

	public override void Die ()	{
		base.Die ();


		ragdoll.EnableRagdoll (true);
		GameManager.Instance.Timer.Add (SpawnAtNewSpawnpoint, respawnTime);

		GameManager.Instance.EventBus.RaiseEvent ("OnPlayerDeath");
	}

	[ContextMenu("Test Die!")]
	void TestDie(){
		Die ();
	}
}
