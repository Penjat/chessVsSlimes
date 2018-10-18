using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

	protected Square square;
	private Animator animator;

	public virtual void SetUp(Square s){
		animator = GetComponent<Animator>();
		SetPos(s);
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
	public virtual void MoveTo(Square newSquare){
		if(square != null){
			square.SetPiece(null);
		}
		SetPos(newSquare);
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
			if(nextSquare == null){
				return;
			}
			if(nextSquare.HasPiece()){
				return;
			}
			nextSquare.SetPossible(true);

		}
	}
	protected void CheckSquare(int x,int z,GridManager gridManager){
		//highlights the square if it is posible to move there
		Square square = gridManager.GetSquare(x,z);
		if(square != null && !square.HasPiece()){
			square.SetPossible(true);
		}
	}
}
