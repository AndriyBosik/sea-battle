/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

using Config;

namespace Entities
{
	/// <summary>
	/// Description of Bullet.
	/// </summary>
	public class Bullet: ShopBullet
	{	
		
		public Bullet(
			int stability,
			int count,
			DamageKind damageKind,
			int costByOne,
			int damage,
			string icon = Images.SMALL_BULLET): base(stability, count, damageKind, costByOne, damage, icon)
		{
			
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
