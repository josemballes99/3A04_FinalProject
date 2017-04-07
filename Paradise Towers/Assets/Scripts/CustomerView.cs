using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerView : MonoBehaviour {

	//For Animation
	Animator anim;

	RuntimePlatform platform = Application.platform;

	// Customer For Selections
	public bool isSelected;
	public GameObject customerCanvas;



	// For Customer Movement
	public float speed;
	public bool isMoving;

	public float walkTime;
	private float walkCount;

	public float waitTime;
	private float waitCount;

	private Rigidbody2D myRigidBody;
	private int direction;

	//Pauses Game
	public void Stop(){
		Time.timeScale = 0.0000001f;
	}

	//Resumes Game
	public void Resume(){
		Time.timeScale = 1f;
	}

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent <Animator> ();
		waitCount = waitTime;
		walkCount = walkTime;
		isSelected = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Checks User Input
		if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer){
			if(Input.touchCount > 0) {
				if(Input.GetTouch(0).phase == TouchPhase.Began){
					checkTouch(Input.GetTouch(0).position);
				}
			}
		}else if(platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.WindowsEditor){
			if(Input.GetMouseButtonDown(0)) {
				Debug.Log ("Touch");
				checkTouch(Input.mousePosition);
			}
		}

		//For Character movement and Animation
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

		//For Character Slection
		if (isSelected) {
			customerCanvas.SetActive (true);
			Stop ();
		} else {
			customerCanvas.SetActive (false);
			Resume ();
		}
		
	}

	public void chooseDirection(){
		direction = Random.Range (0, 4);
		isMoving = true;
		walkCount = walkTime;
	}

	public void checkTouch(Vector2 pos){
		Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
		Vector2 touchpos = new Vector2(wp.x,wp.y);
		Collider2D hit = Physics2D.OverlapPoint(touchpos);

		if(hit){
			Debug.Log("HIT");
			isSelected = !isSelected;
		}
	}
}
