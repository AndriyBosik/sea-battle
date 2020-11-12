/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11.11.2020
 * Time: 12:03
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
	public class User: Model<User>
	{
		public String Username
		{ get; private set; }
		
		public String Password
		{ get; private set; }
		
		public List<Field> Fields
		{ get { return Field.Items.Where(field => field.User == this).ToList(); } }
		
		public User(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}
