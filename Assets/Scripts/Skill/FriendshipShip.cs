using UnityEngine;
using System.Collections;

public class FriendshipShip : MonoBehaviour, IHealth {

	[SerializeField] Transform target;
	[SerializeField] GameObject myBullet;
	[SerializeField] float speed, health, shootRate;
	Rigidbody2D myBody;

	void Awake(){
		health = 3f;
		shootRate = 0.4f;
		speed = Random.Range(1f,4f);
		myBody = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		transform.parent = null;
		StartCoroutine (MoveToPoint ());
		StartCoroutine (Shoot ());
	}

	IEnumerator MoveToPoint(){
		Vector3 myPos = transform.position;
		Vector3 targetPos = target.position;

		transform.position = Vector3.MoveTowards (myPos, targetPos, speed * Time.deltaTime);

		yield return new WaitForSeconds (0.01f);
		if (myPos == targetPos) {
			StartCoroutine (StandBy ());
			yield break;
		} else {
			StartCoroutine (MoveToPoint ());	
		}
	}

	IEnumerator StandBy(){
		float speedX = Random.Range (-0.2f, 0.2f);
		float speedY = Random.Range (-0.2f, 0.2f);

		myBody.velocity = new Vector2 (speedX, speedY);

		yield return new WaitForSeconds (0.5f);
		StartCoroutine (StandBy ());
	}

	IEnumerator Shoot(){
		Instantiate (myBullet, transform.position, Quaternion.identity);

		yield return new WaitForSeconds (shootRate);
		StartCoroutine (Shoot ());
	}

	public void SetTarget(Transform target){
		this.target = target;
	}

	public void TakeDamage(float damage, Transform myTrans){
		health -= damage;
		if (health <= 0) {
			//Die
		}
	}
}
