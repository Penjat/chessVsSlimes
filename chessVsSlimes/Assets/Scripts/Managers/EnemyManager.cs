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
		ClearAllEnemies();
		enemyList = new List<Enemy>();
		AddEnemies(level.GetEnemies());

	}
	public void AddEnemies(List<Level.Ob> obList){

		foreach(Level.Ob ob in obList){
			AddEnemy(ob.GetX(), ob.GetZ(),ob.GetDir(),ob.GetExtraData());
		}
	}
	public Enemy AddExtraData(Enemy enemy, string data){
		Debug.Log(TAG + "finding the extra data: " + data);
		//return if no extraData
		if(data == ""){
			return enemy;
		}
		string[] extraData = data.Split(',');
		//cycle through the traits and data and add them to the enemy
		foreach(string s in extraData){
				enemy = FindAction(enemy,s);
		}
		return enemy;
	}
	public Enemy FindAction(Enemy enemy,string action){
		//finds the action or trait from the string provided

		switch(action){

			//for the actions
			case "hop":
				enemy.AddAction(action);
				break;

			case "shock":
				enemy.AddAction(action);
				break;

			case "hungry":
				enemy.AddAction(action);
				break;

			//for the Traits
			case "explosive":
				enemy.AddTrait(Enemy.EXPLOSIVE);
				break;

			default:
				Debug.Log(TAG + "Warning. Did not understand the data");
				break;
		}
		return enemy;

	}
	public void ClearAllEnemies(){
		//clear the grid and all gameobjects
		if(enemyList == null){
			return;
		}
		foreach(Enemy enemy in enemyList){
			Destroy(enemy.gameObject);
		}
		enemyList.Clear();
	}

	public void AddEnemy(int x, int z,int dir,string extra){

		GameObject g = Instantiate(enemyPrefab);
		Enemy enemy = g.GetComponent<Enemy>();
		enemy.SetUp(this, gridManager.GetSquare(x,z),dir);
		enemy = AddExtraData(enemy,extra);
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
		CheckPlayerWin();
	}
	public void FindNextEnemy(){
		//cycle through the enemies to see if any need to take their turn
		int enemyNum = 0;
		foreach(Enemy enemy in enemyList){
			if(!enemy.GetTurnTaken()){
				//if the enemy hasn't taken its turn, take it
				Debug.Log(TAG + "enemy taking turn " + enemyNum);
				enemy.StartTurn(gridManager);
				return;
			}
			enemyNum++;
		}
		//if no enemies left to take their turns, end enemy turn
		EndTurn();
	}
	public void CheckPlayerWin(){
		if(enemyList.Count == 0){

			mainManager.PlayerWin();
		}
	}
	public GridManager GetGridManager(){
		return gridManager;
	}


}
