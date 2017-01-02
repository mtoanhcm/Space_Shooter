using UnityEngine;
using System.Collections;

public class SingleGun : MonoBehaviour {

	[SerializeField] float gunDamage;
	[SerializeField] GameObject[] Bullet;
	[SerializeField] AudioClip shootClip;
	[SerializeField] float damageStat,fireRate;
	GameObject theBullet;
	Transform myGunTrans;
	bool isShoot;

	// Use this for initialization
	void Awake () {
		
		isShoot = true;
	}

	void Start(){
		CheckKindOfBullet (gamePlayController.instance.GetNameOfBullet());
		StartCoroutine (Shoot ());
	}

	//Đạn bắn ra là con của Gun, do muốn đạn bắn theo hướng xoay của Gun nhưng ko thể lấy Script trực tiếp trong prefab được nên phải set là con để lấy từ parent
	public IEnumerator Shoot(){
		if (isShoot) {

			yield return new WaitForSeconds (.01f);
			Vector3 gunPosition = transform.position;
			GameObject myBulletTemp = Instantiate (theBullet, gunPosition, Quaternion.Euler (transform.rotation.eulerAngles)) as GameObject;
			//myBulletTemp.transform.SetParent (transform);
			AudioSource.PlayClipAtPoint (shootClip, transform.position);

			yield return new WaitForSeconds (fireRate);
			StartCoroutine (Shoot ());
		}
	}

	void CheckKindOfBullet(string bulletName){
		if (bulletName == "NormalBullet") {
			theBullet = Bullet [0];
			fireRate = 0.4f;
		} else if (bulletName == "BigBullet") {
			theBullet = Bullet [1];
			fireRate = 0.8f;
		} else if (bulletName == "LaserBullet") {
			theBullet = Bullet [2];
			fireRate = 1.6f;
		}
	}

	public float GetDamageStat(){
		return gunDamage;
	}

	public void SetIsShoot(bool shoot){
		isShoot = shoot;
	}

	public void SetFireRate(float myFireRate){
		fireRate = myFireRate;
	}
}
