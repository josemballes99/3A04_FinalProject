using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAI : MonoBehaviour {

	Animator anim;

	public float speed;
	public bool isMoving;

	public float walkTime;
	private float walkCount;

	public float waitTime;
	private float waitCount;

	private Rigidbody2D myRigidBody;
	private int direction;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent <Animator> ();
		waitCount = waitTime;
		walkCount = walkTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (isMoving){
			walkCount -= Time.deltaTime; 
			if (walkCount < 0){
				isMoving = false;
				waitCount = waitTime;
			}

			switch(direction){
			case 0:
				myRigidBody.velocity = new Vector2 (0, -speed);
				anim.SetFloat("VSpeed",myRigidBody.velocity.y);
				break;
			case 1:
				myRigidBody.velocity = new Vector2 ( -speed, 0);
				anim.SetFloat("HSpeed",myRigidBody.velocity.x);
				break;
			case 2:
				myRigidBody.velocity = new Vector2 (0, speed);
				anim.SetFloat("VSpeed",myRigidBody.velocity.y);
				break;
			case 3:
				myRigidBody.velocity = new Vector2 ( speed, 0);
				anim.SetFloat("HSpeed",myRigidBody.velocity.x);
				break;
			}

		} else {
			anim.SetFloat("VSpeed",0);
			anim.SetFloat("HSpeed",0);
			myRigidBody.velocity = Vector2.zero;
			waitCount -= Time.deltaTime;

			if (waitCount < 0) {
				chooseDirection();
			}
		}
		
	}

	public void chooseDirection(){
		direction = Random.Range (0, 4);
		isMoving = true;
		walkCount = walkTime;
	}
}
