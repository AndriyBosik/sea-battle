/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Entities
{
	/// <summary>
	/// Description of Gun.
	/// </summary>
	public class ShopGun: ShopItem
	{

		private int Deterioration
		{
			get;
			set;
		}
		
		public ShopGun(int costByOne, int deterioration, string icon): base(costByOne, 0, icon)
		{
			Deterioration = deterioration;
		}
	}
}
