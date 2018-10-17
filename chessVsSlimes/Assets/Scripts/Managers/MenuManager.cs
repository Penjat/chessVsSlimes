using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
	private static string TAG = "MENU_MANAGER: ";

	private MainManager mainManager;
	public GameObject[] menus;

	public static int TITLE = 1;
	public static int LV_SEL = 2;
	public static int GAMEPLAY = 4;



	public void SetUp(MainManager mainM){
		Debug.Log(TAG + "setting up.");
		mainManager = mainM;

	}
	public void NavigateMenu(int m){
		byte mask = 1;
		for(int i=0;i<menus.Length;i++){
			if( (mask & m) !=0 ){
				menus[i].SetActive(true);
			}else{
				menus[i].SetActive(false);
			}
			mask *=2;
		}

	}

}