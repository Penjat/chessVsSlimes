using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level {
	int[,] grid;
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
	}
	public int[,] GetGrid(){
		return grid;
	}
	public int GetSquare(int x,int z){
		return grid[x,z];
	}
}
