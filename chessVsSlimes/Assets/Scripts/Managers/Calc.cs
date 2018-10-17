using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calc : MonoBehaviour {
	//static calculating methods


	public static bool IsOdd(int i){
		//returns true if i is odd
		return ((i & 1)==1);
	}


}
