using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SkillChooseController : MonoBehaviour {

	public static SkillChooseController instance;

	[SerializeField] private Text descText;

	//public List<string> skillList; 
	public int skillNum;
	[SerializeField] private string skill_1, skill_2;

	List<Button> allSkill;
	List<Button> bulletSkill;
	List<Button> gunSkill;
	List<Button> supportSkill;

	//skill stat--------------------



	void MakeInstance(){
		if (instance == null)
			instance = this;
	}

	void Awake(){
		MakeInstance ();

		allSkill = new List<Button> ();
		bulletSkill = new List<Button> ();
		gunSkill = new List<Button> ();
		supportSkill = new List<Button> ();
	}


	// Add danh sach button skill theo list tuong ung
	//Can danh sach nay de rang buoc viec giua cac loai skill thi duoc chon duy nhat 1 skill
	public void AddButtonToList(Button myButton){
		if (myButton.tag == "BulletSkill") {
			bulletSkill.Add (myButton);
		} else if (myButton.tag == "GunSkill") {
			gunSkill.Add (myButton);
		} else if (myButton.tag == "SupportSkill") {
			supportSkill.Add (myButton);
		}

		allSkill.Add (myButton);
	}

	// Kiem tra trang thai click cua button
	// Voi moi loai skill thi chi duoc chon 1 skill
	// Khi nhan button chon skill nay thi cac lua chon skill cung loai khac se tra ve vi tri cu
	public void CheckButtonState(Button myButton){
		if (myButton.tag == "BulletSkill") {
			foreach (Button button in bulletSkill) {
				button.interactable = true;
			}

			myButton.interactable = false;
		}

		if (myButton.tag == "GunSkill") {
			foreach (Button button in gunSkill) {
				button.interactable = true;
			}

			myButton.interactable = false;
		}

		if (myButton.tag == "SupportSkill") {
			foreach (Button button in supportSkill) {
				button.interactable = true;
			}

			myButton.interactable = false;
		}
	}

	// Kiem tra nen skill nao duoc chon thi se dua vao danh sachs skill duoc chon o systemcontroller
	public void SetChoosenSkilltoSystemController(){
		SystemController.instance.ClearListForReChoice ();

		foreach (Button button in allSkill) {
			if (!button.interactable) {
				SystemController.instance.GetSkillName (button.name);
			}
		}

		SceneManager.LoadScene (2);
	}

	public void skillDescript(string skillName){
		if (skillName == "NormalBullet") {
			descText.text = skillName + ":\nSmall size, weak but fast" + "\nDelay: 0.4s" + "\nDamage: 1";
		}

		if (skillName == "BigBullet") {
			descText.text = skillName + ":\nDamage well, medium delay" + "\nDelay: 0.8s" + "\nDamage: 2";
		}

		if (skillName == "LaserBullet") {
			descText.text = skillName + ":\nVery powerful but slow" + "\nDelay: 1.6s" + "\nDamage: 4";
		}

		if (skillName == "DoubleGun") {
			descText.text = skillName + ":\nAdd 2 gun to your ship";
		}

		if (skillName == "TripleGun") {
			descText.text = skillName + ":\nAdd 3 gun to your ship";
		}

		if (skillName == "QuadraGun") {
			descText.text = skillName + ":\nAdd 4 gun to your ship";
		}

		if (skillName == "PentaGun") {
			descText.text = skillName + ":\nAdd 5 gun to your ship";
		}

		if (skillName == "Shield") {
			descText.text = skillName + ":\nCreate an enegry shield to protect your ship" + "\nDuration: 3";
		}

		if (skillName == "Protector") {
			descText.text = skillName + ":\nCreate two metal shield to protect your ship";
		}

		if (skillName == "Bomb") {
			descText.text = skillName + ":\nDestroy all enemy in range";
		}

		if (skillName == "Immortal") {
			descText.text = skillName + ":\nYou become immortal and destroy everything";
		}

		if (skillName == "Missile") {
			descText.text = skillName + ":\nShoot 4 missiles";
		}
			
		if (skillName == "Partner") {
			descText.text = skillName + ":\nYour partner will fight with you";
		}

		if (skillName == "BulletStorm") {
			descText.text = skillName + ":\nCall a storm of bullets" + "\nDelay: 6s";
		}

		if (skillName == "Friendship") {
			descText.text = skillName + ":\nCall friends for a short time";
		}
	}

}