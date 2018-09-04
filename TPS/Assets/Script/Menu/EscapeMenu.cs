using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscapeMenu : MonoBehaviour {


	[SerializeField] GameObject EscapeMenuPanel;
	[SerializeField] Button YesButton;
	[SerializeField] Button NoButton;


	void Awake(){
		EscapeMenuPanel.SetActive (false);
		YesButton.onClick.AddListener (OnYesClicked);
		NoButton.onClick.AddListener (OnNoClicked);
	}


	void OnYesClicked(){
	
		SceneManager.LoadScene ("MainMenu");

	}


	void OnNoClicked(){
		ResumeGame ();
	}


	void Update(){

		if (GameManager.Instance.InputController.Escape) {

			if (GameManager.Instance.IsGamePaused) {
				ResumeGame ();
				return;
			}
			if (!GameManager.Instance.IsGamePaused) {
				PauseGame ();
			} 
		}
	}


	void PauseGame(){
		Time.timeScale = 0;
		EscapeMenuPanel.SetActive (true);
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.MENU);
		Cursor.visible = true;
		GameManager.Instance.IsGamePaused = true;

	}


	void ResumeGame(){
		Time.timeScale = 1;
		EscapeMenuPanel.SetActive (false);
		GameManager.Instance.InputController.SetInputMode (InputController.EInputMode.CHARACTER);
		Cursor.visible = false;
		GameManager.Instance.IsGamePaused = false;

	}

}
