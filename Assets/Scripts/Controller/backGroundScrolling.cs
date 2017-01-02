using UnityEngine;
using System.Collections;

public class backGroundScrolling : MonoBehaviour {

	public static backGroundScrolling instance;

	public float speed;
	private Vector3 offset = Vector3.zero;
	private Material mat;

	void MakeSingleton(){
		if (instance != null) {
			Destroy (gameObject);
		} else if (instance == null) {
			instance = this;
			DontDestroyOnLoad (instance);
		}
	}

	void Awake(){
		MakeSingleton ();
	}

	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer> ().material;
		BackgroundScale ();
		//phải gõ đúng _MainTex
		offset = mat.GetTextureOffset ("_MainTex");
	}
	
	// Update is called once per frame
	void Update () {
		offset.y += speed * Time.deltaTime;
		mat.SetTextureOffset ("_MainTex", offset);
	}

	void BackgroundScale(){
		// thiết lập hình nền co giãn theo độ rộng của camera
		var worldHeight = Camera.main.orthographicSize * 2;
		var worldWidth = worldHeight * Screen.width / Screen.height;
		//scale background theo đúng tỷ lệ của màn hình
		transform.localScale = new Vector3 (worldWidth, worldHeight, 0);
	}
}
