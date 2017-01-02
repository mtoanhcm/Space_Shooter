using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	float speed,delay,damage;
	CircleCollider2D myRange;
	[SerializeField] GameObject bombExplosion;

	// Use this for initialization
	void Awake () {
		speed = 3f;
		delay = 1.5f;
		damage = 10;
		myRange = GetComponent<CircleCollider2D> ();
	}

	void Start(){
		StartCoroutine (BombExplose ());
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.up * speed * Time.deltaTime);
	}

	IEnumerator BombExplose(){
		yield return new WaitForSeconds (delay);

		myRange.enabled = true;
		Instantiate (bombExplosion, transform.position, Quaternion.identity);
		Destroy (gameObject,0.1f);
	}

	void OnTriggerEnter2D(Collider2D target){
		var health = target.GetComponent<IHealth> ();
		if (health != null) {
			health.TakeDamage (damage, transform);
		}
	}
}
