using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	private const string TAG = "ENEMY MANAGER: ";

	private MainManager mainManager;
	private GridManager gridManager;

	private List<Enemy> enemyList;

	private bool takingTurn = true;
	private float timer;

	public GameObject enemyPrefab;


	public void SetUp(MainManager mainM,GridManager gridM){
		Debug.Log(TAG + "setting up.");
		mainManager = mainM;
		gridManager = gridM;

	}
	public void StartLevel(){
		Debug.Log(TAG + "starting level.");
		//TODO clear enemy list on starting level
		enemyList = new List<Enemy>();
		AddEnemy(0,3);
		AddEnemy(0,2);
		AddEnemy(6,2);
		AddEnemy(6,6);
		AddEnemy(1,2);
	}

	public void AddEnemy(int x, int z){

		GameObject g = Instantiate(enemyPrefab);
		Enemy enemy = g.GetComponent<Enemy>();
		enemy.SetUp(this, gridManager.GetSquare(x,z));
		enemyList.Add(enemy);
	}

	public void StartTurn(){
		takingTurn = true;
		timer = 3.0f;
		//reset all enemies
		foreach(Enemy enemy in enemyList){
			enemy.SetTurnTaken(false);
		}

		FindNextEnemy();
	}
	private void EndTurn(){
		mainManager.EndEnemyTurn();
	}
	public void TakeEnemy(Enemy enemy){
		enemyList.Remove(enemy);
	}
	public void FindNextEnemy(){
		//cycle through the enemies to see if any need to take their turn
		int enemyNum = 0;
		foreach(Enemy enemy in enemyList){
			if(!enemy.GetTurnTaken()){
				//if the enemy hasn't taken its turn, take it
				Debug.Log(TAG + "enemy taking turn " + enemyNum);
				enemy.TakeTurn(gridManager);
				return;
			}
			enemyNum++;
		}
		//if no enemies left to take their turns, end enemy turn
		EndTurn();
	}


}
