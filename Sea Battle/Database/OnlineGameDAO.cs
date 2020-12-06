/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/18/2020
 * Time: 00:15
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
	/// Description of OnlineGameDAO.
	/// </summary>
	public class OnlineGameDAO: DAO<OnlineGame>
	{
		public const string ID = "ID";
		public const string USER_ID = "UserID";
		public const string OPPONENT_ID = "OpponentID";
		public const string GAME_RESULT = "GameResult";
		
		protected override string SetTableName()
		{
			return "OnlineGames";
		}
		
		protected override void RemoveLinks(Guid id) {}
		
		protected override OnlineGame Parse(DbDataReader reader)
		{
			var userDAO = new UserDAO();
			var user = userDAO.SelectByID(Guid.Parse(reader.GetString(1)));
			var opponent = userDAO.SelectByID(Guid.Parse(reader.GetString(2)));
			return new OnlineGame(
				Guid.Parse(reader.GetString(0)),
				user,
				opponent,
				(OnlineGame.GameResult)Enum.Parse(typeof(OnlineGame.GameResult), reader.GetString(3), true)
			);
		}
		
		protected override List<string> KeysToInsert()
		{
			return new List<string>
			{
				ID,
				USER_ID,
				OPPONENT_ID,
				GAME_RESULT
			};
		}
		
		protected override List<string> ValuesToInsert(OnlineGame onlineGame)
		{
			return new List<string>
			{
				onlineGame.ID.ToString(),
				onlineGame.User.ID.ToString(),
				onlineGame.Opponent.ID.ToString(),
				onlineGame.Result.ToString()
			};
		}
		
		protected override List<string> KeysToUpdate()
		{
			return new List<string>
			{
				USER_ID,
				OPPONENT_ID,
				GAME_RESULT
			};
		}
		
		protected override List<string> ValuesToUpdate(OnlineGame onlineGame)
		{
			return new List<string>
			{
				onlineGame.User.ID.ToString(),
				onlineGame.Opponent.ID.ToString(),
				onlineGame.Result.ToString()
			};
		}
		
	}
}
