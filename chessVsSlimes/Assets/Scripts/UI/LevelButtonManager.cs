using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonManager : MonoBehaviour {

	private const string TAG = "LEVEL BUTTON MANAGER: ";

	private MainManager mainManager;

	public RectTransform levelButtonCon;
	public GameObject levelButtonPrefab;

	private LevelButton[] levelButtons;

	public void SetUp(MainManager mainM,LevelManager levelManager){
		Debug.Log(TAG + "setting up.");
		mainManager = mainM;
		CreateLevelButtons(levelManager.GetLevels());

	}
	public void CreateLevelButtons(Level[] levels){
		//TODO maybe just pass a string array and load the text files when needed

		levelButtons = new LevelButton[levels.Length];

		for(int i=0;i<levels.Length;i++){
			GameObject g = Instantiate(levelButtonPrefab);
			
			LevelButton levelButton = g.GetComponent<LevelButton>();
			levelButton.SetUp(this,levelButtonCon,i);
			levelButtons[i] = levelButton;
		}

	}
	public void ButtonPressed(LevelButton levelButton){
		Debug.Log(TAG + "level button pressed.");
		mainManager.ToGamePlay(levelButton.GetRef());

	}
}
