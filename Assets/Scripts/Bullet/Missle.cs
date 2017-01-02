using UnityEngine;
using System.Collections;

public class Missle : MonoBehaviour {

	Transform target;
	Rigidbody2D myBody;
	[SerializeField] GameObject missileExplosion;
	[SerializeField] AudioClip clipExplosion;
	[SerializeField] AudioSource sourceExplosion;

	float speed, damage;
	float lastTurn,turn;

	void Awake(){
		myBody = GetComponent<Rigidbody2D> ();
		lastTurn = 0f;
		turn = 2.5f;
		speed = 5f;
		damage = 3;
	}

	void FixedUpdate(){
		MoveMissle ();
	}
	
	void MoveMissle(){
		//Xoay missle
		Quaternion lookToTarget = Quaternion.LookRotation (transform.position - target.position, Vector3.forward);
		lookToTarget.x = 0;
		lookToTarget.y = 0;
		transform.rotation = Quaternion.Lerp (transform.rotation, lookToTarget, Time.deltaTime * turn);
		if (turn < 40) {
			lastTurn += Time.deltaTime * Time.deltaTime * 50f;
			turn += lastTurn;
		}

		myBody.velocity = transform.up * speed;
	}

	public void SetTarget(Transform target){
		this.target = target;
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "Bounds") {
			Destroy (gameObject);
		}

		var health = target.GetComponent<IHealth> ();
		if (health != null) {
			health.TakeDamage (damage, transform);

			Instantiate (missileExplosion, transform.position, Quaternion.identity);
			sourceExplosion.PlayOneShot (clipExplosion);
			Destroy (gameObject);
		}
	}
}
