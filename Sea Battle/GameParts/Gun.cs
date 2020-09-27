/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 26.09.2020
 * Time: 18:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

using GameObjects;

using Config;

namespace GameParts
{
	/// <summary>
	/// Description of Gun.
	/// </summary>
	public class Gun
	{
		public static List<Gun> guns = new List<Gun>();
		
		private static int counter = 0;
		private int id;
		private int playerId;
		
		private int price;
		private int durability; // Percentage value(default = 100)
		private int consumption;
		private int radius;
		private string image;
		
		public List<Bullet> Bullets
		{
			get
			{
				return Bullet.bullets.Where(bullet => bullet.Gun == this).ToList();
			}
		}
		
		public int Id
		{
			get { return id; }
		}
		
		public PlayerData Player
		{
			get { return PlayerData.players.Where(player => player.Id == playerId).FirstOrDefault(); }
			set { playerId = value.Id; }
		}
		
		public Gun(
			PlayerData player,
			int radius = 1,
			int consumption = 1,
			string image = Images.SMALL_GUN)
		{
			counter++;
			id = counter;
			
			Player = player;
			
			this.durability = 100;
			this.consumption = consumption;
			this.radius = radius;
			this.image = image;
		}
		
		public void TryShot(int x, int y, Field field, Bullet bullet, Direction direction)
		{
			if (bullet.GunDamage > this.durability)
			{
				return;
			}
			durability -= bullet.GunDamage;
			durability -= 5;
			
			Shot(x, y, direction, field);
		}
		
		private void Shot(int x, int y, Direction direction, Field field)
		{
			Bullet bullet = GetBullet();
			if (bullet == null)
			{
				return;
			}
			bullet.Count -= consumption;
			bullet.Shot(x, y, direction, field);
		}
		
		private Bullet GetBullet()
		{
			foreach (var bullet in Bullets)
			{
				if (bullet.Count >= consumption)
				{
					return bullet;
				}
			}
			return null;
		}
		
		public void Sell()
		{
			int sellPrice = CalculateSellPrice();
			Player.Money += sellPrice;
		}
		
		public int CalculateSellPrice()
		{
			return (int)((durability/100.0)*price);
		}
	}
}
