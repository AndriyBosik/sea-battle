/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 17.11.2020
 * Time: 17:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

using Config;

namespace Models
{
	/// <summary>
	/// Description of TableManager.
	/// </summary>
	public class TableManager
	{
		private static Dictionary<string, TableManager> tableManagers = new Dictionary<string, TableManager>();
		private static DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory("System.Data.OleDb");
		private static DbConnection connection = dbProviderFactory.CreateConnection();
		
		private DbDataAdapter da;
		private DataTable dt;
		private DataTable temp;
		private DbCommand cmd;
		
		private static DbConnection Connection
		{
			get
			{
				while (true)
				{
					try
					{
						if (connection.State == ConnectionState.Broken)
							connection.Close();
						if (connection.State == ConnectionState.Closed)
							connection.Open();
						return connection;
					}
					catch {}
				}
			}
		}
		
		public DataTable Table
		{ get { return dt; } }
		
		static TableManager()
		{
			connection.ConnectionString = "provider=Microsoft.JET.OLEDB.4.0;data source=" + SolutionConfig.MDB_DATABASE_FILE;
		}
		
		internal TableManager(string tableName)
		{
			try
			{
				da = dbProviderFactory.CreateDataAdapter();
				cmd = dbProviderFactory.CreateCommand();
				DbCommandBuilder cb = dbProviderFactory.CreateCommandBuilder();
				cmd.Connection = Connection;
				cb.ConflictOption = ConflictOption.OverwriteChanges;
				cb.DataAdapter = da;
				dt = new DataTable();
				temp = new DataTable();
				//dt.TableName = temp.TableName = tableName;
				dt.TableName = temp.TableName = "users";
				cmd.CommandText = "Select * from " + Table.TableName;
				da.SelectCommand = cmd;
				da.InsertCommand = cb.GetInsertCommand();
				da.DeleteCommand = cb.GetDeleteCommand();
				da.UpdateCommand = cb.GetUpdateCommand();
				Recharge("1=2");
			}
			catch {}
		}
		
		public static TableManager GetTableManager(string tableName)
		{
			TableManager tm = null;
			try
			{
				tm = tableManagers[tableName];
			}
			catch
			{
				try
				{
					tm = new TableManager(tableName);
					tableManagers.Add(tableName, tm);
				}
				catch {}
			}
			return tm;
		}
		
		internal int Recharge(string query)
		{
			var tn = Table.TableName;
			cmd.CommandText = tn + ".* from " + tn + ((query == "") ? "" : " where " + query);
			try
			{
				return da.Fill(dt);
			}
			catch {}
			return 0;
		}
		
		internal DataRowCollection GetIds(string query)
		{
			cmd.CommandText = "Select ID from " + Table.TableName + ((query == null) ? "" : " where " + query);
			try
			{
				da.Fill(temp);
				return temp.Rows;
			}
			catch {}
			return null;
		}
		
		internal int Update(DataRow dr)
		{
			return da.Update(new [] {dr});
		}
		
		internal int Update(DataRow[] drs)
		{
			return da.Update(drs);
		}
		
	}
}
