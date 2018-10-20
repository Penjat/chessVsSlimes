using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	private const string TAG = "ENEMY MANAGER: ";

	private MainManager mainManager;
	private GridManager gridManager;

	//-----------------------------
	public const int BASIC_SLIME = 0;
	//-----------------------------

	private List<Enemy> enemyList;

	private bool takingTurn = true;
	

	public GameObject enemyPrefab;


	public void SetUp(MainManager mainM,GridManager gridM){
		Debug.Log(TAG + "setting up.");
		mainManager = mainM;
		gridManager = gridM;

	}
	public void StartLevel(Level level){
		Debug.Log(TAG + "starting level.");
		//TODO clear enemy list on starting level
		enemyList = new List<Enemy>();
		AddEnemies(level.GetEnemies());

	}
	public void AddEnemies(List<Level.Ob> obList){
		foreach(Level.Ob ob in obList){
			AddEnemy(ob.GetX(), ob.GetZ());
		}
	}

	public void AddEnemy(int x, int z){

		GameObject g = Instantiate(enemyPrefab);
		Enemy enemy = g.GetComponent<Enemy>();
		enemy.SetUp(this, gridManager.GetSquare(x,z));
		enemyList.Add(enemy);
	}

	public void StartTurn(){
		takingTurn = true;

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
