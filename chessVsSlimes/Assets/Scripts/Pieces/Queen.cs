using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece{
	public override void SetUp(PieceManager pieceM, Square s){
		base.SetUp(pieceM,s);
		pieceValue = 5;

	}
	public override void GetPosMoves(GridManager gridManager){
		//show all the posible moves for this piece

		GetQueenMoves(gridManager);
	}
}
