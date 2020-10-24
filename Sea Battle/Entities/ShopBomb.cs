/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Config;

namespace Entities
{
	/// <summary>
	/// Description of ShopBomb.
	/// </summary>
	public class ShopBomb: Core
	{
		
		public int DeactivationPrice
		{
			get;
			private set;
		}
		
		public ShopBomb(
			int radius,
			int costByOne,
			int damage,
			int deactivationPrice,
			string icon): base(radius, costByOne, damage, icon)
		{
			DeactivationPrice = deactivationPrice;
		}
		
		#region Equals implementation
		public override bool Equals(object obj)
		{
			ShopBomb other = obj as ShopBomb;
			if (other == null)
				return false;
			return  this.icon == other.icon;
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
					"Deactivation Price: " + DeactivationPrice + "\n" +
					"Cost: " + CostByOne + "\n";
		}
	}
}
