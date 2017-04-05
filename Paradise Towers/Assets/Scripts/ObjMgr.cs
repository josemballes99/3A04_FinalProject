using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using Mono.Data.Sqlite;
using System.Data;

namespace context {

public class ObjMgr : MonoBehaviour {

	public GameObject window;
	public Text currProgress;
	
	public TextAsset data;
	public int current = 0;
	
	public static Dictionary<int, Objective> objects = new Dictionary<int, Objective> ();

	//New stuff for scrollRect

	public Text protoItem;
	public GameObject listView;

	//End scrollRect stuff here
	
	void Awake(){
			objects.Clear ();
			loadDB ();
//			Debug.Log("Oawake");
//			data = Resources.Load("objectives.csv") as TextAsset;
//			if (data == null){
//				return;
//			}
//			string[] objList = data.text.Split (new string[] { Environment.NewLine }, StringSplitOptions.None);
//
//			foreach (string obj in objList) {
//				string[] fields = obj.Split (',');
//				int id, progress, reward;
//
//				int.TryParse (fields [0], out id);
//				string caption = fields [1];
//				int.TryParse (fields [2], out progress);
//				int.TryParse (fields [3], out reward);
//			}

//			var Lines = File.ReadLines("test.csv").Select(a => a.Split(';'));
//			var CSV = from line in Lines 
//				select (line.Split(',')).ToArray();
	}

	// Use this for initialization
	void Start () {
		Debug.Log("Ostart");
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

		public void Save(){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/object.log", FileMode.Create);
			Objective newData = new Objective(0,"",0,0,"",0);
			//newData.caption;
			//newData.progress;
			//newData.reward;
			//newData.id;

			bf.Serialize(file, newData);
			file.Close();
		}

		public void Load(){

			if (File.Exists(Application.persistentDataPath + "/object.log")){
				BinaryFormatter bf = new BinaryFormatter();
				FileStream file = File.Open(Application.persistentDataPath + "/object.log", FileMode.Open);
				Objective newData = (Objective)bf.Deserialize(file);
				file.Close();
				//caption;
				//progress;
				//reward;
				//id;
			}
		}


		void loadDB () {
			string dbURL = "URI=file:Objectives.db"; //Path to database.

			IDbConnection connection;
			connection = (IDbConnection) new SqliteConnection(dbURL);
			connection.Open(); //Open connection to the database.

			IDbCommand cmd = connection.CreateCommand();
			string sqlQuery = "SELECT * FROM Objectives";
			//string sqlQuery = "SELECT name FROM sqlite_master WHERE type='table'";
			cmd.CommandText = sqlQuery;

			IDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				int id = reader.GetInt32(0);
				string caption = reader.GetString (1);
				int progress = reader.GetInt32(2);
				int reward = reader.GetInt32(3);
				string query = reader.GetString (4);
				int condition = reader.GetInt32 (5);

				Objective obj = new Objective (id, caption, progress, reward, query, condition);
				objects.Add (id, obj);
				//Debug.Log( "table= "+name);//+"  name ="+name+"  random ="+  rand);
			}
			reader.Close();
			reader = null;
			cmd.Dispose();
			cmd = null;
			connection.Close();
			connection = null;
		}
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