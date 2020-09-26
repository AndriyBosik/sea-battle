/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 26.09.2020
 * Time: 18:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Config;

namespace GameParts
{
	/// <summary>
	/// Description of Gun.
	/// </summary>
	public class Gun
	{
		private Field field;
		
		private int damage;
		private int orientation;
		private int distance;
		private string image;
		
		public Gun(
			Field field,
			int damage = 10,
			int orientation = Gameplay.HORIZONTAL_ORIENTATION,
			int distance = 1,
			string image = Images.SMALL_GUN)
		{
			this.field = field;
			this.damage = damage;
			this.orientation = orientation;
			this.distance = distance;
			this.image = image;
		}
		
		public void MakeShot()
		{
			
		}
	}
}
