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
		
		public int CostByOne
		{
			get;
			private set;
		}
		
		public int Damage
		{
			get;
			private set;
		}

		public ShopItem(int costByOne, int damage, string icon)
		{
			CostByOne = costByOne;
			Damage = damage;
			this.icon = icon;
			Init(icon);
		}
	}
}
