using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

	private LevelButtonManager levelButtonManager;

	private int levelRef;

	public void SetUp(LevelButtonManager levelBeuttonM, RectTransform con, int pos){
		levelButtonManager = levelBeuttonM;
		levelRef = pos;
		transform.SetParent(con);

		float buttonWidth = 60.0f;
		float buttonHeight = 60.0f;
		float padding = 10.0f;

		RectTransform rect = GetComponent<RectTransform>();
		float x1,x2,y1,y2;
		x1 = pos*(buttonWidth+padding)+padding;
		x2 = x1+buttonWidth;
		y1 = -(padding+buttonHeight);
		y2 = y1+buttonHeight;

		rect.offsetMin = new Vector2(x1,y1);
		rect.offsetMax = new Vector2(x2,y2);
	}
	public void Press(){
		levelButtonManager.ButtonPressed(this);
	}
	public int GetRef(){
		//returns the level refernce number
		return levelRef;
	}
}
