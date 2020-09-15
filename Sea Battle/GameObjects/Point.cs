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
	public class Point
	{
		
		private int x;
		private int y;
		
		public int X
		{
			get
			{
				return x;
			}
			set
			{
				x = value;
			}
		}
		
		public int Y
		{
			get
			{
				return y;
			}
			set
			{
				y = value;
			}
		}
		
		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
	}
}
