using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Elementos unidos por una linea
/// </summary>
public class Connection {
	// TODO: convertir startElement y endElement en una lista de elementos.
	Line line;
	List<Element> elements = new List<Element> ();
	public string ConnectionName {
		get;
		set;
	}

	public Connection(List<Element> elems, Line l) {
		elems.Sort ((a, b) => a.name.CompareTo (b.name));
		elements = elems;
		line = l;
		ConnectionName = elements.GetNamesString ();
	}
}