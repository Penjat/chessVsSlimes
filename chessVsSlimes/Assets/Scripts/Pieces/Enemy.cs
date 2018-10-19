using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private EnemyManager enemyManager;
	protected Square square;

	public void SetUp(EnemyManager enemyM, Square s){
		enemyManager = enemyM;
		//animator = GetComponent<Animator>();
		SetPos(s);
	}
	public void SetPos(Square s){
		square = s;
		transform.position = square.transform.position;//TODO position more accuratly
		square.SetEnemy(this);
	}
	public void Take(){
		square.SetEnemy(null);
		Destroy(gameObject);
	}
}
