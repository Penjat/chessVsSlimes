using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece {

	//TODO include a direction
	private bool firstMove;//can move two spaces if is first move

	public override void SetUp(PieceManager pieceM, Square s){
		base.SetUp(pieceM,s);
		pieceValue = 1;
		firstMove = true;
	}
	public override void GetPosMoves (GridManager gridManager){
		if(firstMove){
			CheckLine(gridManager,1,0,2);
			return;
		}
		CheckLine(gridManager,1,0,1);

	}
	public override void MoveTo(Square newSquare){
		base.MoveTo(newSquare);
		firstMove = false;
	}
}
