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
		
		public Deck Deck
		{
			get { return Database.decks.Where(deck => deck.ID == deckId).FirstOrDefault(); }
			set { deckId = value.ID; }
		}
		
		public Bomb(Point point, int radius, int cost, int damage, int deactivationPrice, string icon):
			base(radius, cost, damage, deactivationPrice, icon)
		{
			Status = CellStatus.BOMB;
			this.point = point;
			
			Database.bombs.Add(this);
		}
		
		public override void Uncover()
		{
			this.icon = Images.EMPTY_CELL;
			base.Uncover();
		}
		
		public void Move(Point point)
		{
			this.point = point;
		}
		
		public void Explose(Field field)
		{
			Cell[][] cells = field.cells;
			int n = cells.Length;
			int m = cells[0].Length;
			for (int i = -Radius + 1; i <= Radius - 1; i++)
			{
				int leftSide = -(Radius - 1 - Math.Abs(i));
				int rightSide = -leftSide;
				for (int j = leftSide; j <= rightSide; j++)
				{
					int x = point.X + i;
					int y = point.Y + j;
					if (!field.IsInsideField(x, y))
						continue;
					var damage = Damage - (Math.Abs(i) + Math.Abs(j))*10;
					if (cells[x][y] is Deck)
					{
						var deck = (Deck)cells[x][y];
						deck.Hurt(damage);
						deck.Refresh();
					}
					field.cells[x][y].Uncover();
				}
			}
		}
		
		public void Deactivate()
		{
			// Take money
			Init(Images.EMPTY_CELL);
		}
		
		public override bool Equals(object obj)
		{
			Bomb other = obj as Bomb;
			if (other == null)
				return false;
			return  this.icon == other.icon &&
					this.CostByOne == other.CostByOne &&
					this.Damage == other.Damage &&
					this.DamageKind == other.DamageKind &&
					this.Radius == other.Radius &&
					this.DeactivationPrice == other.DeactivationPrice;
		}
	}
}
