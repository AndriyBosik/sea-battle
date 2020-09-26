/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/22/2020
 * Time: 00:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Config;

namespace GameParts
{
	/// <summary>
	/// Description of Bomb.
	/// </summary>
	public class Bomb: Cell
	{
		private Field field;
		
		private int radius;
		private int cost;
		private int damage;
		private int deactivationPrice;
		
		public Bomb(
			Field field,
			int x,
			int y,
			int radius = 1,
			int cost = 10,
			int damage = 10,
			int deactivationPrice = 20,
			string icon = Images.BOMB)
		{
			Init(x, y, icon);
			this.radius = radius;
			cost = this.cost;
			damage = this.damage;
			this.field = field;
			this.deactivationPrice = deactivationPrice;
		}
		
		public void MakeExplosion()
		{
			
		}
	}
}
