/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/17/2020
 * Time: 22:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data.Common;
using Models;

namespace Database
{
	/// <summary>
	/// Description of UserDAO.
	/// </summary>
	public class UserDAO: DAO<User>
	{
		public const string ID = "ID";
		public const string USERNAME = "Username";
		public const string PASSWORD = "Password";
		
		protected override string SetTableName()
		{
			return "Users";
		}
		
		protected override void RemoveLinks(Guid id)
		{
			new OnlineGameDAO().DeleteByQuery("userId='" + id + "' or opponentId='" + id + "'");
		}
		
		protected override User Parse(DbDataReader reader)
		{
			Guid id = Guid.Parse(reader.GetString(0));
			return new User(
				id,
				reader.GetString(1),
				reader.GetString(2)
			);
		}
		
		protected override List<string> KeysToInsert()
		{
			return new List<string>
			{
				ID,
				USERNAME,
				PASSWORD
			};
		}
		protected override List<string> ValuesToInsert(User user)
		{
			return new List<string>
			{
				user.ID.ToString(),
				user.Username,
				user.Password
			};
		}
		
		protected override List<string> KeysToUpdate()
		{
			return new List<string>
			{
				USERNAME,
				PASSWORD
			};
		}
		
		protected override List<string> ValuesToUpdate(User user)
		{
			return new List<string>
			{
				user.Username,
				user.Password
			};
		}
	}
}
