/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

using Config;

namespace Entities
{
	/// <summary>
	/// Description of Gun.
	/// </summary>
	public class ShopGun: ShopItem
	{

		public int Deterioration
		{
			get;
			set;
		}
		
		public DamageKind DamageKind
		{
			get;
			private set;
		}
		
		public ShopGun(int costByOne, int deterioration, DamageKind damageKind, string icon): base(costByOne, 0, icon)
		{
			DamageKind = damageKind;
			Deterioration = deterioration;
		}
		
		public override string ToString()
		{
			return  "Deterioration: " + Deterioration + "\n" +
					"Cost: " + CostByOne + "\n" +
					"King of damage: " + DamageKindExtensions.ToString(DamageKind) + "\n";
		}
 
	}
}
