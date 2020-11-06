/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

using GameObjects;

using Config;

namespace Entities
{
	/// <summary>
	/// Description of Gun.
	/// </summary>
	public class Gun: ShopItem
	{

		private List<BulletPackInGun> BulletPackInGuns
		{
			get
			{
				return Database.bulletPackInGuns.Where(bulletPackInGun => bulletPackInGun.Gun == this).ToList();
			}
		}
		
		public List<BulletPack> BulletPacks
		{
			get
			{
				return Database.bulletPackInGuns
					.Where(bulletPackInGun => bulletPackInGun.Gun == this)
					.Select(bulletPackInGun => bulletPackInGun.BulletPack)
					.ToList();
			}
		}
		
		public Bonus Bonus
		{ get; private set; }
		
		public DamageKind DamageKind
		{
			get;
			private set;
		}
		
		public Gun(int costByOne, Bonus bonus, DamageKind damageKind, string icon): base(costByOne, 0, icon, true)
		{
			this.Bonus = bonus;
			DamageKind = damageKind;
			
			Database.guns.Add(this);
		}
		
		public int Shot(Field field, BulletPack bullet, Point point, Direction direction, ref int opponentMoney)
		{
			int money = 0;
			BulletPackInGun.MakeShot(this, bullet);
			bullet.Shot(field, point, Bonus, direction, ref money, ref opponentMoney);
			return money;
		}
		
		public override bool Equals(object obj)
		{
			var shopGun = (Gun)obj;
			if (shopGun == null)
			{
				return false;
			}
			return  this.CostByOne == shopGun.CostByOne &&
					this.DamageKind == shopGun.DamageKind;
		}
		
		public override string ToString()
		{
			return  "Cost: " + CostByOne + "\n" +
					"Kind of damage: " + DamageKindExtensions.ToString(DamageKind) + "\n" +
					"Radius: " + Bonus.Radius + "\n" +
					"Damage: " + Bonus.Damage + "\n";
		}
 
	}
}
