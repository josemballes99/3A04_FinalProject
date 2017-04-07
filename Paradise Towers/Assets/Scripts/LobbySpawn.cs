using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using Mono.Data.Sqlite;
using System.Data;

public class LobbySpawn : MonoBehaviour {

	GameObject People;
    public GameObject floorObjects;

	public static Dictionary<int, int> cList = new Dictionary<int, int> ();
	public static List<GameObject> clients = new List<GameObject>();

	void Start () {
		init ();
		Invoke ("spawnCustomer", 5);
	}
	
	// Initialize customers, putting them in lobby
	public void init () {
		cList.Clear ();
		clients.Clear ();
		int i = 0;
		foreach(Transform child in transform){
			child.gameObject.SetActive (false);
			if (clients.Count < 16) {
				cList.Add (i, 0);
				clients.Add (child.gameObject);
			}
			i++;
		}
	}

	public void spawnCustomer() {
        if (floorObjects.transform.Find("lobby").gameObject.activeSelf)
        {
		    clients.ElementAt(Random.Range(0,16)).SetActive(true);
			IDbConnection connection = Queries.connect (Queries.dbURL);
			IDbCommand command = connection.CreateCommand();
			command.CommandText = "UPDATE Occupants set num=num+1 WHERE pos=0 AND num < 16";
			command.ExecuteNonQuery();
			command.Dispose();
			command = null;
			connection.Close();
		    Invoke ("spawnCustomer", 5);
        }
	}
}
