using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public Camera mainCamera;

	public BoxCollider2D topWall;
	public BoxCollider2D rightWall;
	public BoxCollider2D bottomWall;
	public BoxCollider2D leftWall;

	void Awake() {
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject);
		}

		mainCamera = Camera.main;
	}

	// Use this for initialization
	void Start () {
		RelocateWalls ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RelocateWalls() {
		topWall.transform.position = Vector3.zero;
		topWall.transform.localScale = Vector3.one;
		topWall.size = new Vector2 (mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width * 2f, 0f, 0f)).x, 1f);
		topWall.offset = new Vector2 (0f, mainCamera.ScreenToWorldPoint (new Vector3 (0f, Screen.height, 0f)).y - 0.5f);

		rightWall.transform.position = Vector3.zero;
		rightWall.transform.localScale = Vector3.one;
		rightWall.size = new Vector2 (1f, mainCamera.ScreenToWorldPoint (new Vector3 (0f, Screen.height * 2f, 0f)).y);
		rightWall.offset = new Vector2 (mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width, 0f, 0f)).x + 0.5f, 0f);

		bottomWall.transform.position = Vector3.zero;
		bottomWall.transform.localScale = Vector3.one;
		bottomWall.size = new Vector2 (mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width * 2f, 0f, 0f)).x, 1f);
		bottomWall.offset = new Vector2 (0f, mainCamera.ScreenToWorldPoint (new Vector3 (0f, 0f, 0f)).y + 0.5f);

		leftWall.transform.position = Vector3.zero;
		leftWall.transform.localScale = Vector3.one;
		leftWall.size = new Vector2 (1f, mainCamera.ScreenToWorldPoint (new Vector3 (0f, Screen.height * 2f, 0f)).y);
		leftWall.offset = new Vector2 (mainCamera.ScreenToWorldPoint (new Vector3 (0f, 0f, 0f)).x - 0.5f, 0f); 
	}
}
