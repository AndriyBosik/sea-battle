/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/17/2020
 * Time: 21:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Models
{
	/// <summary>
	/// Description of OnlineGame.
	/// </summary>
	public class OnlineGame: Base<OnlineGame>
	{
		public Guid UserID
		{ get; private set; }
		
		public Guid OpponentID
		{ get; private set; }
		
		public User User
		{ get; private set; }
		
		public User Opponent
		{ get; private set; }
		
		public GameResult Result
		{ get; private set; }
		
		public OnlineGame(User user, User opponent, GameResult result)
		{
			Init(user, opponent, result);
		}
		
		public OnlineGame(Guid ID, User user, User opponent, GameResult result): base(ID)
		{
			Init(user, opponent, result);
		}
		
		private void Init(User user, User opponent, GameResult result)
		{
			User = user;
			Opponent = opponent;
			Result = result;
		}
		
		public enum GameResult
		{
			WIN,
			LOSE
		}
		
	}
}
