/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/25/2020
 * Time: 15:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of GAme.
	/// </summary>
	public class Game
	{
		private Player firstPlayer;
		private Player secondPlayer;
		private MoveOrder move;
		
		public Game(Player firstPlayer, Player secondPlayer)
		{
			this.firstPlayer = firstPlayer;
			this.secondPlayer = secondPlayer;
			this.move = MoveOrder.FIRST;
		}
		
		public bool IsFirstPlayer(Player player)
		{
			return player == firstPlayer;
		}
		
		public bool TryMove(Field field)
		{
			if ((move == MoveOrder.FIRST && field == secondPlayer.Field) || (move == MoveOrder.SECOND && field == firstPlayer.Field))
				return true;
			return false;
		}
		
		public Player GetCurrentPlayer()
		{
			return move == MoveOrder.FIRST ? firstPlayer : secondPlayer;
		}
		
		public Player GetAnotherPlayer()
		{
			return move == MoveOrder.FIRST ? secondPlayer : firstPlayer;
		}
		
		public Field GetCurrentField()
		{
			return move == MoveOrder.FIRST ? secondPlayer.Field : firstPlayer.Field;
		}
		
		public bool MakeMove(Field field, int row, int column)
		{
			if (field != GetCurrentField())
				return false;
			GetAnotherPlayer().Money += GetCurrentPlayer().Shot(field, row, column);
			ChangeMove();
			return true;
		}
		
		private void ChangeMove()
		{
			if (move == MoveOrder.FIRST)
				move = MoveOrder.SECOND;
			else
				move = MoveOrder.FIRST;
		}
	}
}
