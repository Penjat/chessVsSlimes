using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {
	private string name;

	int[,] grid;


	private List<Ob> pieceList;
	private List<Ob> enemyList;

	public Level(){

		pieceList = new List<Ob>();
		enemyList = new List<Ob>();


	}
	public void SetName(string s){
		name = s;
	}
	public void CreateGrid(int width,int depth,string[] gridData){
		//TODO pass in the string[] for all the grid data


		grid = new int[width,depth];
		for(int x=0;x <width;x++){

			string[] gridLine = gridData[x].Split(',');
			for(int z=0;z <depth;z++){
				grid[x,z] = int.Parse(gridLine[z]);
			}
		}
	}
	public int[,] GetGrid(){
		return grid;
	}
	public int GetSquare(int x,int z){
		return grid[x,z];
	}
	public void AddEnemy(int type,int x, int z){
		Ob ob = new Ob(type,x,z);
		enemyList.Add(ob);
	}
	public void AddEnemy(int type,int x, int z,string data){
		Ob ob = new Ob(type,x,z);
		ob.SetExtraData(data);
		enemyList.Add(ob);
	}
	public void AddPiece(int type,int x, int z){
		Ob ob = new Ob(type,x,z);
		pieceList.Add(ob);
	}
	public List<Ob> GetEnemies(){
		return enemyList;
	}
	public List<Ob> GetPieces(){
		return pieceList;
	}
	//---------------------------OB----------------------------------------
	public class Ob{
		//a generic object, could be an enemy or a piece or whatever
		//added to lists in level to be converted into Game Pieces on level load
		private int type,xPos,zPos;
		private string extraData = "";//string to be converted into a string array of enemies' traits and actions

		public Ob(int t, int x, int z){
			type = t;
			xPos = x;
			zPos = z;
		}
		public void SetExtraData(string s){
			extraData = s;
		}
		public int GetX(){
			return xPos;
		}
		public int GetZ(){
			return zPos;
		}
		public int GetType(){
			return type;
		}
		public string GetExtraData(){
			return extraData;
		}

	}
}
