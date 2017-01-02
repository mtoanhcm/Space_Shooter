using UnityEngine;
using System.Collections;

public class Protector : MonoBehaviour {

	[SerializeField] float rotateSpeed;
	[SerializeField] GameObject targetExplosion;
	string type = "Support";
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward*rotateSpeed);
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "EnemyBullet" || target.tag == "SmallRock") {
			Destroy (target.gameObject);
			Instantiate (targetExplosion, target.transform.position, Quaternion.identity);
		}
	}

	public string GetType(){
		return this.type;
	}
}
