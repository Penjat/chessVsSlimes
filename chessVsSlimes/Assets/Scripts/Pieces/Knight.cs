using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece {

	public override void GetPosMoves (GridManager grid){
		int x1 = square.GetX();
		int y1 = square.GetZ();

		int x2;
		int y2;
		//up left

		x2 = x1+1;
		y2 = y1+2;
		CheckSquare(x2,y2,grid);

		x2 = x1+1;
		y2 = y1-2;
		CheckSquare(x2,y2,grid);

		x2 = x1-1;
		y2 = y1+2;
		CheckSquare(x2,y2,grid);

		x2 = x1-1;
		y2 = y1-2;
		CheckSquare(x2,y2,grid);


		x2 = x1+2;
		y2 = y1+1;
		CheckSquare(x2,y2,grid);

		x2 = x1+2;
		y2 = y1-1;
		CheckSquare(x2,y2,grid);

		x2 = x1-2;
		y2 = y1+1;
		CheckSquare(x2,y2,grid);

		x2 = x1-2;
		y2 = y1-1;
		CheckSquare(x2,y2,grid);

	}
}
