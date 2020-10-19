/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/18/2020
 * Time: 17:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of ShopLinearBullets.
	/// </summary>
	public class LinearBullets: BulletPack
	{
		public LinearBullets(
			int radius,
			int count,
			int costByOne,
			int damage,
			string icon): base(radius, count, DamageKind.LINEAR, costByOne, damage, icon) {}
		
		public override void Shot(Point point, Direction direction)
		{
			
		}
		
		public override void Sell()
		{
			
		}
		
		public override int QuantifySellPrice()
		{
			return 0;
		}
		
		public override string ToString()
		{
			return  "Radius: " + Radius + "\n" +
					"Count: " + Count + "\n" +
					"Cost: " + CostByOne + "\n" +
					"Damage: " + Damage;
		}
	}
}
