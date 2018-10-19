using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

	private Piece piece;
	private Enemy enemy;
	private int x,z;
	private Animator animator;
	private bool isPosibleMove;
	private bool canTake;//there is an enemy here that can be taken

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


	public Enemy GetEnemy(){
		//returns null if no piece
		return enemy;
	}
	public void SetEnemy(Enemy e){
		enemy = e;
	}
	public bool HasEnemy(){
		return (enemy != null);
	}

	public void Clear(){
		//clears of is possible and canTake
		animator.Play("notPos");
		canTake = false;
		isPosibleMove = false;

	}
	public void SetCanTake(bool b){
		canTake = b;
		if(b){
			animator.Play("canTake");
			return;
		}
		animator.Play("notPos");
	}
	public void SetPossible(bool b){
		//sets if it is a posible move
		isPosibleMove = b;
		if(b){
			animator.Play("isPos");
			return;
		}
		animator.Play("notPos");

	}

	public bool GetIsPossible(){
		return isPosibleMove;
	}
	public bool GetCanTake(){
		return canTake;
	}


	public int GetX(){
		return x;
	}
	public int GetZ(){
		return z;
	}


}
