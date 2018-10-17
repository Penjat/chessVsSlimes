using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	private Square square;

	public void SetPos(Square s){
		square = s;
		transform.position = square.transform.position;//TODO position more accuratly
	}
}
