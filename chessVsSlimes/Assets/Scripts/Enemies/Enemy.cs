using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

	private EnemyManager enemyManager;
	protected int numberOfMoves = 1;//default is one
	protected int movesLeft;

	protected List<ActionEffect> effectList;


	protected List<string> actionList;//the list of all the actions the slime will take
	protected int actionIndex;//the current action being performed

	protected Square square;
	protected bool turnTaken;
	protected bool takingTurn;
	protected float timer;
	protected float rate = 1.0f;
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

				if(actionIndex >= actionList.Count){
					EndTurn();
					return;
				}
				if(!FindMove(enemyManager.GetGridManager(),actionList[actionIndex])){
					//if can't find a move, go to next action
					/*
					actionIndex++;
					if(actionIndex < actionList.Count){
						TakeTurn(enemyManager.GetGridManager());
						return;
					}
					//if out of actions EndTurn
					EndTurn();
					*/
					TakeTurn(enemyManager.GetGridManager());
				}

				//If does find a move
				//TODO perhaps -- movesLeft here
				actionIndex++;

			}
		}

		if(isMoving){
			if(!Moving()){
				isMoving = false;
				Debug.Log("Done moving.");
				movesLeft--;

				//if has moves left, move again
				//TODO put back in moves left
				if(actionIndex < actionList.Count){
					TakeTurn(enemyManager.GetGridManager());
					return;
				}

				//if no actions left, end turn
				EndTurn();


			}
		}
	}
	public virtual bool Moving(){
		//is used when the enemy is moving from square to square
		//and we want to keep track of when the journey is complete

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

		effectList = new List<ActionEffect>();
		actionList = new List<string>();


		//TODO add moves from file
		actionList.Add("shock");
		actionList.Add("hop");
		actionList.Add("hop");

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
	public void StartTurn(GridManager gridManager){
		//is called at the begining of enemies turn
		movesLeft = numberOfMoves;
		actionIndex = 0;
		TakeTurn(gridManager);
	}
	public virtual void TakeTurn(GridManager gridManager){
		//is called for every move the enemy takes


		timer = rate;
		findingMove = true;
		takingTurn = true;
		animator.Play("enemySelected");
		//Move(gridManager);

	}
	public virtual bool FindMove(GridManager gridManager, string action){
		//returns true if can move false if can't
		return false;
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
	public void CheckEffectsDone(){
		//checks if there are still actions pending
		//if none, take turn or end turn
		if(effectList.Count == 0){
			TakeTurn(enemyManager.GetGridManager());
		}

	}
	public void RemoveEffect(ActionEffect effect){
		effectList.Remove(effect);
		CheckEffectsDone();
	}
}
