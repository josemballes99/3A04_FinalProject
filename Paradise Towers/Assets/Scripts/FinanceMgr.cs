using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using UnityEngine.UI;

using Mono.Data.Sqlite;
using System.Data;
using System.Linq;

namespace context {

	public class FinanceMgr : MonoBehaviour
	{
		public GameObject window;
		public Text revText, expText, incText;

		public Text protoItem;
		public GameObject listView;	
		public static List<Text> boxes = new List<Text>();


		public static Stopwatch timer = new Stopwatch();
		public static long lastTime = 0;

		public static int tid = 0;
		public static int Floor = 0;
		public static int Customer = 1;

		static int[] revenueSource = new int[2]{0,0};
		static int[] expenseSource = new int[2]{0,0};
		static int revenuePerPeriod = 0;

		public static int expense = 0;
		public static int netRevenue = 0;

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
			netRevenue = Queries.loadIncome ();
			revenuePerPeriod = Queries.loadRevenue ();
			expense = Queries.loadExpenses ();
			protoItem.text = "";
			UnityEngine.Debug.Log ("revenue = " + revenuePerPeriod);
		}

		// Use this for initialization
		void Start ()
		{
			timer.Start();
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
			if (timer.ElapsedMilliseconds - lastTime < 3000) {
				return;
			}
			long periods = (timer.ElapsedMilliseconds - lastTime) / 3000;
			lastTime = timer.ElapsedMilliseconds;
			netRevenue += revenuePerPeriod * Convert.ToInt32(periods);

			revText.text = revenues().ToString();
			expText.text = expenses().ToString();
			incText.text = (revenues() - expenses()).ToString();

			var f_log = Queries.loadTransactions ();

			if (f_log != null) {				
				bool first = true;
				foreach (string trans in f_log) {
					Text box;
					if (first == true) {
						box = protoItem;
						first = false;
					} else {
						box = Instantiate (protoItem);
						box.transform.SetParent (listView.transform);
						boxes.Add (box);
					}
					box.text = trans;
				}
			}

			IDbConnection connection = Queries.connect (Queries.dbURL);

			IDbCommand command = connection.CreateCommand();
			command.CommandText = "INSERT INTO Income(amt) VALUES ($1)";
			command = Queries.addParam (command, DbType.Int32, "1", revenuePerPeriod * Convert.ToInt32(periods));
			command.ExecuteNonQuery();

			IDbCommand command2 = connection.CreateCommand();
			command2.CommandText = "UPDATE Revenue SET amt=$1";
			command2 = Queries.addParam (command, DbType.Int32, "1", revenuePerPeriod);
			command2.ExecuteNonQuery();
			command.Dispose ();
			command2.Dispose ();
			connection.Close ();

		}

		public static void addFloor(FloorType floor){
			string stmt = "INSERT INTO Cost (tid, source, amt) VALUES ($1, 'Purchased Floor for', $2)";

			IDbConnection connection = Queries.connect (Queries.dbURL);

			IDbCommand command = connection.CreateCommand();
			command.CommandText = stmt;

			int type = floor.getType ();
			string name = FloorType.floorMap[type];

			command = Queries.addParam (command, DbType.Int32, "1", tid);
			//command = Queries.addParam (command, DbType.String, "2", name);
			command = Queries.addParam (command, DbType.Int32, "2", floor.getCost());				
			command.ExecuteNonQuery();
			command.Dispose ();
			connection.Close ();
			revenuePerPeriod += floor.revenues();
			expense += floor.getCost ();
			tid++;
		}

		public static void addUpgrade(FloorType floor){
			string stmt = "INSERT INTO Cost (tid, sourcee, amt) VALUES ($1, Purchased Floor Upgrade, $3)";
			string stmt2 = "UPDATE Floors SET level=level+1 WHERE pos=$4";

			IDbConnection connection = Queries.connect (Queries.dbURL);
			IDbCommand command = connection.CreateCommand();
			command.CommandText = stmt;

			command = Queries.addParam (command, DbType.Int32, "1", tid);
			//command = Queries.addParam (command, DbType.AnsiString, "2", FloorType.floorMap[floor.getType()]);
			command = Queries.addParam (command, DbType.Int32, "3", FloorType.upgrades[floor.tier()]);				
			command.ExecuteNonQuery();
			command.Dispose ();

			IDbCommand command2 = connection.CreateCommand();
			command2.CommandText = stmt2;
			command = Queries.addParam (command, DbType.Int32, "4", FloorType.upgrades[floor.tier()]);
			command2.Dispose ();
			connection.Close ();

			expense += FloorType.upgrades[floor.tier()];
			floor.upgrade ();
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