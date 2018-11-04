using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSlime : Enemy {

	public GameObject shockPrefab;

	public override bool FindMove(GridManager gridManager,string action){
		//passes in a string for the action to be performed
		switch(action){

			case "hungry":
				return CheckHungry(gridManager);

			case "hop":
				return Hop(gridManager);

			case "shock":
			Debug.Log("shocking");
				GameObject g = Instantiate(shockPrefab);
				ShockEffect shock = g.GetComponent<ShockEffect>();
				effectList.Add(shock);
				shock.Create(gridManager,square,this,shockPrefab);
				gridManager.UsingEffects();
				return Shock(gridManager);

			default:
				Debug.Log("didn't recognise action: " + action);
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
	public bool Shock(GridManager gridManager){

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
		List<Square> posSquares = square.CheckAroundSelf(gridManager);

		return FindBest(posSquares);
	}
	public Square FindBest(List<Square> posSquares){
		//cycles through the list and finds the best piece

		Square curBestSquare = null;

		foreach(Square s in posSquares){
			if(curBestSquare == null || curBestSquare.GetPiece().GetPieceValue() < s.GetPiece().GetPieceValue() ){
				curBestSquare = s;
			}
		}
		return curBestSquare;
	}


}
