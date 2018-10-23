using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour{

	private int whosTurn;

	public const int PLAYER_TURN = 0;
	public const int ENEMY_TURN = 1;
	public const int PLAYER_WIN = 2;
	public const int PLAYER_LOSE = 3;

	public Text turnText;
	private string[] turnTextOptions = {"PLayer's Turn","Slime Turn","You Win","You Lost"};

	public void SetUp(MainManager mainM){

	}
	public void SetTurn(int i){
		whosTurn = i;

		turnText.text = turnTextOptions[whosTurn];


	}
	public bool CheckTurn(int i){
		return (i == whosTurn);
	}


}
