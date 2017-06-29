using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {

	public LineRenderer lineRenderer;
	public EdgeCollider2D edgeCol;
	Vector3 startPosition;
	Vector3 endPosition;

	void Awake () {
		lineRenderer.positionCount = 2;
	}

	void SetStartPoint(Vector3 point) {
		lineRenderer.SetPosition (0, point);
	}

	void SetEndPoint(Vector3 point) {
		lineRenderer.SetPosition (1, point);
	}
}
