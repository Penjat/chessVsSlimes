using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece {

	public override void GetPosMoves(GridManager gridManager){
		//show all the posible moves for this piece

		GetKingMoves(gridManager);
	}
}
