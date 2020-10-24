﻿/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/18/2020
 * Time: 17:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of ShopLinearBullets.
	/// </summary>
	public class LinearBullets: BulletPack
	{
		public LinearBullets(
			int radius,
			int count,
			int costByOne,
			int damage,
			string icon): base(radius, count, DamageKind.LINEAR, costByOne, damage, icon) {}
		
		private void GetAdditors(ref int x, ref int y, Direction direction)
		{
			if (direction == Direction.UP)
			{
				x = -1; y = 0;
			}
			else if (direction == Direction.DOWN)
			{
				x = 1; y = 0;
			}
			else if (direction == Direction.LEFT)
			{
				x = 0; y = -1;
			}
			else if (direction == Direction.RIGHT)
			{
				x = 0; y = 1;
			}
		}
		
		public override void Shot(Field field, Point point, Direction direction)
		{
			int x = 0;
			int y = 0;
			GetAdditors(ref x, ref y, direction);
			for (int i = 0; i < Radius; i++)
			{
				if (!field.IsInsideField(point.X + x*i, point.Y + y*i))
					continue;
				//int damage = Damage - (i - point.X)*10;
				int damage = Damage;
				if (field.cells[point.X + x*i][point.Y + y*i] is Deck)
				{
					var deck = (Deck)field.cells[point.X + x*i][point.Y + y*i];
					deck.Hurt(damage);
				}
				field.cells[point.X + x*i][point.Y + y*i].Uncover();
				field.Repaint(point.X + x*i, point.Y + y*i);
			}
		}
		
		public override void Sell()
		{
			
		}
		
		public override int QuantifySellPrice()
		{
			return 0;
		}
		
		public override string ToString()
		{
			return  "Radius: " + Radius + "\n" +
					"Count: " + Count + "\n" +
					"Cost: " + CostByOne + "\n" +
					"Damage: " + Damage;
		}
	}
}
