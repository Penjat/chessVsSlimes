using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	private const string TAG = "INPUT MANAGER: ";

	private Square curSquare;//the square player is currently pointing at

	void Update () {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		int mask = 1<<8;
		//only raycast to the Grid layer
		//should only be squares on this layer
		if (Physics.Raycast(ray, out hit,50.0f,mask)){
			if(curSquare != null){
				curSquare.IsLight(false);
			}
			Square square = hit.collider.GetComponentInParent<Square>();
			Debug.Log(TAG + "hit square x = " + square.GetX() + " y = " + square.GetZ());
			square.IsLight(true);
			curSquare = square;
			return;
		}
		Debug.Log(TAG + "didn't hit nothin'.");
		if(curSquare != null){
			curSquare.IsLight(false);
		}
		curSquare = null;

	}
}
