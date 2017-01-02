using UnityEngine;
using System.Collections;

public class PlayerBounds : MonoBehaviour {

	private float minX,maxX,minY,maxY;

	// Use this for initialization
	void Start () {
		Vector3 bounds = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
		minX = -bounds.x + .3f;
		maxX = bounds.x - .3f;
		minY = -bounds.y + .4f;
		maxY = bounds.y - .4f;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 temp = transform.position;
		if (temp.x < minX) {
			temp.x = minX;
		} else if (temp.x > maxX) {
			temp.x = maxX;
		}

		if (temp.y < minY) {
			temp.y = minY;
		} else if (temp.y > maxY) {
			temp.y = maxY;
		}

		transform.position = temp;
	}
}
