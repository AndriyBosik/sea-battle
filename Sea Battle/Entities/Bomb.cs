/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 22:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;

using Config;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of Bomb.
	/// </summary>
	public class Bomb: ShopBomb
	{
		private Guid deckId;
		private Point point;
		private readonly CellStatus status;
		
		public Deck Deck
		{
			get { return Database.decks.Where(deck => deck.ID == deckId).FirstOrDefault(); }
			set { deckId = value.ID; }
		}
		
		public Bomb(Point point, int radius, int cost, int damage, int deactivationPrice, string icon):
			base(radius, cost, damage, deactivationPrice, icon)
		{
			status = CellStatus.BOMB;
			this.point = point;
			
			Database.bombs.Add(this);
		}
		
		public void Move(Point point)
		{
			this.point = point;
		}
		
		public void Explose()
		{
			// Make explosion
		}
		
		public void Deactivate()
		{
			// Take money
			Init(Images.EMPTY_CELL);
		}
	}
}
