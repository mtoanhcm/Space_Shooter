using UnityEngine;
using System.Collections;

public class redBullet : MonoBehaviour {

	[SerializeField] float speed,damage;
	private Rigidbody2D myBody;

	void Awake(){
		damage = 1;
	}

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		myBody.velocity = new Vector2 (0, -speed);
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
