/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 03.10.2020
 * Time: 0:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;

using System.Windows.Shapes;
using Config;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of Deck.
	/// </summary>
	public class Deck: Base, IFieldComponent
	{
		private Guid shipId;
		
		private int prizeForDestroy;
		private bool destroyed;
		
		#region Properties
		public Ship Ship
		{
			get
			{ return Database.ships.Where(ship => ship.ID == shipId).FirstOrDefault(); }
			set
			{ shipId = value.ID; }
		}
		
		public double PercentageHealthValue
		{
			get { return CurrentHealth*1.0/TotalHealth; }
		}
		
		public int CurrentHealth
		{ get; private set; }
		
		public int TotalHealth
		{ get; private set; }
		
		private DeckKind Kind
		{ get; set; }
		
		public string Orientation
		{ get; private set; }
		#endregion
		
		public Deck(Ship ship):
			this(ship, 100)
		{
			Database.decks.Add(this);
		}
		
		public Deck(
			Ship ship,
			int health)
		{
			Ship = ship;
			
			CurrentHealth = TotalHealth = health;
			destroyed = false;
			prizeForDestroy = 5*ship.Size;
			
			Database.decks.Add(this);
		}
		
		public void GetDamage(int damage, ref int money, ref int opponentMoney)
		{
			CurrentHealth -= damage;
			if (CurrentHealth < 0)
				CurrentHealth = 0;
			if (CurrentHealth == 0 && !destroyed)
			{
				destroyed = true;
				money += prizeForDestroy;
			}
		}
	}
}
