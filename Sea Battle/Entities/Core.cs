/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/18/2020
 * Time: 16:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

using Config;

namespace Entities
{
	/// <summary>
	/// Description of ShopCore.
	/// </summary>
	public class Core: BulletPack
	{
		private const int COUNT_IN_PACK = 1;
		
		public int Radius
		{
			get;
			private set;
		}
		
		public Core(
			int radius,
			int costByOne,
			int damage,
			string icon): base(radius, COUNT_IN_PACK, DamageKind.SPLASH, costByOne, damage, icon)
		{
			Radius = radius;
		}
		
		public override void Shot(Field field, Point point, Direction direction)
		{
			var bomb = new Bomb(point, Radius, CostByOne, Damage, 0, icon);
			bomb.Explose(field);
		}
		
		public override void Sell()
		{
			
		}
		
		public override int QuantifySellPrice()
		{
			return 0;
		}
		
		#region Equals implementation
		public override bool Equals(object obj)
		{
			Core other = obj as Core;
			if (other == null)
				return false;
			return  this.icon == other.icon;
		}

		public static bool operator==(Core lhs, Core rhs) {
			if (ReferenceEquals(lhs, rhs))
				return true;
			if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
				return false;
			return lhs.Equals(rhs);
		}

		public static bool operator !=(Core lhs, Core rhs) {
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
