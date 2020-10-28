/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/12/2020
 * Time: 17:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of ShopGunGenerator.
	/// </summary>
	public class GunProcessor
	{
		public static Gun GenerateGun(GunKind kind)
		{
			string icon = kind.ToString();
			switch (kind)
			{
				case GunKind.SmallGun:
					return new Gun(200, new Bonus{Radius = 0, Damage = 10}, DamageKind.SPLASH, icon);
				case GunKind.MediumGun:
					return new Gun(300, new Bonus{Radius = 2, Damage = 20}, DamageKind.LINEAR, icon);
				case GunKind.LargeGun:
					return new Gun(500, new Bonus{Radius = 1, Damage = 5}, DamageKind.SPLASH, icon);
				default:
					return null;
			}
		}
		
		public static GunKind? GetKind(Gun gun)
		{
			foreach (GunKind kind in (GunKind[])Enum.GetValues(typeof(GunKind)))
				if (gun.Equals(GunProcessor.GenerateGun(kind)))
					return kind;
			return null;
		}
	}
}
