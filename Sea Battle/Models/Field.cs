/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11.11.2020
 * Time: 12:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Linq;

namespace Models
{
	/// <summary>
	/// Description of Field.
	/// </summary>
	public class Field: Model<Field>
	{
		private Guid userId;
		
		public readonly int Rows;
		
		public readonly int Columns;
		
		public User User
		{
			get { return User.Items.Where(user => user.ID == userId).FirstOrDefault(); }
			set { userId = value.ID; }
		}
		
		public Field(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
		}
	}
}
