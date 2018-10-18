using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

	private const string TAG = "MAIN MANAGER: ";

	private MenuManager menuManager;
	private GridManager gridManager;
	private PieceManager pieceManager;
	private InputManager inputManager;

	void Start () {
		Debug.Log(TAG + "starting up.");

		menuManager = GetComponent<MenuManager>();
		menuManager.SetUp(this);

		gridManager = GetComponent<GridManager>();
		gridManager.SetUp();

		pieceManager = GetComponent<PieceManager>();
		pieceManager.SetUp(this,gridManager);

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
		gridManager.CreateGrid();
		pieceManager.StartLevel();
	}


}
