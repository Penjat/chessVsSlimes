using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

	private Piece piece;
	private int x,z;
	private Animator animator;

	public void SetPos(int xPos, int zPos){
		x = xPos;
		z = zPos;
		animator = GetComponent<Animator>();
	}
	public void IsLight(bool b){
		//is the square lit up
		animator.SetBool("isLight",b);
	}
	public Piece GetPiece(){
		//returns null if no piece
		return piece;
	}
	public void SetPiece(Piece p){
		piece = p;
	}
	public bool HasPiece(){
		return (piece != null);
	}
	public int GetX(){
		return x;
	}
	public int GetZ(){
		return z;
	}
}
