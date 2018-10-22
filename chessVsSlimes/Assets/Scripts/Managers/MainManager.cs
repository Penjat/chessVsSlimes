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
	private LevelManager levelManager;


	private TurnManager turnManager;

	void Start () {
		Debug.Log(TAG + "starting up.");

		menuManager = GetComponent<MenuManager>();
		menuManager.SetUp(this);

		levelManager = GetComponent<LevelManager>();
		levelManager.SetUp();
		menuManager.SetUpLevelButtons(levelManager);

		gridManager = GetComponent<GridManager>();
		gridManager.SetUp();

		pieceManager = GetComponent<PieceManager>();
		pieceManager.SetUp(this,gridManager);

		enemyManager = GetComponent<EnemyManager>();
		enemyManager.SetUp(this,gridManager);

		inputManager = GetComponent<InputManager>();
		inputManager.SetUp(this,pieceManager);

		turnManager = GetComponent<TurnManager>();
		turnManager.SetUp(this);

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

	public void ToGamePlay(int levelNum){
		Debug.Log(TAG + "to gameplay.");
		menuManager.NavigateMenu(MenuManager.GAMEPLAY);


		Level level = levelManager.GetLevel(levelNum);
		//TODO clear the grid after
		StartLevel(level);
	}
	public void StartLevel(Level level){
		//TODO pass in a level

		gridManager.CreateGrid(level);
		pieceManager.StartLevel(level);
		enemyManager.StartLevel(level);
		StartPlayerTurn();
	}

	public void StartPlayerTurn(){
		Debug.Log(TAG + "starting player's turn");
		turnManager.SetTurn(TurnManager.PLAYER_TURN);
	}

	public void EndPlayerTurn(){
		Debug.Log(TAG + "ending player's turn");

		StartEnemyTurn();
	}
	public void StartEnemyTurn(){
		Debug.Log(TAG + "starting enemy's turn");
		enemyManager.StartTurn();
		turnManager.SetTurn(TurnManager.ENEMY_TURN);
	}
	public void EndEnemyTurn(){
		Debug.Log(TAG + "ending enemy's turn");
		StartPlayerTurn();
	}

	public void PlayerLose(){
		Debug.Log(TAG + "Player Lost.");
		turnManager.SetTurn(TurnManager.PLAYER_LOSE);
	}
	public void PlayerWin(){
		Debug.Log(TAG + "Player Win.");
		turnManager.SetTurn(TurnManager.PLAYER_WIN);
	}

	public TurnManager GetTurnManager(){
		return turnManager;
	}


}
