using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Mono.Data.Sqlite;
using System.Data;

public class Queries
{




	public static List<Objective> loadDB () {
		string dbURL = "URI=file:Objectives.db"; //Path to database.

		IDbConnection connection;
		connection = (IDbConnection) new SqliteConnection(dbURL);
		connection.Open(); //Open connection to the database.

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

		string dbURL = "URI=file:" + Application.persistentDataPath + "/Objectives.db"; //Path to database.

		IDbConnection connection;
		connection = (IDbConnection) new SqliteConnection(dbURL);
		connection.Open(); //Open connection to the database.

		string q1 = "INSERT INTO Floors (pos, type, level) VALUES ($1, $2, 0)";
		string q2 = "INSERT INTO Occupants (pos, num) VALUES($3, 0)";
		string q3 = "INSERT INTO Cost(tid, source, amt) VALUES ($4, 0, $5)";

		IDbCommand command = connection.CreateCommand();
		command.CommandText = q1;

		IDbCommand command2 = connection.CreateCommand();
		command2.CommandText = q2;

		IDbCommand command3 = connection.CreateCommand();
		command2.CommandText = q3;

		IDbDataParameter param = command.CreateParameter();
		param.DbType = DbType.Int32;
		param.ParameterName = "1";
		param.Value = num;
		command.Parameters.Add(param);

		IDbDataParameter param2 = command.CreateParameter();
		param2.DbType = DbType.Int32;
		param2.ParameterName = "2";
		param2.Value = type;
		command.Parameters.Add(param2);
		command.ExecuteNonQuery();


		IDbDataParameter param3 = command2.CreateParameter();
		param3.DbType = DbType.Int32;
		param3.ParameterName = "3";
		param3.Value = num;
		command2.Parameters.Add(param3);
		command2.ExecuteNonQuery();


		IDbDataParameter param4 = command3.CreateParameter();
		param4.DbType = DbType.Int32;
		param4.ParameterName = "4";
		param4.Value = num;
		command3.Parameters.Add(param4);

		IDbDataParameter param5 = command3.CreateParameter();
		param5.DbType = DbType.Int32;
		param5.ParameterName = "5";
		param5.Value = num;
		command3.Parameters.Add(param5);
		command3.ExecuteNonQuery();
	}


	public static void removeFloor (int pos){

		string dbURL = "URI=file:" + Application.persistentDataPath + "/Objectives.db"; //Path to database.

		IDbConnection connection;
		connection = (IDbConnection) new SqliteConnection(dbURL);
		connection.Open(); //Open connection to the database.

		string sqlQuery = "DELETE FROM Floors WHERE pos=$1";

		IDbCommand command = connection.CreateCommand();

		command.CommandText = sqlQuery;

		IDbDataParameter param = command.CreateParameter();
		param.DbType = DbType.Int32;
		param.ParameterName = "1";
		param.Value = pos;


		command.Parameters.Add(param);
		command.ExecuteNonQuery();
	}

}

