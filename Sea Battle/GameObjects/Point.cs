/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 1:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace GameObjects
{
	/// <summary>
	/// Description of Point.
	/// </summary>
	[Serializable]
	public class Point
	{
		public int X
		{ get; set; }
		
		public int Y
		{ get; set; }
		
		public Point() {}
		
		public Point(int x, int y): this()
		{
			X = x;
			Y = y;
		}
	}
}
