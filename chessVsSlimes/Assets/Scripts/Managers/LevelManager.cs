using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	private const string TAG = "LEVEL MANAGER: ";
	Level[] levels;
	private int curLevelNum;//saves the number of the last level that was fetched

	public void SetUp(){
		Debug.Log(TAG + "setting up.");
		LevelLoader levelLoader = new LevelLoader();
		levels = levelLoader.LoadLevels();
	}
	public Level GetLevel(int levelNum){
		curLevelNum = levelNum;
		return levels[levelNum];
	}
	public Level[] GetLevels(){
		return levels;
	}
	public Level GetLastLevel(){
		//returns the last level that was requested, used for resetLevel
		return levels[curLevelNum];
	}
}
