using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	[SerializeField]
	private GameObject[] Enemy;

	[SerializeField]
	private BoxCollider2D box;

	void Awake(){
		box = GetComponent<BoxCollider2D> ();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (spawnEnemy ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator spawnEnemy(){
		yield return new WaitForSeconds (.2f);

		// Theo toa do cua box, vi du size box la 5 thi toa do la -2.5,0,2.5
		float minX = -box.bounds.size.x / 2;
		float maxX = box.bounds.size.x / 2;

		int enemyRandomIndex = Random.Range (0, Enemy.Length);

		Vector3 temp = transform.position;
		temp.x = Random.Range (minX, maxX);
		Instantiate (Enemy[enemyRandomIndex], temp, Quaternion.identity);

		StartCoroutine (spawnEnemy ());
	}
}
