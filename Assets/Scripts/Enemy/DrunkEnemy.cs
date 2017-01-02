using UnityEngine;
using System.Collections;

public class DrunkEnemy : MonoBehaviour, IHealth {

	// Use this for initialization
	[SerializeField] GameObject bullet;
	[SerializeField] AudioSource audioSrouce;
	[SerializeField] AudioClip shootClip;
	[SerializeField] GameObject explosionEnemy, explosionWhenHit;
	[SerializeField] AudioClip explosionClip;

	private float randomDirectx,randomDirecty,delayTime,nextTime;
	private int randomRotate;
	[SerializeField] float speed, health;
	private Rigidbody2D myBody;

	// Use this for initialization
	void Awake(){
		myBody = GetComponent<Rigidbody2D> ();
		randomDirectx = Random.Range (-speed, speed);
		randomDirecty = Random.Range (-speed, speed);
		randomRotate = Random.Range (-1, 1);
		if (randomRotate == 0)
			randomRotate = 1;
		nextTime = 0f;
	}

	void Start () {
		StartCoroutine (shoot ());
	}

	// Update is called once per frame
	void FixedUpdate () {
		move ();
	}

	void move(){
		delayTime = Random.Range (.1f, 2f);
		if (Time.time > nextTime) {
			myBody.velocity = new Vector2 (Random.Range(-speed,speed), -speed);
			nextTime = Time.time + delayTime;
		}
	}

	IEnumerator shoot(){
		yield return new WaitForSeconds (Random.Range (.5f, 1.5f));
		Vector3 temp = transform.position;
		temp.y -= 0.5f;
		audioSrouce.PlayOneShot (shootClip);
		Instantiate (bullet, temp, Quaternion.identity);
		StartCoroutine (shoot ());
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Bounds") {
			Destroy (gameObject);
		}
	}

	public void TakeDamage(float damage, Transform attacker){
		health -= damage;
		Instantiate (explosionWhenHit, attacker.position, Quaternion.identity);
		if (health <= 0) {
			Destroy (gameObject);
			Vector3 tempPos = transform.position;
			Instantiate (explosionEnemy, tempPos, Quaternion.identity);
			AudioSource.PlayClipAtPoint (explosionClip, tempPos);

			if (ScoreManager.instance != null) {
				ScoreManager.instance.setScore ();
			}
		}
	}
}
