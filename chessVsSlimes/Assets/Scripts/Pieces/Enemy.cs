using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private EnemyManager enemyManager;
	protected Square square;
	protected bool turnTaken;
	protected bool takingTurn;
	protected float timer;
	protected float rate = 0.7f;
	protected Animator animator;

	public GameObject explode;



	void Update(){
		if(takingTurn){
			timer-= Time.deltaTime;
			if(timer <=0 ){

				EndTurn();
			}
		}
	}
	public void SetUp(EnemyManager enemyM, Square s){
		enemyManager = enemyM;
		animator = GetComponent<Animator>();
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
		enemyManager.TakeEnemy(this);
		//Make explosion
		explode.transform.SetParent(null);
		explode.SetActive(true);
		Destroy(explode, 5.0f);

		Destroy(gameObject);
	}
	public void SetTurnTaken(bool b){
		turnTaken = b;
	}
	public bool GetTurnTaken(){
		return turnTaken;
	}
	public virtual void TakeTurn(GridManager gridManager){
		//TODO take the enemy turn
		takingTurn = true;
		timer = rate;
		animator.Play("enemySelected");
		Move(gridManager);

	}
	public void Move(GridManager gridManager){
		Square nextSquare = gridManager.GetSquare(square ,1,0);
		if(nextSquare == null || !nextSquare.GetAvailable()){
			return;
		}
		//TODO check for other enemies
		if(nextSquare.HasPiece()){
			nextSquare.GetPiece().Take();
		}
		if(nextSquare.HasEnemy()){
			nextSquare.GetEnemy().Take();
		}
		MoveTo(nextSquare);
	}
	public virtual void MoveTo(Square newSquare){
		if(square != null ){
			//clear from the old square
			square.SetEnemy(null);
		}
		SetPos(newSquare);
	}
	public void EndTurn(){
		turnTaken = true;
		takingTurn = false;
		enemyManager.FindNextEnemy();
		animator.Play("norm");
	}
}
