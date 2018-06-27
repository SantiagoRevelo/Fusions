using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;


	public RectTransform GUI_TopContent;
	public RectTransform GUI_BottomContent;

	public BoxCollider2D topWall;
	public BoxCollider2D rightWall;
	public BoxCollider2D bottomWall;
	public BoxCollider2D leftWall;

	Camera mainCamera;

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
		
	}
	
	// Update is called once per frame
	void Update () {
		RelocateWalls ();
	}

	void RelocateWalls() {
		topWall.transform.position = mainCamera.transform.position;
		topWall.transform.localScale = Vector3.one;
		Vector2 screen = mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0f));
		Vector2 topC =  mainCamera.ScreenToWorldPoint (new Vector3 (GUI_TopContent.sizeDelta.x, GUI_TopContent.sizeDelta.y));

		topWall.size = new Vector2 (Mathf.Abs(topC.x) * 2, Mathf.Abs(topC.y));
		topWall.offset = new Vector2 (0f, (screen.y));



		rightWall.transform.position = mainCamera.transform.position;
		rightWall.transform.localScale = Vector3.one;
		rightWall.size = new Vector2 (1f, mainCamera.ScreenToWorldPoint (new Vector3 (0f, Screen.height * 2f, 0f)).y);
		rightWall.offset = new Vector2 (mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width, 0f, 0f)).x + 0.5f, 0f);

		bottomWall.transform.position = mainCamera.transform.position;
		bottomWall.transform.localScale = Vector3.one;
		float bottomWallHeight = mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width, GUI_BottomContent.rect.height)).y;
		bottomWall.size = new Vector2 (mainCamera.ScreenToWorldPoint (new Vector3 (Screen.width * 2f, 0f, 0f)).x, bottomWallHeight);
		bottomWall.offset = new Vector2 (0f, mainCamera.ScreenToWorldPoint (Vector3.zero).y + (bottomWallHeight * 0.5f));

		leftWall.transform.position = mainCamera.transform.position;
		leftWall.transform.localScale = Vector3.one;
		leftWall.size = new Vector2 (1f, mainCamera.ScreenToWorldPoint (new Vector3 (0f, Screen.height * 2f, 0f)).y);
		leftWall.offset = new Vector2 (mainCamera.ScreenToWorldPoint (new Vector3 (0f, 0f, 0f)).x - 0.5f, 0f); 
	}
}
