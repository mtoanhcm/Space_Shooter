using UnityEngine;
using System.Collections;

public class bigBullet : MonoBehaviour {

	private Rigidbody2D myBody;
	[SerializeField] SingleGun myGun;
	[SerializeField] float speed, damage,trueDamage;
	string type = "Bullet";

	void Awake(){
		damage = 1;
		SetTrueDagame ();
	}

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		transform.parent = null;

	}

	// Update is called once per frame
	void FixedUpdate () {
		myBody.velocity = transform.up * speed;
	}

	void OnTriggerEnter2D(Collider2D target){
		var health = target.gameObject.GetComponent<IHealth> ();
		if (health != null) {
			health.TakeDamage (trueDamage,transform);
			Destroy (gameObject);
		}

		if (target.tag == "Bounds") {
			Destroy (gameObject);
		}
	}

	void SetTrueDagame(){
		float gunDamage = myGun.GetDamageStat ();
		trueDamage = damage + gunDamage;
	}

	public string GetType(){
		return this.type;
	}
}
