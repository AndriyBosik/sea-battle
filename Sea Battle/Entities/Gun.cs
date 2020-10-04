/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/04/2020
 * Time: 18:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of Gun.
	/// </summary>
	public class Gun: ShopGun
	{
		public Gun(int costByOne, int deterioration, string icon): base(costByOne, deterioration, icon)
		{
			
		}
		
		public void TryShot(Bullet bullet, Point point)
		{
			
		}
		
		private void Shot(Bullet bullet)
		{
			
		}
		
		public void Sell()
		{
			
		}
		
		private int QuantifySellPrice()
		{
			return 0;
		}
	}
}
