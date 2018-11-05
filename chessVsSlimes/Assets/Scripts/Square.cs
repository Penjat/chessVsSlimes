using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

	private Piece piece;
	private Enemy enemy;
	private bool hasEffect;


	private int x,z;
	private Animator animator;
	private bool isPosibleMove;
	private bool isAvailable;
	private bool canTake;//there is an enemy here that can be taken

	public void SetPos(int xPos, int zPos){
		x = xPos;
		z = zPos;
		animator = GetComponent<Animator>();
	}
	public void IsLight(bool b){
		//is the square lit up
		animator.SetBool("isLight",b);
	}

	//------Piece----------
	public Piece GetPiece(){
		//returns null if no piece
		return piece;
	}
	public void SetPiece(Piece p){
		piece = p;
	}
	public bool HasPiece(){
		return (piece != null);
	}

	//------Effect----------
	public void SetHasEffect(bool b){
		hasEffect = b;
	}
	public bool GetHasEffect(){
		return hasEffect;
	}

	//------Enemy----------
	public Enemy GetEnemy(){
		//returns null if no piece
		return enemy;
	}
	public void SetEnemy(Enemy e){
		enemy = e;
	}
	public bool HasEnemy(){
		return (enemy != null);
	}



	public void Clear(){
		//clears of is possible and canTake
		animator.Play("notPos");
		canTake = false;
		isPosibleMove = false;

	}
	public void SetCanTake(bool b){
		canTake = b;
		if(b){
			animator.Play("canTake");
			return;
		}
		animator.Play("notPos");
	}
	public void SetPossible(bool b){
		//sets if it is a posible move
		isPosibleMove = b;
		if(b){
			animator.Play("isPos");
			return;
		}
		animator.Play("notPos");

	}

	public bool GetIsPossible(){
		return isPosibleMove;
	}
	public bool GetCanTake(){
		return canTake;
	}

	public void SetAvailable(bool b){
		//sets whether this square is part of the Level
		gameObject.SetActive(b);
		isAvailable = b;
	}
	public bool GetAvailable(){
		return isAvailable;
	}
	public int GetX(){
		return x;
	}
	public int GetZ(){
		return z;
	}
	public List<Square> CheckAroundSelf(GridManager gridManager){
		//finds all the pieces surrounding the square
		Vector2[] pointsAround =
		{new Vector2(1,0),
			new Vector2(1,1),
			new Vector2(0,1),
			new Vector2(-1,0),
			new Vector2(-1,1),
			new Vector2(-1,-1),
			new Vector2(0,-1),
			new Vector2(1,-1),};

			List<Square> piecesAround = new List<Square>();


			foreach(Vector2 v in pointsAround){
				Square s = gridManager.GetSquare(this,(int)v.x,(int)v.y);
				if(s != null && s.HasPiece()){
					//if it has a piece, add it to the list
					piecesAround.Add(s);
				}
			}
			return piecesAround;
	}
	public Square CheckAroundSelfForBest(GridManager gridManager){
		//checks around self for the piece with the highest value

			//finds all the pieces surrounding the square
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
					Square s = gridManager.GetSquare(this,(int)v.x,(int)v.y);
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
