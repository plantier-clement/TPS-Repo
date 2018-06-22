using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour {

	[SerializeField] int maxAmmo;
	[SerializeField] public float reloadTimeDefault;
	[SerializeField] public float fastReloadTime;
	[SerializeField] public int clipSize;
	[SerializeField] public Container inventory;
	[SerializeField] EWeaponType weaponType;


	[HideInInspector] public int shotsFiredInClip;
	[HideInInspector] public bool isReloading;
	public System.Guid containerItemId;

	public event System.Action OnReloadStart;
	public event System.Action OnReloadEnd;
	public event System.Action OnAmmoChanged;


	protected Player player;

	void Awake(){
		inventory.OnContainerReady += () => {  
			containerItemId = inventory.Add (weaponType.ToString (), maxAmmo);
		};

		player = GetComponentInParent <Player> ();
	}
		

	public void Reload(){
		if (isReloading)
			return;

		HandleOnReloadStart ();

		StartReload ();

	}


	public virtual void StartReload (){
		isReloading = true;

	}


	public virtual void ExecuteReload(int amount){

	}


	public void TakeFromClip(int amount) {
		shotsFiredInClip += amount;

		HandleOnAmmoChanged ();
	}


	public void HandleOnAmmoChanged (){
		if (OnAmmoChanged != null) {
			OnAmmoChanged ();

		}
	}


	public void HandleOnReloadStart (){
		if (OnReloadStart != null) {
			OnReloadStart ();
		}
	}


	public void HandleOnReloadEnd (){
		if (OnReloadEnd != null) {
			OnReloadEnd ();
		}
	}


	public bool IsReloading {
		get {
			return isReloading;
		}
	}


	public int RoundsRemainingInClip {
		get {
			return clipSize - shotsFiredInClip;
		}
	}

	public int RoundsRemainingInInventory {
		get {
			return inventory.GetAmountRemaining (containerItemId);
		}
	}
}
