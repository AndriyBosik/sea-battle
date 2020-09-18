/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 2:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace GameObjects
{
	/// <summary>
	/// Description of Config.
	/// </summary>
	public class Config
	{
		public static string PROJECT_DIRECTORY = Environment.CurrentDirectory + "/../../";
		
		public static string EMPTY_CELL = "water";
		public static string SHIP_CELL = "ship";
		public static string ONE_DECK = "ship";
		public static string INTERNAL = "internal";
		public static string BEGIN = "begin";
		public static string END = "end";
		
		public static string VERTICAL_ORIENTATION = "Vertical";
		public static string HORIZONTAL_ORIENTATION = "Horizontal";
		public static string ONE_DECK_SHIP = "ship";
		
		public static int CELL_SIZE = 35;
		public static int DEFAULT_SHIP_SIZE = 1;
	}
}
