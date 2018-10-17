using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

	private Piece piece;
	private int x,z;

	public void SetPos(int xPos, int zPos){
		x = xPos;
		z = zPos;
	}
	public Piece GetPiece(){
		//returns null if no piece
		return piece;
	}
	public void SetPiece(Piece p){
		piece = p;
	}
}
