using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	private const string TAG = "GRIDMANAGER: ";

	private Square[,] grid;

	public GameObject squarePrefabBlack;
	public GameObject squarePrefabWhite;

	public void SetUp(){
		Debug.Log("setting up.");
	}

	public void CreateGrid(){
		Debug.Log("creating grid.");
		int gridWidth = 8;
		int gridDepth = 8;
		float spacing = 1.1f;
		grid = new Square[gridWidth,gridDepth];

		for(int x=0;x<gridWidth;x++){
			for(int z=0;z<gridDepth;z++){

				GameObject g = Instantiate(GetPrefab(x +z));
				g.transform.position = new Vector3(x*spacing,0,z*spacing);
				Square square = g.GetComponent<Square>();
				square.SetPos(x,z);
				grid[x,z] = square;
			}

		}

	}
	private GameObject GetPrefab(int i){
		//returns a black or white square depending on where we are on the grid
		if(Calc.IsOdd(i)){
			return squarePrefabBlack;
		}
		return squarePrefabWhite;

	}
	public Square GetSquare(int x, int z){
		//check if goes out of bounds of the grid
		if(x >= grid.GetLength(0) || z >= grid.GetLength(1) || x <0 || z < 0){
			return null;
		}
		return grid[x,z];
	}
	public void ClearPosibleMoves(){
		//clears any square marked as posible
		foreach(Square square in grid){
			square.Clear();
		}
	}
}
