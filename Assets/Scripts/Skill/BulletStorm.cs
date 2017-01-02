using UnityEngine;
using System.Collections;

public class BulletStorm: MonoBehaviour {

	[SerializeField] GameObject myBullet;
	Transform bulletStormSprite;
	float rotateSpeed, rotateToShoot, speed, currentFirerSpeed;
	bool isShoot;

	void Awake(){
		rotateSpeed = 9;
		speed = 0.06f;
		bulletStormSprite = transform.GetChild (0);
	}

	void Start(){
		if (myBullet != null) {
			StartCoroutine (RotateSpikeGUn ());
			StartCoroutine (MoveSpikeGun ());
			StartCoroutine (Shoot ());
		}
	}

	void RotateSpikeGunSprite(){
		StartCoroutine (RotateSpikeGUn ());
	}

	IEnumerator Shoot(){
		Instantiate (myBullet, bulletStormSprite.position, Quaternion.Euler (bulletStormSprite.rotation.eulerAngles));

		yield return new WaitForSeconds (0.05f);
		StartCoroutine (Shoot ());
	}

	IEnumerator RotateSpikeGUn(){
		bulletStormSprite.Rotate (Vector3.forward * rotateSpeed);
		yield return new WaitForSeconds (0.03f);

		StartCoroutine (RotateSpikeGUn ());
	}

	IEnumerator MoveSpikeGun(){
		transform.Translate (Vector3.up * speed);
		yield return new WaitForSeconds (0.015f);

		StartCoroutine (MoveSpikeGun ());
	}


	void OnTriggerEnter2D (Collider2D target)
	{
		if (target.tag == "Bounds") {
			Destroy (gameObject);
		}
	}
}
