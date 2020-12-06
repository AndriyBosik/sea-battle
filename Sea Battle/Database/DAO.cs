/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/17/2020
 * Time: 21:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Data.OleDb;
using System.Collections.Generic;

using System.Windows;
using Config;
using Models;

namespace Database
{
	/// <summary>
	/// Description of DAO.
	/// </summary>
	public abstract class DAO<T> where T: Base<T>
	{
		public const int DEFAULT_COUNT = 0;
		
		public static List<T> Items = new List<T>();
		private static OleDbConnection connection;
		
		private readonly string tableName;
		
		public DAO()
		{
			tableName = SetTableName();
			if (connection == null)
			{
				connection = new OleDbConnection();
				connection.ConnectionString = SolutionConfig.CONNECTION_STRING;
			}
		}
		
		protected abstract string SetTableName();
		
		protected abstract void RemoveLinks(Guid id);
		
		protected abstract T Parse(DbDataReader reader);
		
		protected abstract List<string> KeysToInsert();
		protected abstract List<string> ValuesToInsert(T model);
		
		protected abstract List<string> KeysToUpdate();
		protected abstract List<string> ValuesToUpdate(T model);
		
		public T SelectByID(Guid id)
		{
			return SelectFirstByQuery("ID='" + id + "'");
		}
		
		public T SelectFirstByQuery(string query)
		{
			var sqlQuery = "select * from " + tableName + " where " + query;
			var cmd = new OleDbCommand(sqlQuery, connection);
			T model = null;
			OpenConnection(connection);
			var reader = cmd.ExecuteReader();
			if (!reader.HasRows)
			{
				return null;
			}
			while (reader.Read())
			{
				model = Parse(reader);
				break;
			}
			reader.Close();
			CloseConnection(connection);
			return model;
		}
		
		public List<T> SelectAllByQuery(string query,
		                                int count = DEFAULT_COUNT,
		                                string sortingColumn = "",
		                                Sorting sorting = Sorting.none)
		{
			var sqlQuery = "select ";
			if (count != DEFAULT_COUNT)
				sqlQuery += "top " + count;
			sqlQuery += " * from " + tableName;
			if (!query.Equals(""))
				sqlQuery += " where " + query;
			if (sorting != Sorting.none)
				sqlQuery += " order by " + sortingColumn + " " + sorting;
			var cmd = new OleDbCommand(sqlQuery, connection);
			List<T> data = new List<T>();
			OpenConnection(connection);
			var reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				data.Add(Parse(reader));
			}
			reader.Close();
			CloseConnection(connection);
			return data;
		}
		
		public void InsertData(T model)
		{
			var cmd = new OleDbCommand();
			cmd.CommandType = CommandType.Text;
			var keysList = KeysToInsert();
			if (keysList.Count == 0)
				return;
			var keys = "";
			var values = "";
			foreach (var key in keysList)
			{
				keys += "[" + key + "],";
				values += "?,";
			}
			keys = keys.Remove(keys.Length - 1);
			values = values.Remove(values.Length - 1);
			
			cmd.CommandText = "insert into " + tableName + " (" + keys + ") values (" + values + ")";
			
			var keysValues = keysList.Zip(ValuesToInsert(model), (key, value) => new {
				Key = key, Value = value
			} );
			
			foreach (var keyValue in keysValues)
			{
				cmd.Parameters.AddWithValue("@" + keyValue.Key, keyValue.Value);
			}
			cmd.Connection = connection;
			
			OpenConnection(connection);
			cmd.ExecuteNonQuery();
			CloseConnection(connection);
		}
		
		public void UpdateById(Guid id, T model)
		{
			var cmd = new OleDbCommand();
			cmd.CommandType = CommandType.Text;
			var keysValues = KeysToUpdate().Zip(ValuesToUpdate(model), (key, value) => new {
            	Key = key, Value = value
            } );
			var keysValuesString = "";
			foreach (var keyValue in keysValues)
			{
				keysValuesString += "[" + keyValue.Key + "]=?,";
			}
			keysValuesString = keysValuesString.Remove(keysValuesString.Length - 1);
			cmd.CommandText = "update " + tableName + " set " + keysValuesString + " where [ID]=?";
			foreach (var keyValue in keysValues)
			{
				cmd.Parameters.AddWithValue("@" + keyValue.Key, keyValue.Value);
			}
			cmd.Parameters.AddWithValue("@ID", id.ToString());
			cmd.Connection = connection;
			
			OpenConnection(connection);
			cmd.ExecuteNonQuery();
			CloseConnection(connection);
		}
		
		public void DeleteById(Guid id)
		{
			DeleteByQuery("ID='" + id + "'");
		}
		
		public void DeleteByQuery(string query)
		{
			var cmd = new OleDbCommand();
			cmd.CommandType = CommandType.Text;
			var toDelete = SelectAllByQuery(query);
			foreach (var delete in toDelete)
				RemoveLinks(delete.ID);
			cmd.CommandText = "delete * from " + tableName + " where " + query;
			cmd.Connection = connection;
			OpenConnection(connection);
			cmd.ExecuteNonQuery();
			CloseConnection(connection);
		}
		
		private void OpenConnection(OleDbConnection connection)
		{
			if (connection.State != ConnectionState.Open)
				connection.Open();
		}
		
		private void CloseConnection(OleDbConnection connection)
		{
			if (connection.State != ConnectionState.Closed)
				connection.Close();
		}
		
		public enum Sorting
		{
			none,
			asc,
			desc
		}
		
	}
}
