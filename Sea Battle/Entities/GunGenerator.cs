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
	public class GunGenerator
	{
		public static Gun GenerateGun(GunKind kind)
		{
			string icon = kind.ToString();
			switch (kind)
			{
				case GunKind.SmallGun:
					return new Gun(200, 100, DamageKind.SPLASH, icon);
				case GunKind.MediumGun:
					return new Gun(300, 110, DamageKind.LINEAR, icon);
				case GunKind.LargeGun:
					return new Gun(500, 200, DamageKind.SPLASH, icon);
				default:
					return null;
			}
		}
	}
}
