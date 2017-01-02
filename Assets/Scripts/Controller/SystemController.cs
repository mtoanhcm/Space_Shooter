using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemController : MonoBehaviour {

	public static SystemController instance;

	public List<string> skillName;

	void MakeSingleton(){
		if (instance != null) {
			Destroy (gameObject);
		} else if (instance == null) {
			instance = this;
			DontDestroyOnLoad (instance);
		}
	}

	void Awake(){
		MakeSingleton ();
		skillName = new List<string> ();
	}

	void Update(){
		CheckKey ();
	}

	void CheckKey(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	public void GetSkillName(string mySkillName){
		skillName.Add (mySkillName);
	}

	public void ClearListForReChoice(){
		skillName.Clear ();
	}
}
