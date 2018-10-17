using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour {
	//manages all the players movable pieces

	private const string TAG = "PIECE MANAGER: ";

	private MainManager mainManager;
	private GridManager gridManager;

	public GameObject piecePrefab;//TODO only have prfabs for the normal chess pieces, make piece abstract

	public void SetUp(MainManager mainM,GridManager gridM){
		Debug.Log(TAG + "setting up.");
		mainManager = mainM;
		gridManager = gridM;
	}

	public void StartLevel(){
		//TODO pass the level valuse in here
		Debug.Log(TAG + "starting level.");

		GameObject g = Instantiate(piecePrefab);
		Piece piece = g.GetComponent<Piece>();
		piece.SetPos(gridManager.GetSquare(0,4));
	}
}
