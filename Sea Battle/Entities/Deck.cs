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
using System.Xml.Serialization;
using Config;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of Deck.
	/// </summary>
	[Serializable]
	public class Deck: Base, IFieldComponent
	{
		public Guid shipId;
		
		public int prizeForDestroy;
		public bool destroyed;
		
		#region Properties
		[XmlIgnoreAttribute]
		public Ship Ship
		{
			get
			{ return Database.ships.Where(ship => ship.ID == shipId).FirstOrDefault(); }
			set
			{ shipId = value.ID; }
		}
		
		[XmlIgnoreAttribute]
		public double PercentageHealthValue
		{
			get { return CurrentHealth*1.0/TotalHealth; }
		}
		
		public int CurrentHealth
		{ get; set; }
		
		public int TotalHealth
		{ get; set; }
		
		public DeckKind Kind
		{ get; set; }
		
		public string Orientation
		{ get; set; }
		#endregion
		
		public Deck()
		{
			Database.decks.Add(this);
		}
		
		public Deck(Ship ship, int health = 100): this()
		{
			Ship = ship;
			
			CurrentHealth = TotalHealth = health;
			destroyed = false;
			prizeForDestroy = 5*ship.Size;
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
