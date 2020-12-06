/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 28.11.2020
 * Time: 0:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameObjects;

namespace Models
{
	/// <summary>
	/// Description of Ship.
	/// </summary>
	public class Ship: Base<Ship>
	{
		public Point Point
		{ get; private set; }
		
		public int Size
		{ get; private set; }
		
		public string Orientation
		{ get; private set; }
		
		public Guid FieldID
		{ get; private set; }
		
		public Field Field
		{ get; private set; }
		
		public Ship(Point point, int size, string orientation, Field field)
		{
			Init(point, size, orientation, field);
		}
		
		public Ship(Guid id, Point point, int size, string orientation, Field field): base(id)
		{
			Init(point, size, orientation, field);
		}
		
		private void Init(Point point, int size, string orientation, Field field)
		{
			Point = point;
			Size = size;
			Orientation = orientation;
			Field = field;
		}
		
	}
}
