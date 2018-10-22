using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private const string TAG = "LEVEL MANAGER: ";
	Level[] levels;

	public void SetUp(){
		Debug.Log(TAG + "setting up.");
		LevelLoader levelLoader = new LevelLoader();
		levels = levelLoader.LoadLevels();
	}
	public Level GetLevel(int levelNum){

		return levels[levelNum];
	}
	public Level[] GetLevels(){
		return levels;
	}
}
