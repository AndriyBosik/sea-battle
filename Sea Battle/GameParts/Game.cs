/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/25/2020
 * Time: 15:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Entities;

using GameObjects;

namespace FieldEditorParts
{
	/// <summary>
	/// Description of GAme.
	/// </summary>
	public class Game
	{
		private Player firstPlayer;
		private Player secondPlayer;
		private Move move;
		
		public Game(Player firstPlayer, Player secondPlayer)
		{
			this.firstPlayer = firstPlayer;
			this.secondPlayer = secondPlayer;
			this.move = Move.FIRST;
		}
		
		public bool TryMove(Field field)
		{
			if ((move == Move.FIRST && field == secondPlayer.Field) || (move == Move.SECOND && field == firstPlayer.Field))
			{
				return true;
			}
			return false;
		}
		
		public Player GetCurrentPlayer()
		{
			return move == Move.FIRST ? firstPlayer : secondPlayer;
		}
		
		public Field GetCurrentField()
		{
			return move == Move.FIRST ? secondPlayer.Field : firstPlayer.Field;
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
