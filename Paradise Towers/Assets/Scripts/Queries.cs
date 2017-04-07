using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Mono.Data.Sqlite;
using System.Data;

public class Queries
{

	private static string localDBUrl = "URI=file:Objectives.db";
	public static string dbURL = "URI=file:" + Application.persistentDataPath + "/Hotel.db"; //Path to database.


	//First load
	public static List<Objective> loadObjectives () {		

		IDbConnection connection = connect(localDBUrl);
		IDbCommand cmd = connection.CreateCommand();

		string sqlQuery = "SELECT * FROM Objectives";
		cmd.CommandText = sqlQuery;

		IDataReader reader = cmd.ExecuteReader();
		List<Objective> objects = new List<Objective> ();

		while (reader.Read())
		{
			int id = reader.GetInt32(0);
			string caption = reader.GetString (1);
			int progress = reader.GetInt32(2);
			int reward = reader.GetInt32(3);
			string query = reader.GetString (4);
			int condition = reader.GetInt32 (5);

			Objective obj = new Objective (id, caption, progress, reward, query, condition);
			objects.Add (obj);
		}
		reader.Close();
		reader = null;
		cmd.Dispose();
		cmd = null;
		connection.Close();
		connection = null;

		return objects;
	}

	public static List<string> loadTransactions () {		

		IDbConnection connection = connect(dbURL);
		IDbCommand cmd = connection.CreateCommand();

		string sqlQuery = "SELECT * FROM Cost";
		cmd.CommandText = sqlQuery;

		IDataReader reader = cmd.ExecuteReader();
		List<string> events = new List<string> ();
		while (reader.Read())
		{
			int tid = reader.GetInt32(0);
			string evt = reader.GetString (1);
			int amt = reader.GetInt32(2);
			events.Add (tid.ToString () + "\t\t" + evt + "\t\t\t" + amt);
		}
		reader.Close();
		reader = null;
		cmd.Dispose();
		cmd = null;
		connection.Close();
		connection = null;

		return events;
	}


	public static int loadExpenses(){
		IDbConnection connection = connect (dbURL);
		IDbCommand command = connection.CreateCommand();
		command.CommandText = "SELECT SUM(amt) FROM Cost";
		IDataReader reader = command.ExecuteReader();
		int expense = 0;

		while (reader.Read ()) {
			if (!reader.IsDBNull(0)) {
				int id = reader.GetInt32 (0);
				expense = id;
			}
		}
		connection.Close ();
		return expense;
	}

	public static int loadIncome(){
		IDbConnection connection = connect (dbURL);
		IDbCommand command = connection.CreateCommand();
		command.CommandText = "SELECT SUM(amt) FROM Income";
		IDataReader reader = command.ExecuteReader();
		int income = 0;

		while (reader.Read ()) {
			if (!reader.IsDBNull(0)) {
				int id = reader.GetInt32 (0);
				income = id;
			}
		}
		connection.Close ();
		return income;
	}

	public static int loadRevenue(){
		IDbConnection connection = connect (dbURL);
		IDbCommand command = connection.CreateCommand();
		command.CommandText = "SELECT SUM(amt) FROM Revenue";
		IDataReader reader = command.ExecuteReader();
		int revenue = 0;

		while (reader.Read ()) {
			if (!reader.IsDBNull(0)) {
				int id = reader.GetInt32 (0);
				revenue = id;
			}
		}
		connection.Close ();
		return revenue;
	}


	public static void addFloor (int num, int type, int tid, int cost){

		IDbConnection connection = connect (dbURL);

		IDbCommand command = connection.CreateCommand();
		command.CommandText = "INSERT INTO Floors (pos, type, level) VALUES ($1, $2, 0)";
		command = addParam (command, DbType.Int32, "1", num);
		command = addParam (command, DbType.Int32, "2", type);
		command.ExecuteNonQuery();
		command.Dispose();
		command = null;


		IDbCommand command2 = connection.CreateCommand();
		command2.CommandText = "INSERT INTO Occupants (pos, num) VALUES($3, 0)";
		command2 = addParam(command2, DbType.Int32, "3", num);
		command2.ExecuteNonQuery();
		command2.Dispose();
		command2 = null;


		connection.Close();
		connection = null;
	}


