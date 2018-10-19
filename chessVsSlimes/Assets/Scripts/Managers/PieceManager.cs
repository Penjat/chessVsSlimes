using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour {
	//manages all the players movable pieces

	private const string TAG = "PIECE MANAGER: ";

	private MainManager mainManager;
	private GridManager gridManager;

	//-----------------------------
	public const int KNIGHT = 3;
	//-----------------------------

	private Piece curPiece;

	private List<Piece> pieceList;

	public GameObject piecePrefab;//TODO only have prfabs for the normal chess pieces, make piece abstract

	public void SetUp(MainManager mainM,GridManager gridM){
		Debug.Log(TAG + "setting up.");

		pieceList = new List<Piece>();

		mainManager = mainM;
		gridManager = gridM;
	}

	public void StartLevel(Level level){
		//TODO pass the level valuse in here
		Debug.Log(TAG + "starting level.");

		CreatePieces(level.GetPieces());
	}
	public void CreatePieces(List<Level.Ob> obList){
		foreach(Level.Ob ob in obList){
			AddPiece(ob.GetX(), ob.GetZ());
		}
	}

	public void SelectPiece(Piece piece){
		//set the current piece as selected and deselect the old piece if their is one
		if(curPiece != null){
			curPiece.SetSelected(false);
		}
		curPiece = piece;
		gridManager.ClearPosibleMoves();
		curPiece.SetSelected(gridManager);
	}
	public void AddPiece(int x,int z){
		GameObject g = Instantiate(piecePrefab);
		Piece piece = g.GetComponent<Piece>();
		piece.SetUp(this,gridManager.GetSquare(x,z));
		pieceList.Add(piece);
		//TODO add to piece list
	}
	public void RemovePiece(Piece piece){
		pieceList.Remove(piece);
	}
	public void SquarePressed(Square square){

		//if clicking on square with a piece
		if(square.HasPiece()){
			SelectPiece(square.GetPiece());
			return;
		}

		//if clicking on empty square
		//and there is a piece selected
		if(curPiece != null && square.GetIsPossible()){
			curPiece.MoveTo(square);
			curPiece.SetSelected(false);
			curPiece = null;
			gridManager.ClearPosibleMoves();
			mainManager.EndPlayerTurn();
		}
		if(curPiece != null && square.GetCanTake()){
			//TODO clear enemy,separet function for taking enemy
			square.GetEnemy().Take();
			curPiece.MoveTo(square);
			curPiece.SetSelected(false);
			curPiece = null;
			gridManager.ClearPosibleMoves();
			mainManager.EndPlayerTurn();
		}

	}
}
