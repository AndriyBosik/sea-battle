/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11.11.2020
 * Time: 12:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using GameObjects;

namespace Models
{
	/// <summary>
	/// Description of Ship.
	/// </summary>
	public class Ship: Model<Ship>
	{
		private Guid fieldId;
		
		public readonly int Size;
		
		public readonly string Orientation;
		
		public readonly Point Point;
		
		public Field Field
		{
			get { return Field.Items.Where(field => field.ID == fieldId).FirstOrDefault(); }
		}
		
		public Ship(int size, string orientation, Point point)
		{
			Size = size;
			Orientation = orientation;
			Point = point;
		}
	}
}
