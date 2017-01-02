using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour {

	Button myButton;

	// Use this for initialization
	void Awake () {
		myButton = GetComponent<Button> ();
	}

	void Start(){
		SkillChooseController.instance.AddButtonToList (myButton);
		myButton.onClick.AddListener (() => ButtonClick ());
	}

	void ButtonClick(){
		SkillChooseController.instance.CheckButtonState (myButton);
		SkillChooseController.instance.skillDescript (myButton.name);
	}
}
