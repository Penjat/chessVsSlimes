using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	private Square square;
	private Animator animator;

	public void SetUp(Square s){
		animator = GetComponent<Animator>();
		SetPos(s);
	}

	public void SetPos(Square s){
		square = s;
		transform.position = square.transform.position;//TODO position more accuratly
		square.SetPiece(this);
	}
	public void SetSelected(bool b){
		//TODO animate piece selected
		if(b){
			animator.Play("pieceSel");
			return;
		}
		animator.Play("pieceNorm");

	}
	public void MoveTo(Square newSquare){
		if(square != null){
			square.SetPiece(null);
		}
		SetPos(newSquare);
	}
}
