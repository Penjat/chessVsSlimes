using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece  {

	public override void GetPosMoves(GridManager gridManager){
		//show all the posible moves for this piece

		GetBishopMoves(gridManager);
	}
}
