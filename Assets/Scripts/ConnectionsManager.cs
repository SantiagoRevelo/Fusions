﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Elementos unidos por una linea
/// </summary>
public struct Connection {
	// TODO: convertir startElement y endElement en una lista de elementos.
	public Element startElement;
	public Element endElement;
	Line line;

	public Connection(Element start, Element end, Line l) {
		startElement = start;
		endElement = end;
		line = l;
	}


}

public class Member {
	public string elementName;
	public int times;

	public Member(string name = "", int count = 0) {
		elementName = name;
		times = count;
	}
}

/// <summary>
/// Grupo de conexiones
/// </summary>
public class ConnectionsGroup {

	public List<Connection> connectionsList;
	public int ID { get; private set;}

	public List<Member> memebersList = new List<Member> ();

	public ConnectionsGroup(int index) {
		ID = index;
		connectionsList = new List<Connection> ();
	}

	public void AddConnection(Element from, Element to, Line line) {
		Connection newConn = new Connection(from, to, line);
		// TODO: Comprobar que la conecction no existe ya (para que no se dupliquen las lineas
		if (!connectionsList.Contains (newConn)) {
			connectionsList.Add (newConn);

			AddElementToMembers (from);
			AddElementToMembers (to);

			// TODO: No se si esto es muy necesario mas allá de ser una ayuda visual en el editor
			GameObject emptyGO = GameObject.Find("Connection_" + ID);
			if (emptyGO == null)
				emptyGO = new GameObject ("Connection_" + ID);
			
			from.transform.SetParent (emptyGO.transform);
			to.transform.SetParent (emptyGO.transform);
			line.name = "Line " + from.name + "-" + to.name;
			line.transform.SetParent(emptyGO.transform);

			Debug.LogFormat ("Nueva conexión: [{0}] - [{1}]", from.name, to.name);

			if (memebersList.Count >= 3) {
				CollapseGroup ();
			}
		} else {
			Debug.LogFormat ("<color=orange> La Connection [{0}]-[{1}] ya existe </color>", from.name, to.name);
		}
	}

	private void AddElementToMembers ( Element elem) {
		Member newMember = new Member(elem.name, 1);



		if (!memebersList.ContainsMember (newMember)) {
			memebersList.Add (newMember);
		} else {
			int id = -1;
			id = memebersList.FindIndex (m => m.elementName == elem.name);
			if (id >= 0) {
				memebersList [id].times++;			} 
			else {
				Debug.Log ("<color=magenta> Error WTF 1: El miembro existente no se ha encontrado para sumar ocurrencias </color>");
			}
		}			
	}

	private void CollapseGroup() {
		Member maxConnectedMember = new Member ();
		foreach (Member m in memebersList) {
			if (maxConnectedMember.elementName == string.Empty) {
				maxConnectedMember = m;
			}
			
			if (m.times > maxConnectedMember.times) {
				maxConnectedMember = m;
			}
		}
	}

}

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
				//KeyValuePair<string, string> entry in myDic
				foreach ( KeyValuePair<int, ConnectionsGroup> cg in connectionsGroupDictionary) {
					foreach (Member m in cg.Value.memebersList) {
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

		return ActiveLine;
	}

	public void ReleaseActiveLine () {
		ActiveLine = null;
	}
}