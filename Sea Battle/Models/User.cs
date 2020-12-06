/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/17/2020
 * Time: 21:23
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

namespace Models
{
	/// <summary>
	/// Description of User.
	/// </summary>
	public class User: Base<User>
	{
		public string Username
		{ get; private set; }
		
		public string Password
		{ get; private set; }
		
		public List<OnlineGame> OnlineGames
		{
			get { return OnlineGame.Items.Where(og => og.User.ID == ID || og.Opponent.ID == ID).ToList(); }
		}
		
		public User(string username, string password): base()
		{
			Init(username, password);
		}
		
		public User(Guid id, string username, string password): base(id)
		{
			//OnlineGames = onlineGames;
			Init(username, password);
		}
		
		private void Init(string username, string password)
		{
			Username = username;
			Password = password;
		}
		
		public override bool Equals(object obj)
		{
			User other = obj as User;
			if (other == null)
				return false;
			return ID == other.ID;
		}
		
		public override string ToString()
		{
			return Username;
		}

	}
}
