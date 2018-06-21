using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour {

	[SerializeField] int maxAmmo;
	[SerializeField] float reloadTimeDefault;
	[SerializeField] float fastReloadTime;
	[SerializeField] int clipSize;
	[SerializeField] Container inventory;
	[SerializeField] EWeaponType weaponType;


	[HideInInspector] public int shotsFiredInClip;

	bool isReloading;
	System.Guid containerItemId;


	public event System.Action OnReloadStart;
	public event System.Action OnReloadEnd;
	public event System.Action OnAmmoChanged;


	void Awake(){
		inventory.OnContainerReady += () => {  
			containerItemId = inventory.Add (weaponType.ToString (), maxAmmo);
		};
	}
		

	public void Reload(){
		if (isReloading)
			return;

		isReloading = true;
		HandleOnReloadStart ();

		GameManager.Instance.Timer.Add (() => {
			ExecuteReload(inventory.TakeFromContainer (containerItemId, clipSize - RoundsRemainingInClip));
		}, reloadTimeDefault);
	}


	private void ExecuteReload(int amount){
		shotsFiredInClip -= amount;
		isReloading = false;

		HandleOnAmmoChanged ();
		HandleOnReloadEnd ();
	}


	public void TakeFromClip(int amount) {
		shotsFiredInClip += amount;

		HandleOnAmmoChanged ();
	}


	public void HandleOnAmmoChanged (){
		if (OnAmmoChanged != null)
			OnAmmoChanged ();
	}


	void HandleOnReloadStart (){
		if (OnReloadStart != null) {
			OnReloadStart ();
		}
	}


	void HandleOnReloadEnd (){
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
