/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 22:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Config;

namespace Entities
{
	/// <summary>
	/// Description of BaseBomb.
	/// </summary>
	public class ShopItem: Cell
	{
		public int MaxCount
		{ get; protected set; }
		
		public int BuyedCount
		{ get; private set; }
		
		public int CostByOne
		{ get; protected set; }
		
		public int Damage
		{ get; protected set; }

		public ShopItem(int costByOne, int damage, string icon, int maxCount = 1)
		{
			BuyedCount = 0;
			MaxCount = maxCount;
			CostByOne = costByOne;
			Damage = damage;
			this.icon = icon;
			Init(icon);
		}
		
		public bool MaxCountBuyed()
		{
			return BuyedCount == MaxCount;
		}
		
		public bool TryBuy()
		{
			if (BuyedCount < MaxCount)
			{
				BuyedCount++;
				return true;
			}
			return false;
		}
	}
}
