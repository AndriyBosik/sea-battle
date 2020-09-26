/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 26.09.2020
 * Time: 18:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace GameParts
{
	/// <summary>
	/// Description of Bullet.
	/// </summary>
	public class Bullet
	{
		private Gun gun;
		
		private readonly int damage;
		private readonly int cost;
		private int stability;
		private int count;
		
		public Bullet(Gun gun, int damage = 10, int cost = 1, int stability = 1, int count = 32)
		{
			this.gun = gun;
			
			this.damage = damage;
			this.cost = cost;
			this.stability = stability;
			this.count = count;
		}
	}
}
