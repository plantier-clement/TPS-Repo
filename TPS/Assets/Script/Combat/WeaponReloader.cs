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

	 public int shotsFiredInClip;

	bool isReloading;
	System.Guid containerItemId;


	public event System.Action<Shooter> OnReloadStart;
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
		GameManager.Instance.Timer.Add (() => {
			ExecuteReload(inventory.TakeFromContainer (containerItemId, clipSize - RoundsRemainingInClip));
		}, reloadTimeDefault);
	}


	private void ExecuteReload(int amount){
		shotsFiredInClip -= amount;
		isReloading = false;

		HandleOnAmmoChanged ();
	}


	public void TakeFromClip(int amount) {
		shotsFiredInClip += amount;

		HandleOnAmmoChanged ();
	}


	public void HandleOnAmmoChanged (){
		if (OnAmmoChanged != null)
			OnAmmoChanged ();
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
