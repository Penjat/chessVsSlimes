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

			int type;
			int x;
			int z;

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

				case "Enemy":
					Debug.Log(TAG + "creating an enemy.");
					string[] enemyData = line[1].Split(',');
					type = int.Parse(enemyData[0]);
					x = int.Parse(enemyData[1]);
					z = int.Parse(enemyData[2]);
					level.AddEnemy(type,x, z);
					break;

				case "Piece":
					Debug.Log(TAG + "creating a piece.");
					string[] pieceData = line[1].Split(',');
					type = int.Parse(pieceData[0]);
					x = int.Parse(pieceData[1]);
					z = int.Parse(pieceData[2]);
					level.AddPiece(type,x, z);
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
