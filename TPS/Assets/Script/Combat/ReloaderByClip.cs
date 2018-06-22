using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloaderByClip : WeaponReloader {


	public override void StartReload ()	{
		base.StartReload ();

		GameManager.Instance.Timer.Add (() => {
			ExecuteReload(inventory.TakeFromContainer (containerItemId, clipSize - RoundsRemainingInClip));
		}, reloadTimeDefault);
	}


	public override void ExecuteReload (int amount)
	{
		shotsFiredInClip -= amount;
		isReloading = false;

		HandleOnAmmoChanged ();
		HandleOnReloadEnd ();
	}


}
