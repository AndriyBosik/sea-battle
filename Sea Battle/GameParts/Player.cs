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

using Config;

namespace FieldEditorParts
{
	/// <summary>
	/// Description of PlayerData.
	/// </summary>
	public class Player
	{
		public static List<Player> players = new List<Player>();
		
		private static int counter = 0;
		int id;
		
		private Field field;
		
		public Field Field
		{
			get { return field; }
			set { field = value; }
		}
		
		public int Id
		{
			get { return id; }
		}
		
		public int Money
		{
			get; set;
		}
		
		public List<Gun> Guns
		{ get; private set; }
		
		public List<BulletPack> BulletPacks;
		
		public int HealthPoints
		{
			get; set;
		}
		
		public Player()
		{
			counter++;
			id = counter;
			this.Money = Gameplay.INITIAL_MONEY;
			this.HealthPoints = Gameplay.INITIAL_HEALTH_POINT;
			
			Guns = new List<Gun>();
			BulletPacks = new List<BulletPack>();
		}
		
		public void HealDeck(Deck deck)
		{
			int health = Math.Min(HealthPoints, deck.TotalHealth - deck.CurrentHealth);
			deck.Heal(health);
			HealthPoints -= health;
		}
		
	}
}
