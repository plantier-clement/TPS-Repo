using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gamemode : MonoBehaviour {



	protected bool isVictory;
	[SerializeField] GameObject VictoryScreen;
	[SerializeField] GameObject DefeatScreen;

	[SerializeField] float timer;


	protected virtual void Start () {
		VictoryScreen.SetActive (false);
		DefeatScreen.SetActive (false);
		GameManager.Instance.EventBus.AddListener ("OnPlayerDeath", () => 
			{
				isVictory = false;
				TriggerEndGame(isVictory);
			});

	}


	protected virtual void Update(){
	
	}


	protected virtual void TriggerEndGame(bool value){

		TriggerVictoryDefeat (value);
	}


	protected virtual void TriggerVictoryDefeat(bool value){

		Time.timeScale = 0;
		VictoryScreen.SetActive (value);
		DefeatScreen.SetActive (!value);


		if (value) {
			print ("victory");
			return;

		}
		if (!value)
			print ("defeat");
	}

}
