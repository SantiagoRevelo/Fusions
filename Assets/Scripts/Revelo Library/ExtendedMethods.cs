using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendedMethods {

	public static bool ContainsMember(this List<ConnectionMember> me, ConnectionMember other) {
		ConnectionMember mbmr = me.Find (m => m.elementName == other.elementName && m.times == other.times);
		return mbmr!= null;
	}

	public static string GetNamesString(this List<Element> list) {
		string ret = "";
		for (int i = 0; i < list.Count; i++) {
			ret += list [i].name;
		}
		return ret;
	}
}
