/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/18/2020
 * Time: 17:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
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
			int costByOne,
			int damage): base(radius, DamageKind.LINEAR, costByOne, damage)
		{}
		
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
		
		protected override List<CellToDestroy> GetCellsToDestroy(Bonus bonus, Point point, Direction direction)
		{
			int x = 0;
			int y = 0;
			GetAdditors(ref x, ref y, direction);
			var list = new List<CellToDestroy>();
			for (int i = 0; i < Radius + bonus.Radius; i++)
			{
				int defense = 0;
				list.Add(new CellToDestroy(new Point(point.X + x*i, point.Y + y*i), defense));
			}
			return list;
		}
		
		public override string ToString()
		{
			return  "Radius: " + Radius + "\n" +
					"Cost: " + CostByOne + "\n" +
					"Damage: " + Damage + "\n";
		}
	}
}
