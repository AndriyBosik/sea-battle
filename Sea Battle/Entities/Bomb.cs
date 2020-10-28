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
		private Field field;
		private bool explosed;
		
		public Deck Deck
		{
			get { return Database.decks.Where(deck => deck.ID == deckId).FirstOrDefault(); }
			set { deckId = value.ID; }
		}
		
		public Bomb(Field field, Point point, int radius, int cost, int damage, int deactivationPrice, string icon):
			base(radius, cost, damage, deactivationPrice, icon)
		{
			Status = CellStatus.BOMB;
			this.point = point;
			this.field = field;
			this.explosed = false;
			
			Database.bombs.Add(this);
		}
		
		public override void Uncover()
		{
			this.icon = Images.EMPTY_CELL;
			base.Uncover();
		}
		
		public int Explose(Bonus bonus, ref int opponentMoney)
		{
			int money = 0;
			Cell[][] cells = field.cells;
			int n = cells.Length;
			int m = cells[0].Length;
			explosed = true;
			int newRadius = Radius + bonus.Radius;
			int newDamage = Damage + bonus.Damage;
			for (int i = -newRadius + 1; i <= newRadius - 1; i++)
			{
				int leftSide = -(newRadius - 1 - Math.Abs(i));
				int rightSide = -leftSide;
				for (int j = leftSide; j <= rightSide; j++)
				{
					int x = point.X + i;
					int y = point.Y + j;
					if (!field.IsInsideField(x, y))
						continue;
					var damage = newDamage - (Math.Abs(i) + Math.Abs(j))*10;
					if (cells[x][y] is Deck)
					{
						var deck = (Deck)cells[x][y];
						money += deck.Hurt(damage);
						deck.Refresh();
					}
					else if (cells[x][y] is Bomb)
					{
						var bomb = (Bomb)cells[x][y];
						if (!bomb.explosed)
							opponentMoney += bomb.Explose(new Bonus{Radius = 0, Damage = 0}, ref money);
					}
					field.cells[x][y].Uncover();
				}
			}
			return money;
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
