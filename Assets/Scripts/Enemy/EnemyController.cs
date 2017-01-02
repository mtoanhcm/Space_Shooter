using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour, IHealth {

	// Use this for initialization
	[SerializeField] float speed,health;
	[SerializeField] GameObject bullet;
	[SerializeField] AudioSource audioSrouce;
	[SerializeField] AudioClip shootClip;
	[SerializeField] GameObject explosionEnemy,explosionWhenHit;
	[SerializeField] AudioClip explosionClip;

	Rigidbody2D myBody;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		StartCoroutine (shoot ());
	}

	// Update is called once per frame
	void FixedUpdate () {
		myBody.velocity = new Vector2 (0, -speed);
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
		if (target.tag == "Rock") {
			Destroy (gameObject);
			Instantiate (explosionEnemy, target.transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint (explosionClip, target.transform.position);
		}

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
