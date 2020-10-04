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

namespace Entities
{
	/// <summary>
	/// Description of BulletPackInGun.
	/// </summary>
	public class BulletPackInGun: Base
	{	
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
	}
}
