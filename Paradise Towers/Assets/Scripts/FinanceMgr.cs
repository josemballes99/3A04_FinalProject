using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using UnityEngine.UI;

using Mono.Data.Sqlite;
using System.Data;

namespace context {

	public class FinanceMgr : MonoBehaviour
	{
		public GameObject window;
		public Text revText, expText, incText;

		private static Stopwatch timer = new Stopwatch();

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
			Queries.createLog();
			timer.Reset ();
			timer.Start ();
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
			if (timer.ElapsedMilliseconds < 1000) {
				return;
			}
			revenuePerMinute+=2;
			expensePerMinute++;
			revText.text = revenues().ToString();
			expText.text = expenses().ToString();
			incText.text = (revenues() - expenses ()).ToString();

			timer.Reset ();
			timer.Start ();
		}

		public static void addFloor(FloorType floor){
			string stmt = "INSERT INTO Cost (tid, srcType, amt) VALUES ($1, Purchased $2 Floor, $3)";

			IDbConnection connection = Queries.connect (Queries.dbURL);

			IDbCommand command = connection.CreateCommand();
			command.CommandText = stmt;

			command = Queries.addParam (command, DbType.Int32, "1", tid);
			command = Queries.addParam (command, DbType.AnsiString, "2", floor.getType());
			command = Queries.addParam (command, DbType.Int32, "3", floor.getCost());				
			command.ExecuteNonQuery();
			tid++;
		}

		public static void addUpgrade(FloorType floor){
			string stmt = "INSERT INTO Cost (tid, srcType, amt) VALUES ($1, Purchased $2 Floor Upgrade, $3)";

			IDbConnection connection = Queries.connect (Queries.dbURL);
			IDbCommand command = connection.CreateCommand();
			command.CommandText = stmt;


			command = Queries.addParam (command, DbType.Int32, "1", tid);
			command = Queries.addParam (command, DbType.AnsiString, "2", floor.getType());
			command = Queries.addParam (command, DbType.Int32, "3", floor.getCost());				

			command.ExecuteNonQuery();
			tid++;
		}

		public static void addClient(FloorType floor){
			string stmt = "INSERT INTO Cost (tid, srcType, amt) VALUES ($1, Added worker to $2 Floor, $3)";

			IDbConnection connection = Queries.connect (Queries.dbURL);
			IDbCommand command = connection.CreateCommand();
			command.CommandText = stmt;

			command = Queries.addParam (command, DbType.Int32, "1", tid);
			command = Queries.addParam (command, DbType.AnsiString, "2", floor.getType());
			command = Queries.addParam (command, DbType.Int32, "3", floor.getCost());				

			command.ExecuteNonQuery();
			tid++;
		}			

	}



}