/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 14.11.2020
 * Time: 18:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Entities
{
	/// <summary>
	/// Description of SerializableField.
	/// </summary>
	[Serializable]
	public class SerializableField
	{
		public int Rows
		{ get; set; }
		
		public int Columns
		{ get; set; }
		
		public List<SerializableShip> Ships
		{ get; set; }
		
		public SerializableField() {}
		
		public SerializableField(int rows, int columns, List<Ship> ships): this()
		{
			Rows = rows;
			Columns = columns;
			Ships = new List<SerializableShip>();
			foreach (var ship in ships)
			{
				Ships.Add(ship.GetSerializable());
			}
		}
	}
}
