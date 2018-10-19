using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	private const string TAG = "ENEMY MANAGER: ";

	private MainManager mainManager;
	private GridManager gridManager;

	public GameObject enemyPrefab;

	public void SetUp(MainManager mainM,GridManager gridM){
		Debug.Log(TAG + "setting up.");
		mainManager = mainM;
		gridManager = gridM;

	}
	public void StartLevel(){
		Debug.Log(TAG + "starting level.");
		AddEnemy(0,3);
		AddEnemy(0,2);
		AddEnemy(6,2);
		AddEnemy(6,6);
	}

	public void AddEnemy(int x, int z){

		GameObject g = Instantiate(enemyPrefab);
		Enemy enemy = g.GetComponent<Enemy>();
		enemy.SetUp(this, gridManager.GetSquare(x,z));
	}


}
