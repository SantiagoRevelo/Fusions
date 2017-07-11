using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElementType {
	Circulo_Borde_Punteado,
	Circulo_Borde_Liso,
	Circulo_Solido,
	Estrella
}

public class Element : MonoBehaviour {

	public ElementType type;
	public Sprite[] sprites;
	public Color32 color;

	public Vector2 dir;
	public float speed;
	public bool isSelected;

	private bool connected;

	private SpriteRenderer sr;

	void Awake () {
		if (sr == null)
			sr = GetComponent<SpriteRenderer> ();

		// Debug Only ///////////////////////
		sr.sprite = sprites [(int)type];
		sr.color = color;
		/////////////////////////////////////
	}

	void Start () {
		//velocity = new Vector2 (Random.Range (-2.0f, 2.0f), Random.Range (-2.0f, 2.0f));
		dir = new Vector2 ( Random.Range (-1.0f, 1.01f), Random.Range(-1.0f, 1.1f) );
	}
	
	// Update is called once per frame
	void Update () {
		if (!isSelected) {
			transform.Translate (dir * speed * Time.deltaTime);
		} else {
			isSelected = !Input.GetMouseButtonUp (0);
		}
	}

	public void Setup (string name, ElementType t, Color32 c) {
		type = t;
		color = c;
		sr.color = color;
		sr.sprite = sprites [(int)type];
	}

	void OnTriggerEnter2D (Collider2D col) {
		if ( col.name.ToLower ().Contains ("wall") ) {
			if ( col.name.ToLower ().Contains ("top") || col.name.ToLower ().Contains ("bottom") ) {
				dir.y *= -1;
			} else if ( col.name.ToLower ().Contains ("right") || col.name.ToLower ().Contains ("left")) {
				dir.x *= -1;
			}
		}
	}
}
