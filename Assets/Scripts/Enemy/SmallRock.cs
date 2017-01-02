using UnityEngine;
using System.Collections;

public class SmallRock : MonoBehaviour, IHealth {

	public float speed,rockHp,damage;
	private Rigidbody2D myBody;
	private int randomRotate;
	private float randomDirectx,randomDirecty;

	[SerializeField] private GameObject explosionRock;
	[SerializeField] private GameObject explosionWhenHit;
	[SerializeField] private AudioClip explosionClip;

	// Use this for initialization
	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		randomDirectx = Random.Range (-speed, speed);
		randomDirecty = Random.Range (-speed, speed);
		randomRotate = Random.Range (-1, 1);
		if (randomRotate == 0)
			randomRotate = 1;
		//Debug.Log (randomDirect);
	}

	// Update is called once per frame
	void FixedUpdate () {
		myBody.velocity = new Vector2 (randomDirectx, randomDirecty);
		transform.Rotate (new Vector3 (0, 0, randomRotate*100) * Time.deltaTime);
	}

	#region take dame
	public void TakeDamage(float damage, Transform attacker){
		rockHp -= damage;
		Instantiate (explosionWhenHit, attacker.position, Quaternion.identity);
		if (rockHp <= 0) {
			Destroy (gameObject);
			Vector3 tempPos = transform.position;
			Instantiate (explosionRock, tempPos, Quaternion.identity);
			AudioSource.PlayClipAtPoint (explosionClip, tempPos);

			if (ScoreManager.instance != null) {
				ScoreManager.instance.setScore ();
			}
		}
	}
	#endregion

	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Bounds") {
			Destroy (gameObject);
		}

		var health = target.gameObject.GetComponent<IHealth> ();
		if (health != null) {
			health.TakeDamage (damage,transform);
			Destroy (gameObject);
			Instantiate (explosionRock, target.transform.position, Quaternion.identity);
			AudioSource.PlayClipAtPoint (explosionClip, target.transform.position);
		}
	}
}
