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
		}
		
		public int Id
		{
			get { return id; }
		}
		
		public int Money
		{
			get; set;
		}
		
		public int HealthPoints
		{
			get; set;
		}
		
		public Player(Field field)
		{
			counter++;
			id = counter;
			
			this.field = field;
			this.Money = Gameplay.INITIAL_MONEY;
			this.HealthPoints = Gameplay.INITIAL_HEALTH_POINT;
			
		}
		
		public void HealDeck(Deck deck)
		{
			int health = Math.Min(HealthPoints, deck.TotalHealth - deck.CurrentHealth);
			deck.Heal(health);
			HealthPoints -= health;
		}
		
	}
}
