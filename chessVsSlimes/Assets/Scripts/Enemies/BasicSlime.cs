using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlime : Enemy {

	public override bool FindMove(GridManager gridManager){
		//returns true if can move false if can't
		Square nextSquare = gridManager.GetSquare(square ,1,0);
		if(nextSquare == null || !nextSquare.GetAvailable()){
			return false;
		}
		//TODO check for other enemies
		if(nextSquare.HasPiece()){
			nextSquare.GetPiece().Take();
		}
		if(nextSquare.HasEnemy()){
			nextSquare.GetEnemy().Take();
		}
		MoveTo(nextSquare);
		return true;
	}

}
