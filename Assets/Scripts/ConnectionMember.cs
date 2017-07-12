using UnityEngine;
using System.Collections;

public class ConnectionMember {
	public string elementName;
	public int times;

	public ConnectionMember(string name = "", int count = 0) {
		elementName = name;
		times = count;
	}
}