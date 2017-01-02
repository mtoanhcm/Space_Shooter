using UnityEngine;
using System.Collections;

public class PartnerShip : MonoBehaviour {

	[SerializeField] Transform target;
	[SerializeField] GameObject myBullet;
	float speed, fireRate;

	void Awake(){
		speed = Random.Range (2.0f, 6.0f);
		fireRate = 0.5f;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (Move ());
		StartCoroutine (Shoot ());
	}

	IEnumerator Move(){
		if (target != null) {
			transform.position = Vector3.MoveTowards (transform.position, target.position, Time.deltaTime * speed);

			yield return new WaitForSeconds (0.01f);
			StartCoroutine (Move ());
		} else {
			Destroy (gameObject);
		}
	}

	IEnumerator Shoot(){
		Instantiate (myBullet, transform.position, Quaternion.identity);

		yield return new WaitForSeconds (fireRate);
		StartCoroutine (Shoot ());
	}

	public void SetTarget(Transform target){
		this.target = target;
	}

}
