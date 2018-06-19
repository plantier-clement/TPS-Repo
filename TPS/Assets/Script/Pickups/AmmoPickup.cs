using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : PickupItem {

	[SerializeField] EWeaponType weaponType;
	[SerializeField] float respawnTime;
	[SerializeField] int amount;

	public override void OnPickup (Transform playerTransform)
	{
		var playerInventory = playerTransform.GetComponentInChildren <Container>();
		GameManager.Instance.Respawner.Despawn (gameObject, respawnTime);
		playerInventory.Put (weaponType.ToString (), amount);

		playerTransform.GetComponent <Player>().PlayerShoot.ActiveWeapon.Reloader.HandleOnAmmoChanged ();
	}




}
