/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:28
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
	/// Description of Bullet.
	/// </summary>
	public class BulletPack: ShopBulletPack
	{	
//		public List<Gun> PossibleGuns
//		{
//			get { return Database.guns.Where(gun => gun.DamageKind == this.DamageKind).ToList(); }
//		}
		
		private List<BulletPackInGun> BulletPackInGuns
		{
			get
			{
				return Database.bulletPackInGuns.Where(bulletPackInGun => bulletPackInGun.Gun == this).ToList();
			}
		}
		
		public List<Gun> Guns
		{
			get
			{
				return Database.bulletPackInGuns
					.Where(bulletPackInGun => bulletPackInGun.Gun == this)
					.Select(bulletPackInGun => bulletPackInGun.Gun)
					.ToList();
			}
		}
		
		public BulletPack(
			int stability,
			int count,
			DamageKind damageKind,
			int costByOne,
			int damage,
			string icon = Images.SMALL_BULLET): base(stability, count, damageKind, costByOne, damage, icon)
		{
			Database.bulletPacks.Add(this);
		}
		
		public void Shot(Point point)
		{
			
		}
		
		public void Shot(Point point, Direction direction)
		{
			
		}
		
		public void Sell()
		{
			
		}
		
		public int QuantifySellPrice()
		{
			return (int)(CostByOne*Count);
		}
	}
}
