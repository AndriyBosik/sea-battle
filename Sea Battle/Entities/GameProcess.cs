/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/27/2020
 * Time: 19:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of GameProcess.
	/// </summary>
	public class GameProcess
	{
		private List<Move> moves;
		
		private Field firstField;
		private Field secondField;
		
		public GameProcess(Field firstField, Field secondField)
		{
			this.firstField = firstField;
			this.secondField = secondField;
			
			moves = new List<Move>();
				
			Database.gameProcess.Add(this);
		}
		
		public void AddMove(MoveOrder moveOrder, GunKind gun, BombKind bomb, Point point)
		{
			moves.Add(new Move{MoveOrder = moveOrder, Gun = gun, Bomb = bomb, Point = point});
		}
		
		private class Move
		{
			public MoveOrder MoveOrder
			{ get; set; }
			
			public GunKind Gun
			{ get; set; }
			
			public BombKind Bomb
			{ get; set; }
			
			public Point Point
			{ get; set; }
		}
	}
}
