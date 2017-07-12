using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Grupo de conexiones
/// </summary>
public class ConnectionsGroup {

	public List<Connection> connectionsList;
	public int ID { get; private set;}

	public List<ConnectionMember> memebersList = new List<ConnectionMember> ();

	public ConnectionsGroup(int index) {
		ID = index;
		connectionsList = new List<Connection> ();
	}

	public void AddConnection(Element from, Element to, Line line) {
		Connection newConn = new Connection(new List<Element> () {from, to}, line);
		// TODO: Comprobar que la conecction no existe ya (para que no se dupliquen las lineas
		if (connectionsList.Find(c => c.ConnectionName == newConn.ConnectionName) == null) {
			connectionsList.Add (newConn);

			AddElementToMembers (from);
			AddElementToMembers (to);

			// TODO: No se si esto es muy necesario mas allá de ser una ayuda visual en el editor
			GameObject emptyGO = GameObject.Find("Connection_" + ID);
			if (emptyGO == null)
				emptyGO = new GameObject ("Connection_" + ID);

			from.transform.SetParent (emptyGO.transform);
			to.transform.SetParent (emptyGO.transform);

			line.name = "Line_" + newConn.ConnectionName;
			line.transform.SetParent(emptyGO.transform);
			Debug.LogFormat ("Nueva conexión: [{0}] - [{1}]", from.name, to.name);
					
			if (memebersList.Count >= 3) {
				CollapseGroup ();
			}
		} else {
			GameObject.Destroy (line.gameObject);
			Debug.LogFormat ("<color=orange> La Connection [{0}]-[{1}] ya existe </color>", from.name, to.name);
		}
	}

	private void AddElementToMembers ( Element elem) {
		ConnectionMember newMember = new ConnectionMember(elem.name, 1);
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
		ConnectionMember maxConnectedMember = new ConnectionMember ();
		foreach (ConnectionMember m in memebersList) {
			if (maxConnectedMember.elementName == string.Empty) {
				maxConnectedMember = m;
			}

			if (m.times > maxConnectedMember.times) {
				maxConnectedMember = m;
			}
		}
	}

}
