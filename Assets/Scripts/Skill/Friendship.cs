using UnityEngine;
using System.Collections;

public class Friendship : MonoBehaviour {

	Component[] myChild;
	[SerializeField] GameObject friendShip;

	void Awake(){
		GetALLChilds ();
	}

	// Use this for initialization
	void Start () {
		CallFriendsShip ();
		transform.parent = null;
	}

	void GetALLChilds(){
		myChild = transform.GetComponentsInChildren<Transform> ();
	}

	void CallFriendsShip(){
		Debug.Log (myChild [5].transform.position);
		for (int i = 1; i <= 4; i++) {
			GameObject theShip = Instantiate (friendShip, myChild[5].transform.position, Quaternion.identity) as GameObject;
			theShip.GetComponent<FriendshipShip> ().SetTarget (myChild [i].transform);
		}
	}
}
