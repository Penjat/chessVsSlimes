﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour {

	protected PieceManager pieceManager;

	protected int pieceValue;

	protected Square square;
	private Animator animator;

	public GameObject explode;

	protected bool isMoving;
	protected Vector3 startMarker;//starting position of the move
	protected float startTime;
	protected float journeyLength;
	protected float speed = 10.0f;

	public virtual void SetUp(PieceManager pieceM, Square s){
		pieceManager = pieceM;
		animator = GetComponent<Animator>();
		SetPos(s);
	}
	public void Update(){
		if(isMoving){
			if(!Moving()){
				isMoving = false;
				EndTurn();

			}
		}
	}

	public virtual bool Moving(){
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startMarker, square.transform.position, fracJourney);
		if(journeyLength-distCovered < .1f){

			transform.position = square.transform.position;
			return false;
		}
		return true;
	}

	public void SetPos(Square s){
		square = s;
		transform.position = square.transform.position;//TODO position more accuratly
		square.SetPiece(this);
	}
	public void SetSelected(GridManager gridManager){
		SetSelected(true);
		GetPosMoves(gridManager);
	}
	public void SetSelected(bool b){
		//TODO animate piece selected
		if(b){
			animator.Play("pieceSel");
			return;
		}
		animator.Play("pieceNorm");

	}

	public void Take(){
		//reset the squares piece if it is the same as this piece
		if(square != null && square.GetPiece() == this){
			square.SetPiece(null);
		}
		explode.transform.SetParent(null);
		explode.SetActive(true);
		Destroy(explode, 5.0f);

		pieceManager.RemovePiece(this);
		Destroy(gameObject);
	}

	public virtual void MoveTo(Square newSquare){
		if(square != null){
			square.SetPiece(null);
		}
		square = newSquare;
		//transform.position = square.transform.position;
		startMarker = transform.position;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker, square.transform.position);
		isMoving = true;
		square.SetPiece(this);
		//SetPos(newSquare);
	}
	public virtual void GetPosMoves(GridManager gridManager){
		//show all the posible moves for this piece

		GetQueenMoves(gridManager);
	}


	protected void GetRookMoves(GridManager gridManager){
		//check forward
		CheckLine(gridManager,1,0);
		CheckLine(gridManager,0,1);
		CheckLine(gridManager,-1,0);
		CheckLine(gridManager,0,-1);

	}

	protected void GetBishopMoves(GridManager gridManager){
		//check forward
		CheckLine(gridManager,1,1);
		CheckLine(gridManager,-1,1);
		CheckLine(gridManager,-1,-1);
		CheckLine(gridManager,1,-1);

	}

	protected void GetQueenMoves(GridManager gridManager){
		GetBishopMoves(gridManager);
		GetRookMoves(gridManager);
	}
	protected void GetKingMoves(GridManager gridManager){
		CheckLine(gridManager,1,1,1);
		CheckLine(gridManager,-1,1,1);
		CheckLine(gridManager,-1,-1,1);
		CheckLine(gridManager,1,-1,1);

		CheckLine(gridManager,1,0,1);
		CheckLine(gridManager,0,1,1);
		CheckLine(gridManager,-1,0,1);
		CheckLine(gridManager,0,-1,1);
	}
	protected void CheckLine(GridManager gridManager,int deltaX,int deltaZ){
		CheckLine(gridManager,deltaX,deltaZ,20);
	}
	protected void CheckLine(GridManager gridManager,int deltaX,int deltaZ,int numberOfSteps){
		//checks a line starting from the player's position
		//deltaX and delta z are either 1,-1, or 0

		//TODO add number of steps
		//count up one step at a time
		for(int step=1;step<=numberOfSteps;step++){

			int x = square.GetX() + step*deltaX;
			int z = square.GetZ() + step*deltaZ;
			Square nextSquare = gridManager.GetSquare(x,z);

			//if reaches the end of the board, stop checking
			if(nextSquare == null || !nextSquare.GetAvailable()){
				return;
			}
			if(nextSquare.HasEnemy()){
				//set the square as takable but stop checking
				nextSquare.SetCanTake(true);
				return;
			}
			if(nextSquare.HasPiece()){
				//if the square is blocked by another piece, stop checking
				return;
			}
			nextSquare.SetPossible(true);

		}
	}
	protected void CheckSquare(int x,int z,GridManager gridManager){
		//highlights the square if it is posible to move there
		Square nextSquare = gridManager.GetSquare(x,z);
		if(nextSquare != null && nextSquare.HasEnemy()){
			nextSquare.SetCanTake(true);
			return;
		}
		if(nextSquare == null ||  nextSquare.HasPiece() || !nextSquare.GetAvailable()){
			return;
		}

			//TODO don't need to check if enemy because already checked earlier
		nextSquare.SetPossible(true);


	}
	public void EndTurn(){
		pieceManager.EndTurn();
	}
	public int GetPieceValue(){
		return pieceValue;
	}
}
