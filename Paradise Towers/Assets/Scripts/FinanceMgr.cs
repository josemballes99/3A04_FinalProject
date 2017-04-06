using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

		public static List<Objective> objects = new List<Objective> ();

		public Text protoItem;
		public GameObject listView;	
		public static List<Text> boxes = new List<Text>();


		private static Stopwatch timer = new Stopwatch();

		public static int tid = 0;
		public static int Floor = 0;
		public static int Customer = 1;

		static int[] revenueSource = new int[2]{0,0};
		static int[] expenseSource = new int[2]{0,0};
		static int revenuePerPeriod = 0;

		static int expense = 0;
		static int netRevenue = 0;

		public static int netIncome(){
			return netRevenue - expense;
		}

		public static int revenues(){
			return netRevenue; // * time elapsed
		}

		public static int expenses(){
			return expense; // * time elapsed
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
			incText.text = netIncome().ToString();
		}
			

		//Add data structure to track financial events so we can put them in the scrollview

		public static void addRevenueSource(int source, int amt){
			revenuePerPeriod += amt;
			revenueSource [source]++;
		}

		public static void addExpenseSource(int source, int amt){
			expense += amt;
			expenseSource [source]++;

			//INSERT INTO COST (tid, source, amt) VALUES (tid, source, amt);
		}

		// Update is called once per frame
		void Update ()
		{			
			if (timer.ElapsedMilliseconds < 3000) {
				return;
			}
			netRevenue += revenuePerPeriod;

			revText.text = revenues().ToString();
			expText.text = expenses().ToString();
			incText.text = (revenues() - expenses()).ToString();

			timer.Reset ();
			timer.Start ();
		}

		public static void addFloor(FloorType floor){
			string stmt = "INSERT INTO Cost (tid, source, amt) VALUES ($1, 'Purchased $2 Floor', $3)";

			IDbConnection connection = Queries.connect (Queries.dbURL);

			IDbCommand command = connection.CreateCommand();
			command.CommandText = stmt;

			int type = floor.getType ();
			string name = FloorType.floorMap[type];

			command = Queries.addParam (command, DbType.Int32, "1", tid);
			command = Queries.addParam (command, DbType.String, "2", name);//Make string
			command = Queries.addParam (command, DbType.Int32, "3", floor.getCost());				
			command.ExecuteNonQuery();
			command.Dispose ();
			connection.Close ();
			revenuePerPeriod += floor.revenues();
			tid++;
		}

		public static void addUpgrade(FloorType upgrade){
			string stmt = "INSERT INTO Cost (tid, srcType, amt) VALUES ($1, Purchased $2 Floor Upgrade, $3)";

			IDbConnection connection = Queries.connect (Queries.dbURL);
			IDbCommand command = connection.CreateCommand();
			command.CommandText = stmt;

			command = Queries.addParam (command, DbType.Int32, "1", tid);
			command = Queries.addParam (command, DbType.AnsiString, "2", upgrade.getType());
			command = Queries.addParam (command, DbType.Int32, "3", upgrade.getCost());				
			command.ExecuteNonQuery();
			command.Dispose ();
			connection.Close ();
			revenuePerPeriod += upgrade.revenues();
			tid++;
		}

		public static void addClient(FloorType client){
			string stmt = "INSERT INTO Cost (tid, srcType, amt) VALUES ($1, Added worker to $2 Floor, $3)";

			IDbConnection connection = Queries.connect (Queries.dbURL);
			IDbCommand command = connection.CreateCommand();
			command.CommandText = stmt;

			command = Queries.addParam (command, DbType.Int32, "1", tid);
			command = Queries.addParam (command, DbType.AnsiString, "2", client.getType());
			command = Queries.addParam (command, DbType.Int32, "3", client.getCost());
			command.ExecuteNonQuery();
			command.Dispose ();
			connection.Close ();
			tid++;
		}			

	}



}