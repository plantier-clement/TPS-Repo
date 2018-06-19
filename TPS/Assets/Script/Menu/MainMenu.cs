using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {



	[SerializeField] Button StartGameButton;
	[SerializeField] Button QuitGameButton;
	public string LevelName;




	void Start () {
		StartGameButton.onClick.AddListener (() => {
			StartGame (LevelName);
		});


		QuitGameButton.onClick.AddListener (() => {
			EndGame ();
		});
	}




	public void StartGame(string name){
	
		SceneManager.LoadScene (name);
	}


	public void EndGame(){
	
		Application.Quit ();
	}

}
