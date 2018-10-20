using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

	private const string TAG = "LEVEL LOADER: ";

	private Level curLevel;

	public void SetUp(){
		Debug.Log(TAG + "setting up.");

		TextAsset textAsset = Resources.Load<TextAsset>("Levels/level1");
		string[] levelData = textAsset.ToString().Split('\n');


		curLevel = ReadLevelData(levelData);

	}
	public Level ReadLevelData(string[] levelData){
		//loads the data from the string[] and puts it inside the level

		Level level = new Level();


		int lineNum = 0;
		int linesIgnored = 0;//keeps track if any lines are read but ignored
		Debug.Log(TAG + "reading level data. Length = " + levelData.Length);

		while(lineNum < levelData.Length){
			Debug.Log(TAG + "reading line " + lineNum );
			string[] line = levelData[lineNum].Split(':');
			string dataTag = line[0];//dataTag informs what king of data is in the line
			lineNum++;

			switch(dataTag){

				case "Name":
					Debug.Log(TAG + "setting level name.");
					level.SetName(line[1]);
					break;


				case "Grid":
					Debug.Log(TAG + "creating the grid.");
					string[] gridSize = line[1].Split(',');
					int width = int.Parse(gridSize[0]);
					int depth = int.Parse(gridSize[1]);
					string[] gridData = new string[width];//TODO decide if should be depth or width
					Array.Copy(levelData,lineNum,gridData,0,width);
					level.CreateGrid(width,depth,gridData);
					lineNum += width;
					break;



				default:
					Debug.Log(TAG + "ignoring line.");
					linesIgnored++;
					break;
			}
		}
		Debug.Log(TAG + "levelData loaded. " + linesIgnored + "of " + levelData.Length + " lines ignored.");
		return level;
	}

	public Level GetLevel(){


		return curLevel;
	}
}
