using UnityEngine;
using System.Collections;

public class PartnerBullet : MonoBehaviour {

	private Rigidbody2D myBody;
	[SerializeField] float speed, damage;

	void Awake(){
		damage = 1;
		speed = 10;
	}

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		myBody.velocity = transform.up * speed;
	}

	void OnTriggerEnter2D(Collider2D target){
		var health = target.gameObject.GetComponent<IHealth> ();
		if (health != null) {
			health.TakeDamage (damage,transform);
			Destroy (gameObject);
		}

		if (target.tag == "Bounds") {
			Destroy (gameObject);
		}
	}
}
