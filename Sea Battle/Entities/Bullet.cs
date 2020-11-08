/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/06/2020
 * Time: 18:27
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of Bullet.
	/// </summary>
	public abstract class Bullet: ShopItem
	{
		public int Radius
		{ get; protected set; }
		
		public DamageKind DamageKind
		{ get; private set; }
		
		public Bullet(int radius, DamageKind damageKind, int costByOne, int damage):
			base(costByOne, damage)
		{
			Radius = radius;
			DamageKind = damageKind;
		}
	}
}
