using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour, IHealth {
	[SerializeField] private float speed,rockHp,damage;
	private Rigidbody2D myBody;

	public int numSmallRocks;

	private int randomRotate;

	[SerializeField] GameObject explosionRock;
	[SerializeField] GameObject smallRock;
	[SerializeField] GameObject explosionWhenHit;
	[SerializeField] AudioClip explosionClip;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		randomRotate = Random.Range (-1, 1);
		if (randomRotate == 0)
			randomRotate = 1;
	}

	// Update is called once per frame
	void FixedUpdate () {
		myBody.velocity = new Vector2 (0, -speed);
		transform.Rotate (new Vector3 (0, 0, randomRotate*70) * Time.deltaTime);
	}


	void smallRockCreate(){
		for (int i = 0; i < numSmallRocks; i++) {
			Instantiate (smallRock, transform.position, Quaternion.identity);
		}
	}

	public void TakeDamage(float damage, Transform attacker){
		rockHp -= damage;
		Instantiate (explosionWhenHit, attacker.position, Quaternion.identity);
		if (rockHp <= 0) {
			Vector3 tempPos = transform.position;
			Instantiate (explosionRock, tempPos, Quaternion.identity);
			AudioSource.PlayClipAtPoint (explosionClip, tempPos);
			smallRockCreate();
			Destroy (gameObject);

			if (ScoreManager.instance != null) {
				ScoreManager.instance.setScore ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Bounds") {
			Destroy (gameObject);
		}

		var health = target.gameObject.GetComponent<IHealth> ();
		if (health != null) {
			health.TakeDamage (damage,transform);
			Instantiate (explosionRock, target.transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint (explosionClip, target.transform.position);
			smallRockCreate();
			Destroy (gameObject);
		}
	}
}
