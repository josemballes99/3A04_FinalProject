using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LobbySpawn : MonoBehaviour {

	GameObject People;
	private List<GameObject> Customers = new List<GameObject>();

	// Use this for initialization
	void Start () {
		init ();
		Invoke ("spawnCustomer", 5);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void init () {
		foreach(Transform child in transform){
			child.gameObject.SetActive (false);
			Customers.Add (child.gameObject);
		}
	}
	public void spawnCustomer () {
		Customers.ElementAt(Random.Range(0,16)).SetActive(true);
		Invoke ("spawnCustomer", 5);
	}
}
