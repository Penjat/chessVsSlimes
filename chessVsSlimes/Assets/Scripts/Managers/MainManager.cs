using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

	private const string TAG = "MAIN MANAGER: ";

	private MenuManager menuManager;
	private GridManager gridManager;
	private PieceManager pieceManager;
	private InputManager inputManager;
	private EnemyManager enemyManager;

	void Start () {
		Debug.Log(TAG + "starting up.");

		menuManager = GetComponent<MenuManager>();
		menuManager.SetUp(this);

		gridManager = GetComponent<GridManager>();
		gridManager.SetUp();

		pieceManager = GetComponent<PieceManager>();
		pieceManager.SetUp(this,gridManager);

		enemyManager = GetComponent<EnemyManager>();
		enemyManager.SetUp(this,gridManager);

		inputManager = GetComponent<InputManager>();
		inputManager.SetUp(this,pieceManager);

		ToTitle();


	}

	public void ToLevelSel(){
		Debug.Log(TAG + "to level select.");
		menuManager.NavigateMenu(MenuManager.LV_SEL);
	}
	public void ToTitle(){
		Debug.Log(TAG + "to title menu.");
		menuManager.NavigateMenu(MenuManager.TITLE);
	}

	public void ToGamePlay(){
		Debug.Log(TAG + "to gameplay.");
		menuManager.NavigateMenu(MenuManager.GAMEPLAY);

		//TODO clear the grid after
		StartLevel();
	}
	public void StartLevel(){
		//TODO pass in a level

		gridManager.CreateGrid();
		pieceManager.StartLevel();
		enemyManager.StartLevel();
		StartPlayerTurn();
	}

	public void StartPlayerTurn(){
		Debug.Log(TAG + "starting player's turn");
	}

	public void EndPlayerTurn(){
		Debug.Log(TAG + "ending player's turn");
		StartEnemyTurn();
	}
	public void StartEnemyTurn(){
		Debug.Log(TAG + "starting enemy's turn");
		EndEnemyTurn();
	}
	public void EndEnemyTurn(){
		Debug.Log(TAG + "ending enemy's turn");
		StartPlayerTurn();
	}


}
