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
	public class ShopBomb: ShopItem
	{
		public int Radius
		{
			get;
			private set;
		}
		
		public int DeactivationPrice
		{
			get;
			private set;
		}
		
		public ShopBomb(
			int radius, int cost, int damage, int deactivationPrice, string icon = Images.BOMB): base(cost, damage, icon)
		{
			Radius = radius;
			DeactivationPrice = deactivationPrice;
		}
	}
}
