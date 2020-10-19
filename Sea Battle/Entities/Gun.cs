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
		
		public int Deterioration
		{
			get;
			set;
		}
		
		public DamageKind DamageKind
		{
			get;
			private set;
		}
		
		public Gun(int costByOne, int deterioration, DamageKind damageKind, string icon): base(costByOne, 0, icon)
		{
			DamageKind = damageKind;
			Deterioration = deterioration;
			
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
		
		public override bool Equals(object obj)
		{
			var shopGun = (Gun)obj;
			if (shopGun == null)
			{
				return false;
			}
			return  this.CostByOne == shopGun.CostByOne &&
					this.Deterioration == shopGun.Deterioration &&
					this.DamageKind == shopGun.DamageKind;
		}
		
		public override string ToString()
		{
			return  "Deterioration: " + Deterioration + "\n" +
					"Cost: " + CostByOne + "\n" +
					"King of damage: " + DamageKindExtensions.ToString(DamageKind) + "\n";
		}
 
	}
}
