using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	private const string TAG = "GRIDMANAGER: ";

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

		for(int x=0;x<gridWidth;x++){
			for(int z=0;z<gridDepth;z++){

				GameObject g = Instantiate(GetPrefab(x +z));
				g.transform.position = new Vector3(x*spacing,0,z*spacing);
			}

		}
	}
	public GameObject GetPrefab(int i){
		//returns a black or white square depending on where we are on the grid
		if(Calc.IsOdd(i)){
			return squarePrefabBlack;
		}
		return squarePrefabWhite;

	}
}
