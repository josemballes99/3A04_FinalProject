using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;

using Mono.Data.Sqlite;
using System.Data;
using System.Linq;

namespace context {

public class ObjMgr : MonoBehaviour {

	public GameObject window;
	public Text currObj;
	public Text currProgress;
	
	public TextAsset data;
	public int current = 0;
	
	//public static Dictionary<int, Objective> objects = new Dictionary<int, Objective> ();
	public static List<Objective> objects = new List<Objective> ();


	public Text protoItem;
	public GameObject listView;	
	public static List<Text> boxes = new List<Text>();
	
	private static Stopwatch timer = new Stopwatch();
	

	void Awake(){
			timer.Reset ();
			timer.Start ();
			ObjMgr.objects.Clear ();
			Queries.createLog ();
//			/IDbConnection connection = Queries.connect(Queries.dbURL);
//			IDbCommand cmd = connection.CreateCommand();
//			string sqlQuery = "SELECT name FROM sqlite_master WHERE type='table'";
			//string sqlQuery = "SELECT name FROM sqlite_master WHERE type='table'";
			//cmd.CommandText = sqlQuery;

			//IDataReader reader = cmd.ExecuteReader();
			//List<Objective> objects = new List<Objective> ();

//			while (reader.Read ()) {
//				string s = reader.GetString (0);
//				UnityEngine.Debug.Log(s);
//			}
				
			var objectives = Queries.loadObjectives ();

			// Loads the objectives into the scrollview
			if (objectives != null) {
				int i = 0;
				bool first = true;
				foreach (Objective obj in objectives){
					objects.Add (obj);
					if (i == 0) {
						i++;
						continue;
					}

					Text box;
					if (first == true) {
						box = protoItem;
						first = false;
					} else {
						box = Instantiate (protoItem);
					}

					box.transform.SetParent (listView.transform);
					box.text = obj.caption + "\t\t\t\t" + obj.progress + "%";
					boxes.Add (box);
				}
			}
	}

	// Use this for initialization
	void Start () {
		window.SetActive (true);
		currProgress.text = objects.ElementAt (0).progress + "%";
		currObj.text = objects.ElementAt (0).caption;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer.ElapsedMilliseconds < 10000)
			return;
			
			for (int i = 1; i < objects.Count (); i++) {
				Objective obj = objects.ElementAt (i);
				string stmt = objects.ElementAt (i).query;
				int result = Queries.execute (stmt);
				int progress = obj.progress;
				int status;
				if (result > 0)
					status = progress / result;
				else
					status = 0;

				if (status >= 1) {
					objects.ElementAt (i).progress = 100;
					boxes.ElementAt (i-1).text = obj.caption + "\t\t\t\t100%";
				} else {
					objects.ElementAt (i).progress = status * 100;
					boxes.ElementAt (i-1).text = obj.caption + "\t\t\t\t" + (status * 100).ToString() + "%";
				}
			}
			timer.Reset();
			timer.Start();
	}

//		public void Save(){
//			BinaryFormatter bf = new BinaryFormatter();
//			FileStream file = File.Open(Application.persistentDataPath + "/object.log", FileMode.Create);
//			Objective newData = new Objective(0,"",0,0,"",0);
//			//newData.caption;
//			//newData.progress;
//			//newData.reward;
//			//newData.id;
//
//			bf.Serialize(file, newData);
//			file.Close();
//		}
//
//		public void Load(){
//
//			if (File.Exists(Application.persistentDataPath + "/object.log")){
//				BinaryFormatter bf = new BinaryFormatter();
//				FileStream file = File.Open(Application.persistentDataPath + "/object.log", FileMode.Open);
//				Objective newData = (Objective)bf.Deserialize(file);
//				file.Close();
//				//caption;
//				//progress;
//				//reward;
//				//id;
//			}
//		}
//
//
//		void loadDB () {
//			string dbURL = "URI=file:Objectives.db"; //Path to database.
//
//			IDbConnection connection;
//			connection = (IDbConnection) new SqliteConnection(dbURL);
//			connection.Open(); //Open connection to the database.
//
//			IDbCommand cmd = connection.CreateCommand();
//			string sqlQuery = "SELECT * FROM Objectives";
//			//string sqlQuery = "SELECT name FROM sqlite_master WHERE type='table'";
//			cmd.CommandText = sqlQuery;
//
//			IDataReader reader = cmd.ExecuteReader();
//			while (reader.Read())
//			{
//				int id = reader.GetInt32(0);
//				string caption = reader.GetString (1);
//				int progress = reader.GetInt32(2);
//				int reward = reader.GetInt32(3);
//				string query = reader.GetString (4);
//				int condition = reader.GetInt32 (5);
//
//				Objective obj = new Objective (id, caption, progress, reward, query, condition);
//				objects.Add (obj);
//				//Debug.Log( "table= "+name);//+"  name ="+name+"  random ="+  rand);
//			}
//			reader.Close();
//			reader = null;
//			cmd.Dispose();
//			cmd = null;
//			connection.Close();
//			connection = null;
//		}
	}
}

[Serializable]
public class Objective
{

	public string caption;
	public int reward;
	public int progress;
	public string query;
	public int id;
	public int condition;

	public Objective(int id, string caption, int progress, int reward, string query, int condition){
		this.id = id;
		this.caption = caption;
		this.progress = progress;
		this.query = query;
		this.reward = reward;
		this.condition = condition;

	}

}