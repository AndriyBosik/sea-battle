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
using GameObjects;

namespace GameParts
{
	/// <summary>
	/// Description of Bomb.
	/// </summary>
	public class Bomb: Cell
	{
		private PlayerData player;
		
		private Point point;
		private int radius;
		private int cost;
		private int damage;
		private int deactivationPrice;
		
		public Bomb(
			PlayerData player,
			int radius = 1,
			int cost = 10,
			int damage = 10,
			int deactivationPrice = 20,
			string icon = Images.BOMB)
		{
			Init(icon);
			this.radius = radius;
			cost = this.cost;
			damage = this.damage;
			this.player = player;
			this.deactivationPrice = deactivationPrice;
		}
		
		public void Move(int x, int y)
		{
			this.point = new Point(x, y);
		}
		
		public void MakeExplosion()
		{
			MakeExplosion(point.X, point.Y);
		}
		
		private void MakeExplosion(int x, int y)
		{
			// Recursive explosion
		}
		
		public void Deactivate()
		{
			player.Money -= cost;
		}
	}
}
