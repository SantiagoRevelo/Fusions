using UnityEngine;
using System.Collections;

public static class GameInput {
	
	
	public static Vector2 touchWorldPosition {
		get {  
			if ( Input.touches.Length > 0 ) {
            	return (Vector2)Camera.main.ScreenToWorldPoint(Input.touches[0].position);
			}
			return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}

	public static Vector2 touchScreenPosition {
		get {  
			if ( Input.touches.Length > 0 ) {
				return (Vector2)Input.touches[0].position;
			}
			return (Vector2)Input.mousePosition;
		}
	}
	
	public static bool isTouchUp {
		get { 
			return (Input.touches.Length > 0 
					&& (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled))
					|| Input.GetMouseButtonUp(0);
		}
	}
	
	public static bool isTouchDown {
		get { 
			return (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began) 
					|| Input.GetMouseButtonDown(0);
		}
			
	}
	
}
	