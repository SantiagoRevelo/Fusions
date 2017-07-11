using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

	public LineRenderer lineRenderer;
	public EdgeCollider2D edgeCol;

	Element from;

	public Element From {
		get { return from;}
		private set { from = value; }
	}

	Element to;

	public Element To {
		get { return to;}
		private set { to = value; }
	}

	Vector3 startPosition;
	Vector3 endPosition;

	void Awake () {
		lineRenderer = GetComponent<LineRenderer> ();
		edgeCol = GetComponent<EdgeCollider2D> ();
	}

	void Start () {
		lineRenderer.positionCount = 2;
	}

	void Update () {
		if (lineRenderer != null) {
			if (from != null) {
				startPosition = from.transform.position;
				endPosition = (to == null) ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : to.transform.position;
			}
		}

		lineRenderer.SetPosition (0, startPosition);
		lineRenderer.SetPosition (1, endPosition);

		edgeCol.points = new Vector2[]{startPosition, endPosition};
	}

	public void SetStartElement(Element start) {
		From = start;
	}

	public void SetEndElement(Element end) {
		To = end;
	}

}
