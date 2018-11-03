using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlime : Enemy {

	public override bool FindMove(GridManager gridManager,string action){
		//passes in a string for the action to be performed
		switch(action){

			case "hungry":
				return CheckHungry(gridManager);

			case "hop":
				return Hop(gridManager);

			default:
				Debug.Log("didn't recognise action " + action);
				return false;
		}

}
	public bool Hop(GridManager gridManager){
		//hops forward in the current direction
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

	public bool CheckHungry(GridManager gridManager){
		//checks around for any pieces to attack
		Square nextSquare = CheckAround(gridManager);
		if(nextSquare != null){
			nextSquare.GetPiece().Take();
			MoveTo(nextSquare);
			return true;
		}

		return false;
	}

	public Square CheckAround(GridManager gridManager){
		//checks the surounding squares and returns square with the highest piece
		//returns null if no piece found

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
