using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager {

	private int whosTurn;

	public const int PLAYER_TURN = 0;
	public const int ENEMY_TURN = 1;

	public void SetTurn(int i){
		whosTurn = i;
	}
	public bool CheckTurn(int i){
		return (i == whosTurn);
	}
}
