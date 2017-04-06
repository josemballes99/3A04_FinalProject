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
		//string sqlQuery = "SELECT name FROM sqlite_master WHERE type='table'";
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
			//Debug.Log( "table= "+name);//+"  name ="+name+"  random ="+  rand);
		}
		reader.Close();
		reader = null;
		cmd.Dispose();
		cmd = null;
		connection.Close();
		connection = null;

		return objects;
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
		command.CommandText = "CREATE TABLE IF NOT EXISTS Cost(tid INTEGER, source INTEGER, amt INTEGER)";
		command.ExecuteNonQuery();

		IDbCommand command2 = connection.CreateCommand();
		command2.CommandText = "CREATE TABLE IF NOT EXISTS Floors(pos INTEGER, type INTEGER, level INTEGER)";
		command2.ExecuteNonQuery();

		IDbCommand command3 = connection.CreateCommand();
		command3.CommandText = "CREATE TABLE IF NOT EXISTS Occupants(pos INTEGER, num INTEGER)";
		command3.ExecuteNonQuery();

		IDbCommand command4 = connection.CreateCommand();
		command4.CommandText = "CREATE TABLE IF NOT EXISTS Upgrade(ftype INTEGER, amt INTEGER)";
		command4.ExecuteNonQuery();
	}
}

