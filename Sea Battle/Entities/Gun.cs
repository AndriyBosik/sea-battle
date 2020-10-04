/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/04/2020
 * Time: 18:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of Gun.
	/// </summary>
	public class Gun: ShopGun
	{
//		public List<BulletPack> PossibleBulletPacks
//		{
//			get { return Database.bulletPacks.Where(bulletPack => bulletPack.DamageKind == this.DamageKind).ToList(); }
//		}
		
		private List<BulletPackInGun> BulletPackInGuns
		{
			get
			{
				return Database.bulletPackInGuns.Where(bulletPackInGun => bulletPackInGun.BulletPack == this).ToList();
			}
		}
		
		public List<BulletPack> BulletPacks
		{
			get
			{
				return Database.bulletPackInGuns
					.Where(bulletPackInGun => bulletPackInGun.BulletPack == this)
					.Select(bulletPackInGun => bulletPackInGun.BulletPack)
					.ToList();
			}
		}
		
		public Gun(int costByOne, int deterioration, string icon): base(costByOne, deterioration, icon)
		{
			Database.guns.Add(this);
		}
		
		public void TryShot(BulletPack bullet, Point point)
		{
			
		}
		
		private void Shot(BulletPack bullet)
		{
			
		}
		
		public void Sell()
		{
			
		}
		
		private int QuantifySellPrice()
		{
			return 0;
		}
	}
}
