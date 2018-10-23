using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private EnemyManager enemyManager;
	protected Square square;
	protected bool turnTaken;
	protected bool takingTurn;
	protected float timer;
	protected float rate = 0.2f;
	protected Animator animator;

	protected bool findingMove;//have some time before starting the move
	protected bool isMoving;
	protected Vector3 startMarker;//starting position of the move
	protected float startTime;
	protected float journeyLength;
	protected float speed = 2.0f;

	public GameObject explode;



	void Update(){

		if(findingMove){
			timer-= Time.deltaTime;
			if(timer <=0 ){
				findingMove = false;
				if(!Move(enemyManager.GetGridManager())){
					//if can't move, end turn
					EndTurn();
				}

			}
		}

		if(isMoving){
			if(!Moving()){
				isMoving = false;
				EndTurn();
			}
		}
	}
	public virtual bool Moving(){
		float distCovered = (Time.time - startTime) * speed;
		float fracJourney = distCovered / journeyLength;
		transform.position = Vector3.Lerp(startMarker, square.transform.position, fracJourney);
		if(journeyLength-distCovered < .1f){

			transform.position = square.transform.position;
			return false;
		}
		return true;
	}

	public void SetUp(EnemyManager enemyM, Square s){
		enemyManager = enemyM;
		animator = GetComponent<Animator>();
		//animator = GetComponent<Animator>();
		SetPos(s);
	}
	public void SetPos(Square s){
		square = s;
		transform.position = square.transform.position;
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
		findingMove = true;
		takingTurn = true;
		timer = rate;
		animator.Play("enemySelected");
		//Move(gridManager);

	}
	public bool Move(GridManager gridManager){
		//returns true if can move false if can't
		Square nextSquare = gridManager.GetSquare(square ,1,0);
		if(nextSquare == null || !nextSquare.GetAvailable()){
			return false;
		}
		//TODO check for other enemies
		if(nextSquare.HasPiece()){
			nextSquare.GetPiece().Take();
		}
		if(nextSquare.HasEnemy()){
			nextSquare.GetEnemy().Take();
		}
		MoveTo(nextSquare);
		return true;
	}
	public virtual void MoveTo(Square newSquare){
		if(square != null ){
			//clear from the old square
			square.SetEnemy(null);
		}
		//SetPos(newSquare);
		square = newSquare;
		//transform.position = square.transform.position;
		startMarker = transform.position;
		startTime = Time.time;
		journeyLength = Vector3.Distance(startMarker, square.transform.position);
		isMoving = true;
		animator.Play("Jump");
		square.SetEnemy(this);
	}
	public void EndTurn(){
		turnTaken = true;
		takingTurn = false;
		transform.position = square.transform.position;//TODO do this better
		enemyManager.FindNextEnemy();
		animator.Play("norm");
	}
}
