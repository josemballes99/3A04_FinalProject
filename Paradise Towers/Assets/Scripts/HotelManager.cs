using context;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Mono.Data.Sqlite;
using System.Data;

public class HotelManager : MonoBehaviour {

    private FloorManager floorManager;
	private static bool first = true;

	void Awake(){
		
		if (first == true) {
			first = false;
			Queries.createLog();

			IDbConnection connection = Queries.connect (Queries.dbURL);
			IDbCommand command = connection.CreateCommand();
			command.CommandText = "INSERT INTO Revenue (amt) VALUES (15)";
			command.ExecuteNonQuery();
			command.Dispose();
			command = null;
			connection.Close();
			FinanceMgr.timer.Start ();
			ObjMgr.timer.Start ();
		}

//		IDbConnection connection2 = Queries.connect (Queries.dbURL);
//		IDbCommand command2 = connection2.CreateCommand();
//		command2.CommandText = "SELECT * FROM Occupants";
//		IDataReader reader = command2.ExecuteReader();
//		while (reader.Read ()) {
//			if (!reader.IsDBNull(0)) {
//				int pos = reader.GetInt32 (0);
//				int num = reader.GetInt32 (1);
//			}
//		}
//		command2.Dispose();
//		command2 = null;
//		connection2.Close();
	}

    // Use this for initialization
    void Start () {
        floorManager = GameObject.Find("FloorManager").GetComponent<FloorManager>();

    }

    /** MAIN MENU FUNCTIONS **/
    public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void setLobbyVisible()
    {
        foreach(FloorType floor in floorManager.getFloors())
        {
            if (floor.getType() == 3)            //Lobby
                floor.setVisibility(true);
            else
                floor.setVisibility(false);
        }
    }
}
