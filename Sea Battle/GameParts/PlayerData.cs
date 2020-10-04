/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/27/2020
 * Time: 15:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

using Entities;

namespace GameParts
{
	/// <summary>
	/// Description of PlayerData.
	/// </summary>
	public class PlayerData
	{
		public static List<PlayerData> players = new List<PlayerData>();
		
		private static int counter = 0;
		int id;
		
		private Field field;
		private int money;
		private int healthPoints;
		
		public Field Field
		{
			get { return field; }
		}
		
		public int Id
		{
			get { return id; }
		}
		
		public int Money
		{
			get { return money; }
			set { money = value; }
		}
		
//		public List<Gun> Guns
//		{
//			get
//			{
//				//return Database.guns.Where(gun => gun.Player == this).ToList();
//			}
//		}
		
//		public List<Bullet> Bullets
//		{
//			get
//			{
//				//return Bullet.bullets.Where(bullet=> bullet.Player == this).ToList();
//			}
//		}
		
		public PlayerData(Field field, int money = 1000, int healthPoints = 1000)
		{
			counter++;
			id = counter;
			
			this.field = field;
			this.money = money;
			this.healthPoints = healthPoints;
		}
		
		public void HealDeck(Deck deck)
		{
			deck.Heal(Math.Min(healthPoints, deck.TotalHealth - deck.CurrentHealth));
		}
		
	}
}
