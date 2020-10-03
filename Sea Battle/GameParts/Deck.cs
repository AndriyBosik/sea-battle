/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/17/2020
 * Time: 23:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using GameObjects;

using Config;

using System;
using System.Linq;
using System.Windows.Media;
using System.Collections.Generic;

namespace GameParts
{
	/// <summary>
	/// Description of Deck.
	/// </summary>
	public class Deck: Cell
	{
		public static List<Deck> decks = new List<Deck>();
		
		private DeckKind kind;
		
		private int shipId;
		private int health;
		private int maxHealth;
		private int destroyPrize;
		
		public Ship Ship
		{
			get
			{
				return Ship.ships.Where(ship => ship.Id == shipId).FirstOrDefault();
			}
			set
			{
				shipId = value.Id;
			}
		}
		
		public int MaxHealth
		{
			get { return maxHealth; }
			set { maxHealth = value; }
		}
		
		public int Health
		{
			get { return health; }
			set { health = value; }
		}
		
		public Deck(
			Ship ship,
			DeckKind kind = DeckKind.ONE_DECK,
			string orientation = Gameplay.HORIZONTAL_ORIENTATION,
			int health = 100)
		{
			Ship = ship;
			Init(kind, orientation);
			decks.Add(this);
			this.maxHealth = this.health = health;
		}
		
		private void Init(DeckKind kind, string orientation)
		{
			this.kind = kind;
			this.icon = DeckKindProcessor.GetIcon(kind);
			
			base.Init(this.icon);
			
			status = CellStatus.DECK;
			RotateDeck(orientation);
		}
		
		private void RotateDeck(string orientation)
		{
			if (orientation == Gameplay.VERTICAL_ORIENTATION)
			{
				image.LayoutTransform = new RotateTransform(90);
			}
		}
		
		public void Hurt(int damage)
		{
			health -= damage;
		}
		
		public void Heal(int health)
		{
			this.health += health;
		}
	}
}
