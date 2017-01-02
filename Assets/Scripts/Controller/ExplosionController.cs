using UnityEngine;
using System.Collections;

public class ExplosionController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (explosionDestroy());
	}

	IEnumerator explosionDestroy(){
		yield return new WaitForSeconds (1f);
		Destroy (gameObject);
	}
}
