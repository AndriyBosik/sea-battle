/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

using Config;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of ShopBullet.
	/// </summary>
	public abstract class BulletPack: Bullet
	{
		private List<BulletPackInGun> BulletPackInGuns
		{
			get
			{
				return Database.bulletPackInGuns
					.Where(bulletPackInGun => bulletPackInGun.BulletPack == this)
					.ToList();
			}
		}
		
		public List<Gun> Guns
		{
			get
			{
				return Database.bulletPackInGuns
					.Where(bulletPackInGun => bulletPackInGun.BulletPack == this)
					.Select(bulletPackInGun => bulletPackInGun.Gun)
					.ToList();
			}
		}
		
		public BulletPack(
			int radius,
			DamageKind damageKind,
			int costByOne,
			int damage): base(radius, damageKind, costByOne, damage)
		{
			Database.bulletPacks.Add(this);
		}
		
		protected abstract List<CellToDestroy> GetCellsToDestroy(Bonus bonus, Point point, Direction direction);
		
		public static int GetTheCheapestPrice(DamageKind kind)
		{
			return Database.bulletPacks.Where(bp => bp.DamageKind == kind).Min(bp => bp.CostByOne);
		}
		
		public void Shot(
			Field field, Point point, Bonus bonus, Direction direction, ref int money, ref int opponentMoney)
		{
			var cells = GetCellsToDestroy(bonus, point, direction);
			foreach (var cell in cells)
			{
				if (!field.IsInsideField(cell.Point.X, cell.Point.Y))
					continue;
				var cellDrawer = field.GetElement(cell.Point.X, cell.Point.Y);
				var damage = Math.Max(0, Damage - cell.Defense);
				cellDrawer.GetDamage(damage, ref money, ref opponentMoney);
				cellDrawer.Uncover();
			}
		}
		
		public override bool Equals(object obj)
		{
			var other = obj as BulletPack;
			if (other == null)
				return false;
			return  this.Radius == other.Radius &&
					this.DamageKind == other.DamageKind &&
					this.CostByOne == other.CostByOne &&
					this.Damage == other.Damage;
		}
		
		protected class CellToDestroy
		{
			public Point Point
			{ get; private set; }
			
			public int Defense
			{ get; private set; }
			
			public CellToDestroy(Point point, int defense)
			{
				Point = point;
				Defense = defense;
			}
		}

	}
}
