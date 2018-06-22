using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloaderByCartridge : WeaponReloader {


	int routineCount = 0;

	Coroutine reloadCoroutine;

	public bool ReloadCoroutineRunning;

	public override void StartReload ()	{
		base.StartReload ();
		player.PlayerAnimation.SetTriggerStartReloadCartridge ();
		routineCount = 0;
		reloadCoroutine = StartCoroutine (ReloadCoroutine (reloadTimeDefault));
		ReloadCoroutineRunning = true;

	}


	public IEnumerator ReloadCoroutine (float period){
		while (RoundsRemainingInClip < clipSize) {
			print ("reload coroutine: " + routineCount);
			ExecuteReload (inventory.TakeFromContainer (containerItemId, 1));
			routineCount++;
			yield return new WaitForSeconds (period);

		}
		EndReload ();
		yield break;
	}


	public override void ExecuteReload (int amount)	{
		shotsFiredInClip -= amount;
		HandleOnAmmoChanged ();
	}


	public void EndReload(){
		if (ReloadCoroutineRunning) {
			print ("coroutine was running");
			StopCoroutine (reloadCoroutine);
		}
		print ("killing coroutine");
		ReloadCoroutineRunning = false;

		player.PlayerAnimation.SetTriggerStopReloadCartridge ();
		isReloading = false;
		HandleOnReloadEnd ();
	}



}
