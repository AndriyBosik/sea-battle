/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 14.11.2020
 * Time: 19:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of SerializableShip.
	/// </summary>
	[Serializable]
	public class SerializableShip
	{
		public Point Point
		{ get; set; }
		
		public string Orientation
		{ get; set; }
		
		public int Size
		{ get; set; }
		
		public SerializableShip() {}
		
		public SerializableShip(int size, string orientation, Point point)
		{
			Size = size;
			Orientation = orientation;
			Point = point;
		}
	}
}
