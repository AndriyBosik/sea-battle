/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 26.09.2020
 * Time: 18:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

using Config;

namespace GameParts
{
	/// <summary>
	/// Description of Gun.
	/// </summary>
	public class Gun
	{
		private static int counter = 0;
		private int id;
		
		private Field field;
		
		private int durability;
		private int consumption;
		private int radius;
		private string image;
		
		public Gun(
			Field field,
			int durability = 100,
			int radius = 1,
			int consumption = 1,
			string image = Images.SMALL_GUN)
		{
			counter++;
			id = counter;
			
			this.field = field;
			
			this.durability = durability;
			this.consumption = consumption;
			this.radius = radius;
			this.image = image;
		}
		
		public void MakeShot(int x, int y, Bullet bullet, string orientation = Gameplay.HORIZONTAL_ORIENTATION)
		{
			if (bullet.GunDamage > this.durability)
			{
				return;
			}
			durability -= bullet.GunDamage;
			// do some stuff
		}
	}
}
