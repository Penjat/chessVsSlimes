using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungrySlime : Enemy {

	public override bool FindMove(GridManager gridManager){
		Square nextSquare = CheckAround(gridManager);
		if(nextSquare != null){
			nextSquare.GetPiece().Take();
			MoveTo(nextSquare);
			return true;
		}

		return false;
	}

	
	public Square CheckAround(GridManager gridManager){
		Vector2[] pointsAround =
		{new Vector2(1,0),
			new Vector2(1,1),
			new Vector2(0,1),
			new Vector2(-1,0),
			new Vector2(-1,1),
			new Vector2(-1,-1),
			new Vector2(0,-1),
			new Vector2(1,-1),};

			Square curBestSquare = null;

			foreach(Vector2 v in pointsAround){
				Square s = gridManager.GetSquare(square,(int)v.x,(int)v.y);
				if(s != null && s.HasPiece()){

					//if the curBestSquare is null or the newSquare has a piece of higher value
					//set the new square to the current best
					//greedy algorithm
					if(curBestSquare == null || curBestSquare.GetPiece().GetPieceValue() < s.GetPiece().GetPieceValue() ){
						curBestSquare = s;
					}

				}
			}
			return curBestSquare;
	}



}
