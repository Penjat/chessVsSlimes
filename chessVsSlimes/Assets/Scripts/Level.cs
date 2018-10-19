using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {
	int[,] grid;

	private List<Ob> pieceList;
	private List<Ob> enemyList;

	public Level(){

		grid = new int[8,8];
		for(int x=0;x <8;x++){
			for(int z=0;z <8;z++){
				grid[x,z] = 1;
			}
		}
		grid[3,5] = 0;
		grid[3,6] = 0;
		grid[7,7] = 0;
		grid[2,2] = 0;

		pieceList = new List<Ob>();
		enemyList = new List<Ob>();

		AddEnemy(EnemyManager.BASIC_SLIME,4,6);
		AddEnemy(EnemyManager.BASIC_SLIME,0,3);
		AddEnemy(EnemyManager.BASIC_SLIME,5,0);

		AddPiece(PieceManager.KNIGHT,0,0);
		AddPiece(PieceManager.KNIGHT,3,3);
		AddPiece(PieceManager.KNIGHT,2,4);


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

	public class Ob{
		//a generic object, could be an enemy or a piece or whatever
		//added to lists in level to be converted into Game Pieces on level load
		private int type,xPos,zPos;

		public Ob(int t, int x, int z){
			type = t;
			xPos = x;
			zPos = z;
		}
		public int GetX(){
			return xPos;
		}
		public int GetZ(){
			return zPos;
		}

	}
}
