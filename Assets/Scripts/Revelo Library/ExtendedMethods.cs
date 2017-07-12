using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendedMethods {

	public static bool ContainsMember(this List<Member> me, Member other) {
		Member mbmr = me.Find (m => m.elementName == other.elementName && m.times == other.times);
		return mbmr!= null;
	}

}
