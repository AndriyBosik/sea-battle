/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/18/2020
 * Time: 16:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using GameObjects;

using Config;

namespace Entities
{
	/// <summary>
	/// Description of ShopCore.
	/// </summary>
	public class Core: BulletPack
	{
		public Core(
			int radius,
			int costByOne,
			int damage): base(radius, DamageKind.SPLASH, costByOne, damage)
		{}
		
		protected override List<CellToDestroy> GetCellsToDestroy(Bonus bonus, Point point, Direction direction)
		{
			var list = new List<CellToDestroy>();
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
					var defense = (Math.Abs(i) + Math.Abs(j))*10;
					list.Add(new CellToDestroy(new Point(x, y), defense));
				}
			}
			return list;
		}
		
		#region Equals implementation
		public override bool Equals(object obj)
		{
			Core other = obj as Core;
			if (other == null)
				return false;
			return  this.Radius == other.Radius &&
					this.DamageKind == other.DamageKind &&
					this.CostByOne == other.CostByOne &&
					this.Damage == other.Damage;
		}

		public static bool operator==(Core lhs, Core rhs) {
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Core lhs, Core rhs) {
			return !(lhs == rhs);
		}

		#endregion
		
		public override string ToString()
		{
			return  "Radius: " + Radius + "\n" +
					"Damage: " + Damage + "\n" +
					"Cost: " + CostByOne + "\n";
		}
	}
}
