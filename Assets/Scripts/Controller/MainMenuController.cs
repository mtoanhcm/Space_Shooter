using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void playButton(){
		SceneManager.LoadScene (1);
	}

	public void quitButton(){
		Application.Quit ();
	}
}
