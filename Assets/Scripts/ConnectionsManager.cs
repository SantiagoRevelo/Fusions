using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionsManager : MonoBehaviour {

	public static ConnectionsManager instance = null; 

	public GameObject linePrefab;

	private Line activeLine;
	private GameObject activeLineGO;

	private Element startElement;
	private Element endElement;

	Dictionary<int, ConnectionsGroup> connectionsGroupDictionary = new Dictionary<int, ConnectionsGroup>();

	public Line ActiveLine {
		get { return activeLine; }
		private set { activeLine = value; }
	}

	void Awake() {
		if (instance == null) {
			instance = this;
		}
		else if (instance != this) {
			Destroy(gameObject);
		}
	}

	void Update () {
		if (GameInput.isTouchDown) {
			TapStart(GameInput.touchScreenPosition);
		} else if (GameInput.isTouchUp) {
			TapRelease(GameInput.touchScreenPosition);
		} else {
			Drag(GameInput.touchScreenPosition);
		}
	}

	Element GetTouchedElement(Vector2 pos) {
		RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
		if (hits != null) {
			foreach (RaycastHit2D hit in hits) {
				if (hit.transform.tag == "element") {
					Element elem = hit.transform.GetComponent<Element> ();
					return elem;
				}
			}
		}
		return null;
	}

	void TapStart (Vector2 pos) {
		Element elem = GetTouchedElement (pos);
		if (elem != null) {
			elem.isSelected = true;
			AddLine (elem);
			startElement = elem;
		}
	}

	void TapRelease (Vector2 pos) {
		if (ActiveLine != null) {
			Element elem = GetTouchedElement (pos);
			if ( elem != null && elem != startElement && startElement.type == elem.type && startElement.color.Equals(elem.color) ) {


				ActiveLine.SetEndElement (elem);
				endElement = elem;

				int ConnectionID = -1;
				//TODO: comprobar si hemos soltado encima de un elemento que ya pertenece a una Conecction
				foreach ( KeyValuePair<int, ConnectionsGroup> cg in connectionsGroupDictionary) {
					foreach (ConnectionMember m in cg.Value.memebersList) {
						if (endElement.name == m.elementName || startElement.name == m.elementName)
							ConnectionID = cg.Value.ID;
					}
				}

				ConnectionsGroup connG;
				if (ConnectionID == -1)
					connG = new ConnectionsGroup (connectionsGroupDictionary.Count);
				else
					connG = connectionsGroupDictionary[ConnectionID];

				connG.AddConnection (startElement, endElement, ActiveLine);
				connectionsGroupDictionary.Add (connectionsGroupDictionary.Count, connG);

			} 
			else {
				Destroy (ActiveLine.gameObject);
			}

			ActiveLine = null;
			startElement = null;
		}
	}

	void Drag (Vector2 pos) {
	}
	
	// Update is called once per frame
	public Line AddLine (Element elem) {
		activeLineGO = Instantiate (linePrefab);
		ActiveLine = activeLineGO.GetComponent<Line>();
		ActiveLine.SetStartElement(elem);
		ActiveLine.lineRenderer.startColor = elem.color;
		ActiveLine.lineRenderer.endColor = elem.color;

		return ActiveLine;
	}

	public void ReleaseActiveLine () {
		ActiveLine = null;
	}
}
