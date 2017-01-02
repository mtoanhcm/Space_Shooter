using UnityEngine;
using System.Collections;

public class laserBullet : MonoBehaviour {

	private Rigidbody2D myBody;
	[SerializeField] SingleGun myGun;
	[SerializeField] float speed, damage,trueDamage;

	void Awake(){
		damage = 10;
		SetTrueDagame ();
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
			health.TakeDamage (trueDamage,transform);
		}

		if (target.tag == "Bounds") {
			Destroy (gameObject);
		}
	}

	void SetTrueDagame(){
		float gunDamage = myGun.GetDamageStat ();
		trueDamage = damage + gunDamage;
	}
}
