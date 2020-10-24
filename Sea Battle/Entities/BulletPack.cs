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
	public abstract class BulletPack: ShopItem
	{
		
		private List<BulletPackInGun> BulletPackInGuns
		{
			get
			{
				return Database.bulletPackInGuns.Where(bulletPackInGun => bulletPackInGun.BulletPack == this).ToList();
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
		
		public DamageKind DamageKind
		{
			get;
			private set;
		}
		
		public int Radius
		{
			get;
			private set;
		}
		
		public int Count
		{
			get;
			set;
		}
		
		public BulletPack(
			int radius,
			int count,
			DamageKind damageKind,
			int costByOne,
			int damage,
			string icon): base(costByOne, damage, icon, Gameplay.INFINITY)
		{
			Radius = radius;
			Count = count;
			DamageKind = damageKind;
			
			Database.bulletPacks.Add(this);
		}
		
		public abstract void Shot(Field field, Point point, Direction direction);
		
		public abstract void Sell();
		
		public abstract int QuantifySellPrice();

	}
}
