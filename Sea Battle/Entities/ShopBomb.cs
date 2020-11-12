/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of ShopBomb.
	/// </summary>
	public class ShopBomb: Bullet
	{
		public ShopBomb(
			int radius,
			int costByOne,
			int damage): base(radius, DamageKind.SPLASH, costByOne, damage)
		{}
		
		#region Equals implementation
		public override bool Equals(object obj)
		{
			ShopBomb other = obj as ShopBomb;
			if (other == null)
				return false;
			return  this.Radius == other.Radius &&
					this.Damage == other.Damage;
		}

		public static bool operator==(ShopBomb lhs, ShopBomb rhs) {
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}

		public static bool operator !=(ShopBomb lhs, ShopBomb rhs) {
			return !(lhs == rhs);
		}

		#endregion
		
		public override string ToString()
		{
			return  "Radius: " + Radius + "\n" +
					"Damage: " + Damage + "\n" +
					"Cost: " + CostByOne + "\n";
		}
	}
	
}
