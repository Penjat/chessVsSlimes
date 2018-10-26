using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece  {

	public override void SetUp(PieceManager pieceM, Square s){
		base.SetUp(pieceM,s);
		pieceValue = 3;

	}
	public override void GetPosMoves(GridManager gridManager){
		//show all the posible moves for this piece

		GetBishopMoves(gridManager);
	}
}
