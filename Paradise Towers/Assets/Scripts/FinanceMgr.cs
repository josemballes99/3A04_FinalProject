using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

using Mono.Data.Sqlite;
using System.Data;

namespace context {

	public class FinanceMgr : MonoBehaviour
	{
		public GameObject window;
		public Text revText, expText, incText;

		private readonly TimeSpan origin = DateTime.Now.TimeOfDay;

		public static int tid = 0;
		public static int Floor = 0;
		public static int Customer = 1;

		static int[] revenueSource = new int[2]{0,0};
		static int[] expenseSource = new int[2]{0,0};
		static int revenuePerMinute = 0;
		static int expensePerMinute = 0;

		public static int incomePerMinute (){
			return (revenuePerMinute - expensePerMinute);
		}

		public static int revenues(){
			return revenuePerMinute; // * time elapsed
		}

		public static int expenses(){
			return expensePerMinute; // * time elapsed
		}

		void Awake (){
			//window.SetActive (false);
			Debug.Log("fstart");
		}

		// Use this for initialization
		void Start ()
		{
			revText.text = revenues().ToString();
			expText.text = expenses().ToString();
			incText.text = (revenues() - expenses ()).ToString();
		}
			

		//Add data structure to track financial events so we can put them in the scrollview

		public static void addRevenueSource(int source, int amt){
			revenuePerMinute += amt;
			revenueSource [source]++;
		}

		public static void addExpenseSource(int source, int amt){
			expensePerMinute += amt;
			expenseSource [source]++;

			//INSERT INTO COST (tid, source, amt) VALUES (tid, source, amt);
		}

		// Update is called once per frame
		void Update ()
		{
			revenuePerMinute+=2;
			expensePerMinute++;
			revText.text = revenues().ToString();
			expText.text = expenses().ToString();
			incText.text = (revenues() - expenses ()).ToString();
			if (revenues () == 200) {
				window.SetActive (false);
			}
		}


//		void load () {
//			string dbURL = "URI=file:Objectives.db"; //Path to database.
//
//			IDbConnection connection;
//			connection = (IDbConnection) new SqliteConnection(dbURL);
//			connection.Open(); //Open connection to the database.
//
//			IDbCommand cmd = connection.CreateCommand();
//			//string sqlQuery = "SELECT value,name, randomSequence " + "FROM PlaceSequence";
//			string sqlQuery = "SELECT * FROM Objectives";
//			//string sqlQuery = "SELECT name FROM sqlite_master WHERE type='table'";
//			cmd.CommandText = sqlQuery;
//
//			IDataReader reader = cmd.ExecuteReader();
//			while (reader.Read())
//			{
//				int value = reader.GetInt32(0);
//				//string name = reader.GetString(0);
//				//int rand = reader.GetInt32(2);
//				Debug.Log( "table= "+name);//+"  name ="+name+"  random ="+  rand);
//				revText.text = value;
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