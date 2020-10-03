/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 26.09.2020
 * Time: 18:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

using GameObjects;

namespace GameParts
{
	/// <summary>
	/// Description of Bullet.
	/// </summary>
	public class Bullet
	{
		public static List<Bullet> bullets = new List<Bullet>();
		
		private Gun gun;
		
		private int playerId;
		
		private int gunId;
		private readonly int gunDamage;
		private int stability;
		private int totalCount;
		private int currentCount;
		
		public Gun Gun
		{
			get
			{
				return Gun.guns.Where(gun => gun.Id == gunId).FirstOrDefault();
			}
			set
			{
				gunId = value.Id;
			}
		}
		
		public int Count
		{
			get { return currentCount; }
			set { currentCount = value; }
		}
		
		public int GunDamage
		{
			get
			{
				return gunDamage;
			}
		}
		
		public PlayerData Player
		{
			get { return PlayerData.players.Where(player => player.Id == playerId).FirstOrDefault(); }
			set { playerId = value.Id; }
		}
		
		public Bullet(
			Gun gun,
		    int damage = 10,
		    DamageKind damageKind = DamageKind.LINEAR,
		    int cost = 1,
		    int gunDamage = 10,
		    int stability = 1,
		    int count = 32)
		{
			this.gun = gun;
			
			this.damageKind = damageKind;
			this.damage = damage;
			this.gunDamage = gunDamage;
			this.cost = cost;
			this.stability = stability;
			totalCount = currentCount = count;
		}
		
		public void Shot(int x, int y, Direction direction, Field field)
		{
			if (damageKind == DamageKind.LINEAR)
			{
				MakeLinearShot(x, y, direction, field);
			}
			else
			{
				MakeSplash(x, y, field);
			}
		}
		
		private void MakeLinearShot(int x, int y, Direction direction, Field field)
		{
			// The operations to perform Linear shot
		}
		
		private void MakeSplash(int x, int y, Field field)
		{
			// The operations to perform splash shot
		}
		
		public void Sell()
		{
			int sellPrice = QuantifySellPrice();
			Player.Money += sellPrice;
		}
		
		private int QuantifySellPrice()
		{
			return (int)(currentCount*1.0/totalCount*cost);
		}
	}
}
