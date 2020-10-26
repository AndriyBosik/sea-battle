/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/25/2020
 * Time: 16:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

namespace Processors
{
	/// <summary>
	/// Description of DirectionProcessor.
	/// </summary>
	public static class DirectionProcessor
	{
		public static Direction GetNextDirection(Direction current)
		{
			switch (current)
			{
				case Direction.UP:
					return Direction.RIGHT;
				case Direction.RIGHT:
					return Direction.DOWN;
				case Direction.DOWN:
					return Direction.LEFT;
				case Direction.LEFT:
					return Direction.UP;
				default:
					return Direction.NO_DIRECTION;
			}
		}
		
		public static Direction GetPreviousDirection(Direction current)
		{
			switch (current)
			{
				case Direction.UP:
					return Direction.LEFT;
				case Direction.LEFT:
					return Direction.DOWN;
				case Direction.DOWN:
					return Direction.RIGHT;
				case Direction.RIGHT:
					return Direction.UP;
				default:
					return Direction.NO_DIRECTION;
			}
		}
	}
}
