using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {

	private const string TAG = "MAIN MANAGER: ";

	private MenuManager menuManager;
	private GridManager gridManager;

	void Start () {
		Debug.Log(TAG + "starting up.");

		menuManager = GetComponent<MenuManager>();
		menuManager.SetUp(this);

		gridManager = GetComponent<GridManager>();
		gridManager.SetUp();

		ToTitle();

		Debug.Log("5 " + Calc.IsOdd(5));
		Debug.Log("6 " + Calc.IsOdd(6));
		Debug.Log("7 " + Calc.IsOdd(7));
		Debug.Log("12 " + Calc.IsOdd(12));
		Debug.Log("534 " + Calc.IsOdd(534));
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
	}


}
