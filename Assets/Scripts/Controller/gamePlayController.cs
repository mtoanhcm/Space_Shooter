using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class gamePlayController : MonoBehaviour {

	public static gamePlayController instance;

	[SerializeField] GameObject gameOverPanel,mySkill;
	[SerializeField] Transform playerTrans;
	[SerializeField] Animator gameOverAnim;
	[SerializeField] Text finalScore;

	List<GameObject> skillGO;
	string bulletSkill,gunSkill,supportSkill, typeOfSkill;
	Vector3 skillTrans;
	float timeDelaySkillSupportAttack;

	// Use this for initialization
	void Awake () {
		MakeInstance ();

		timeDelaySkillSupportAttack = 4f;

		LoadAllPrefabsToList ();

		GetSkillNameFromSystemController ();
		CheckSupportSKillType (supportSkill);
	}

	void Start(){
		CreateSkillFromSkillList ();
	}

	// Update is called once per frame
	void MakeInstance () {
		if (instance == null) {
			instance = this;
		}
	}

	public void PlayerDiedShowPanel(){
		gameOverPanel.SetActive (true);
		gameOverAnim.Play ("Fadein");

		finalScore.text = "" + ScoreManager.instance.getScore ();

	}

	public void restartButton(){
		Application.LoadLevel (1);
	}

	void LoadAllPrefabsToList(){
		var tempList = Resources.LoadAll ("Skill");
		skillGO = new List<GameObject> ();
		foreach (var temp in tempList) {
			skillGO.Add (temp as GameObject);
		}
	}

	void GetSkillNameFromSystemController(){
		foreach (string name in SystemController.instance.skillName) {

			if (name.EndsWith ("Bullet")) {
				bulletSkill = name;
			} else if (name.EndsWith ("Gun")) {
				gunSkill = name;
			} else {
				supportSkill = name;
			}
		}
	}

	void CreateSkillFromSkillList(){
		Vector3 playerPos = playerTrans.position;

		//GunCreate
		CreateGun(playerPos);
		CreateSupportSkill (playerPos);
	}

	void CreateGun(Vector3 playerPos){
		foreach (GameObject skill in skillGO) {
			if (skill.name == gunSkill) {
				GameObject myChild = Instantiate (skill, playerPos, Quaternion.identity) as GameObject;
				myChild.transform.parent = playerTrans;
			}
		}
	}

	//Bullet duoc quy dinh o Gameobject SingleGun
	public string GetNameOfBullet(){
		return this.bulletSkill;
	}

	void CreateSupportSkill(Vector3 playerPos){
		foreach (GameObject skill in skillGO) {
			if (skill.name == supportSkill) {
				if (typeOfSkill == "Attack") {
					StartCoroutine (UseAttackSuppotSkill (skill, timeDelaySkillSupportAttack));
				} else if (typeOfSkill == "Passive") {
					UsePassiveSupportSkill (skill);
				}
			}
		}
	}

	IEnumerator UseAttackSuppotSkill(GameObject skill, float delay){
		Vector3 playerPos = playerTrans.position;

		Instantiate (skill, playerPos, Quaternion.identity);

		yield return new WaitForSeconds (delay);
		StartCoroutine (UseAttackSuppotSkill (skill, delay));
	}

	void UsePassiveSupportSkill(GameObject skill){
		Vector3 playerPos = playerTrans.position;

		GameObject child = Instantiate (skill, playerPos, Quaternion.identity) as GameObject;
		child.transform.parent = playerTrans;
	} 

	void CheckSupportSKillType(string skill){
		if (skill == "BulletStorm" || skill == "Bomb") {
			typeOfSkill = "Attack";
		}

		if (skill == "Protector" || skill == "Shield"  || skill == "Missile" || skill == "Partner" || skill == "Friendship") {
			typeOfSkill = "Passive";
		}
	}
}