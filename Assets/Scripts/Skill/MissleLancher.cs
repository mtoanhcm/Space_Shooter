using UnityEngine;
using System.Collections;

public class MissleLancher : MonoBehaviour {

	[SerializeField] GameObject myMissle;
	[SerializeField] Transform myTarget;
	float delay, misslesNum;

	void Awake(){
		misslesNum = 4f;
		delay = 2f;
	}

	// Use this for initialization
	void Start () {
		SetMissleTarget ();
		StartCoroutine (FireMissles ());
	}

	IEnumerator FireMissles(){
		for (int i = 0; i < misslesNum; i++) {
			float missleBeginRotate = 0f;
			if (i % 2 == 0) {
				missleBeginRotate = Random.Range (-140f, -45f);
			} else {
				missleBeginRotate = Random.Range (45f, 140f);
			}

			GameObject missleGO = Instantiate (myMissle, transform.position, Quaternion.Euler (0, 0, missleBeginRotate)) as GameObject;
			Missle missle = missleGO.GetComponent<Missle> ();
			missle.SetTarget (myTarget);
		}

		yield return new WaitForSeconds (delay);
		StartCoroutine (FireMissles ());
	}

	void SetMissleTarget(){
		Transform target = transform.GetChild (0);
		target.parent = null;
		target.position = new Vector3 (0, 20, 0);
	}
}
