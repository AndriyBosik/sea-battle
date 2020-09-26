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
		
		public Bomb(Field field, int x, int y, int deactivationPrice, string icon = Images.BOMB)
		{
			Init(x, y, icon);
			radius = 1;
			cost = 10;
			damage = 10;
			this.field = field;
			this.deactivationPrice = deactivationPrice;
		}
		
		public void MakeExplosion()
		{
			
		}
	}
}
