/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Config;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of ShopBullet.
	/// </summary>
	public class ShopBullet: ShopItem
	{
		public DamageKind DamageKind
		{
			get;
			private set;
		}
		
		public int Stability
		{
			get;
			private set;
		}
		
		public int Count
		{
			get;
			set;
		}
		
		public ShopBullet(
			int stability,
			int count,
			DamageKind damageKind,
			int cost,
			int damage,
			string icon = Images.SMALL_BULLET): base(cost, damage, icon)
		{
			Stability = stability;
			Count = count;
			DamageKind = damageKind;
		}
	}
}
