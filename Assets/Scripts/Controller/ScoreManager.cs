using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance;

	[SerializeField]
	public Text scoreText;

	public int score = 0;

	void Awake(){
		MakeInstance ();
	}

	public void setScore(){
		score++;
		scoreText.text = "" + score;
	}

	public int getScore(){
		//this nghia la tro ve class gan nhat
		return this.score;
	}

	public void MakeInstance(){
		if (instance == null) {
			instance = this;
		}
	}
}
