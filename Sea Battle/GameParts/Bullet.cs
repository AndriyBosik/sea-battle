/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 26.09.2020
 * Time: 18:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

namespace GameParts
{
	/// <summary>
	/// Description of Bullet.
	/// </summary>
	public class Bullet
	{
		private Gun gun;
		
		private readonly int damage;
		private readonly int cost;
		private readonly DamageKind damageKind;
		private readonly int gunDamage;
		private int stability;
		private int count;
		
		public int GunDamage
		{
			get
			{
				return gunDamage;
			}
		}
		
		public Bullet(
			Gun gun,
		    int damage = 10,
		    DamageKind damageKind = DamageKind.LINEAR,
		    int cost = 1,
		    int gunDamage = 10,
		    int stability = 1,
		    int count = 32)
		{
			this.gun = gun;
			
			this.damageKind = damageKind;
			this.damage = damage;
			this.gunDamage = gunDamage;
			this.cost = cost;
			this.stability = stability;
			this.count = count;
		}
		
		public void Shot(int x, int y, Field field)
		{
			
		}
	}
}
