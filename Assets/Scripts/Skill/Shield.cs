using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour, IHealth {
	//private Rigidbody2D myBody;

	[SerializeField] float shieldDur, nextTimeActive;
	private bool isActive;
	private float currenTimeActive;
	CircleCollider2D shieldBox;
	string type = "Support";

	// Update is called once per frame
	void Awake () {
		shieldBox = GetComponent<CircleCollider2D> ();
		isActive = true;
	}

	void Update(){
		ShieldActive ();
	}

	void ShieldActive(){
		if (isActive) {
			if (shieldDur <= 0) {
				shieldBox.enabled = false;
				gameObject.GetComponent<SpriteRenderer> ().color = new Color(1,1,1, .1f);
				isActive = false;
				currenTimeActive = Time.time + nextTimeActive;
			}
		} else if (!isActive){
			if (Time.time > currenTimeActive) {
				shieldBox.enabled = true;
				gameObject.GetComponent<SpriteRenderer> ().color = new Color(1,1,1,1);
				shieldDur = 3;
				isActive = true;
			}
		}
	}

	public void TakeDamage(float damage, Transform attacker){
		shieldDur -= damage;
	}

	public string GetType(){
		return this.type;
	}
}
