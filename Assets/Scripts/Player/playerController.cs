using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerController : MonoBehaviour, IHealth {

	public float speed,playerHP;
	private bool isDrag;
	Component[] mySinglegunCom;

	[SerializeField] GameObject yellowBullet;
	[SerializeField] GameObject explosionWhenHit;
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip playerExplosion;
	[SerializeField] ParticleSystem playerExplose;

	// Use this for initialization
	void Awake () {
		isDrag = false;
	}

	void Start(){
		//Cau truc lay componet cua tat ca child (bao gom grandchild)
		mySinglegunCom = GetComponentsInChildren<SingleGun> ();
	}

	void Update(){
		OnMouseOver ();
		//touchMove ();
	}

	void OnMouseOver(){
		//detect gameobject khi click vao, set trang thai cho phep drag
		//dung layermask de set va cham voi layer Player, so 1 la cu phap mac dinh
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction,Mathf.Infinity, 1 << LayerMask.NameToLayer("Player"));
			if(hit){
				isDrag = true;
			} else isDrag = false;
		}

		//Gan vi tri cua player vao vi tri cua mouse
		if (Input.GetMouseButton (0)) {
			if (isDrag == true){
				Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector3 playerPos = transform.position;

				playerPos.x = touchPos.x;
				playerPos.y = touchPos.y;
				transform.position = new Vector3(playerPos.x,playerPos.y,0);
			}
		}

	}

	void touchMove(){
		//phat hien khi co su kien touch
		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				//tao 1 tia raycast tu vi tri touch xuong, neu cham voi layer Player thi set dieu kien cho phep drag
				Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
				RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity, 1 << LayerMask.NameToLayer ("Player"));
				if (hit) {
					isDrag = true;
				}
			}
			//su kien xay ra neu touch move tren man hinh
			//lay vi tri nhan vat gan vao vi tri touch move
			if (Input.GetTouch (0).phase == TouchPhase.Moved) {
				if (isDrag) {
					Vector3 touchPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
					Vector3 playerPos = transform.position;

					playerPos.x = touchPos.x;
					playerPos.y = touchPos.y;
					transform.position = new Vector3 (playerPos.x, playerPos.y, 0);
				}
			}
		} else
			isDrag = false;
	}

	void playerDie(){
		Destroy (gameObject);

		AudioSource.PlayClipAtPoint (playerExplosion, transform.position);
		Instantiate (playerExplose, transform.position, Quaternion.identity);

		//SetSingleGunShoot (false);

		if (gamePlayController.instance != null) {
			gamePlayController.instance.PlayerDiedShowPanel ();
		}

		if (ScoreManager.instance != null) {
			ScoreManager.instance.scoreText.gameObject.SetActive (false);
		}

		//GameObject.Find ("Score Text").SetActive (false);
	}

	public void TakeDamage(float damage, Transform attacker){
		playerHP -= damage;
		Instantiate (explosionWhenHit, attacker.position, Quaternion.identity);
		//myAnim.Play ("PlayerHit",-1,0f);
		if (playerHP <= 0) {
			playerDie ();
		}
	}

	void OnTriggerEnter2D(Collider2D target){
		if (target.tag == "Rock") {
			playerDie ();
		}
	}

	void SetSingleGunShoot(bool myShoot){
		for (int i = 0; i < mySinglegunCom.Length; i++) {
			mySinglegunCom [i].GetComponent<SingleGun> ().SetIsShoot (myShoot);
		}
	}

}
