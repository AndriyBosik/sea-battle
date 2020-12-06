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
		public const string FIRST_PLAYER_WON = "firstPlayerWon";
		public const string SECOND_PLAYER_WON = "secondPlayerWon";
		
		private const string CONGRATULATION_MESSAGE = " has won!!!";
		
		private Player firstPlayer;
		private Player secondPlayer;
		private MoveOrder move;
		private Messanger messanger;
		private string winner;
		
		public bool IsEnded
		{ get; private set; }
		
		public Game(Player firstPlayer, Player secondPlayer, Messanger messanger)
		{
			this.firstPlayer = firstPlayer;
			this.secondPlayer = secondPlayer;
			this.move = MoveOrder.FIRST;
			this.messanger = messanger;
			this.winner = "";
			this.IsEnded = false;
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
			if (field != GetCurrentField() || !GetCurrentPlayer().ReadyToShot())
				return false;
			GetAnotherPlayer().Money += GetCurrentPlayer().Shot(field, row, column);
			ChangeMove();
			if (GetCurrentPlayer().Field.AreAllShipsDestroyed() || !GetCurrentPlayer().HasBullets())
			{
				FindWinner(GetAnotherPlayer());
				messanger.CongratulatePlayer(GetAnotherPlayer().Name + CONGRATULATION_MESSAGE);
			}
			return true;
		}
		
		private void FindWinner(Player player)
		{
			IsEnded = true;
			winner = SECOND_PLAYER_WON;
			if (player == firstPlayer)
				winner = FIRST_PLAYER_WON;
		}
		
		public Player GetWinner()
		{
			if (!IsEnded)
				return null;
			if (winner.Equals(FIRST_PLAYER_WON))
				return firstPlayer;
			return secondPlayer;
		}
		
		public void PlayerGiveUp()
		{
			IsEnded = true;
			FindWinner(GetAnotherPlayer());
			messanger.CongratulatePlayer(GetAnotherPlayer().Name + CONGRATULATION_MESSAGE);
		}
		
		private void ChangeMove()
		{
			if (move == MoveOrder.FIRST)
				move = MoveOrder.SECOND;
			else
				move = MoveOrder.FIRST;
		}
		
		public interface Messanger
		{
			void CongratulatePlayer(string text);
		}
	}
}
