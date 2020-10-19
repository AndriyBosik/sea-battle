/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/25/2020
 * Time: 15:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Config
{
	/// <summary>
	/// Description of Gameplay.
	/// </summary>
	public static class Gameplay
	{
		public const string FIRST_PLAYER_FIELD = "FirstPlayer";
		public const string SECOND_PLAYER_FIELD = "SecondPlayer";
		
		public const string VERTICAL_ORIENTATION = "Vertical";
		public const string HORIZONTAL_ORIENTATION = "Horizontal";
		
		public const int CELL_SIZE = 35;
		public const int DEFAULT_SHIP_SIZE = 1;
		
		public const int INITIAL_MONEY = 1000;
		public const int INITIAL_HEALTH_POINT = 1000;
		
		public const int INFINITY = int.MaxValue;
	}
}
