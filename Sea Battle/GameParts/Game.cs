/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/25/2020
 * Time: 15:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Config;

using GameObjects;

using Processors;

namespace FieldEditorParts
{
	/// <summary>
	/// Description of GAme.
	/// </summary>
	public class Game
	{
		private Field first;
		private Field second;
		private Move move;
		
		public Game(Field first, Field second)
		{
			this.first = first;
			this.second = second;
			this.move = Move.FIRST;
		}
		
		public bool TryMove(Field field)
		{
			if ((move == Move.FIRST && field == first) || (move == Move.SECOND && field == second))
			{
				return true;
			}
			return false;
		}
		
		public void MakeMove(int x, int y)
		{
			if (move == Move.FIRST)
			{
				//first.UpdateCell(x, y, ImageProcessor.GetImage(Images.BOMB));
			}
			else
			{
				//second.UpdateCell(x, y, ImageProcessor.GetImage(Images.BOMB));
			}
			ChangeMove();
		}
		
		private void ChangeMove()
		{
			if (move == Move.FIRST)
			{
				move = Move.SECOND;
			}
			else
			{
				move = Move.FIRST;
			}
		}
	}
}
