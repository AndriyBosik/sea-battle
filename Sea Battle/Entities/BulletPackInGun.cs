/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/04/2020
 * Time: 18:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;

namespace Entities
{
	/// <summary>
	/// Description of BulletPackInGun.
	/// </summary>
	public class BulletPackInGun: Base
	{	
		public int Count
		{ get; private set; }
		
		private Guid bulletPackID;
		public BulletPack BulletPack
		{
			get { return Database.bulletPacks.Where(bulletPack => bulletPack.ID == bulletPackID).FirstOrDefault(); }
			set { bulletPackID = value.ID; }
		}
		
		private Guid gunID;
		public Gun Gun
		{
			get { return Database.guns.Where(gun => gun.ID == gunID).FirstOrDefault(); }
			set { gunID = value.ID; }
		}
		
		public BulletPackInGun(Gun gun, BulletPack bulletPack)
		{
			var bpig = Database.bulletPackInGuns
				.Where(item => item.Gun == gun && item.BulletPack.Equals(bulletPack)).FirstOrDefault();
			if (bpig == null)
			{
				this.Gun = gun;
				this.BulletPack = bulletPack;
				this.Count = 1;
				Database.bulletPackInGuns.Add(this);
			}
			else
			{
				bpig.Count++;
			}
		}
		
		public static int GetCount(Gun gun, BulletPack bulletPack)
		{
			var bpig = Database.bulletPackInGuns
				.Where(item => item.Gun == gun && item.BulletPack.Equals(bulletPack)).FirstOrDefault();
			if (bpig == null)
				return 0;
			return bpig.Count;
		}
		
		public static void MakeShot(Gun gun, BulletPack bulletPack)
		{
			var bpig = Database.bulletPackInGuns
				.Where(item => item.Gun == gun && item.BulletPack.Equals(bulletPack)).FirstOrDefault();
			if (bpig == null)
				return;
			bpig.Count--;
		}
	}
}