	public static void removeFloor (int pos){

		IDbConnection connection = connect(dbURL);
		IDbCommand command = connection.CreateCommand();

		command.CommandText = "DELETE FROM Floors WHERE pos=$1";
		command = addParam (command, DbType.Int32, "1", pos);
		command.ExecuteNonQuery();

		command.Dispose();
		command = null;

		connection.Close();
		connection = null;
	}


	public static int execute(string stmt){
		IDbConnection connection = connect(dbURL);
		IDbCommand command = connection.CreateCommand();
		command.CommandText = stmt;
		IDataReader reader = command.ExecuteReader();

		while (reader.Read ()) {
			if (!reader.IsDBNull(0)) {
				int id = reader.GetInt32 (0);
				return id;
			}
		}
		return 0;
	}

	public static IDbConnection connect(string url){
		IDbConnection connection;
		connection = (IDbConnection) new SqliteConnection(url);
		connection.Open(); //Open connection to the database.
		return connection;
	}

	public static IDbCommand addParam(IDbCommand cmd, DbType type, string name, object val){
		IDbDataParameter param = cmd.CreateParameter();
		param.DbType = type;
		param.ParameterName = name;
		param.Value = val;
		cmd.Parameters.Add(param);
		return cmd;
	}


	public static void createLog(){
		IDbConnection connection = Queries.connect (Queries.dbURL);

		IDbCommand command = connection.CreateCommand();
		command.CommandText = "DROP TABLE Cost";
		command.ExecuteNonQuery();
		command = connection.CreateCommand();
		command.CommandText = "CREATE TABLE IF NOT EXISTS Cost(tid INTEGER, source TEXT, amt INTEGER, PRIMARY KEY(tid))";
		command.ExecuteNonQuery();

		IDbCommand command2 = connection.CreateCommand();
		command2.CommandText = "DROP TABLE Floors";
		command2.ExecuteNonQuery();
		command2 = connection.CreateCommand();
		command2.CommandText = "CREATE TABLE IF NOT EXISTS Floors(pos INTEGER, type INTEGER, level INTEGER)";
		command2.ExecuteNonQuery();

		IDbCommand command3 = connection.CreateCommand();
		command3.CommandText = "DROP TABLE Occupants";
		command3.ExecuteNonQuery();
		command3 = connection.CreateCommand();
		command3.CommandText = "CREATE TABLE IF NOT EXISTS Occupants(pos INTEGER, num INTEGER)";
		command3.ExecuteNonQuery();

		IDbCommand command4 = connection.CreateCommand();
		command4.CommandText = "DROP TABLE Upgrade";
		command4.ExecuteNonQuery();
		command4 = connection.CreateCommand();
		command4.CommandText = "CREATE TABLE IF NOT EXISTS Upgrade(ftype INTEGER, amt INTEGER)";
		command4.ExecuteNonQuery();

		IDbCommand command5 = connection.CreateCommand();
		command5.CommandText = "DROP TABLE Income";
		command5.ExecuteNonQuery();
		command5 = connection.CreateCommand();
		command5.CommandText = "CREATE TABLE IF NOT EXISTS Income(amt INTEGER)";
		command5.ExecuteNonQuery();

		IDbCommand command6 = connection.CreateCommand();
		command6.CommandText = "DROP TABLE Revenue";
		command6.ExecuteNonQuery();
		command6 = connection.CreateCommand();
		command6.CommandText = "CREATE TABLE IF NOT EXISTS Revenue(amt INTEGER)";
		command6.ExecuteNonQuery();


		command.Dispose ();
		command2.Dispose ();
		command3.Dispose ();
		command4.Dispose ();
		command5.Dispose ();
		command6.Dispose ();
		connection.Close ();
	}
}