/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 11:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using GameObjects;

using System;
using System.Linq;
using System.Collections.Generic;

namespace GameParts
{
	/// <summary>
	/// Description of Ship.
	/// </summary>
	public class Ship
	{
		public static List<Ship> ships = new List<Ship>();
		
		private int size;
		private string orientation;
		
		private static int counter = 0;
		
		private int stability;
		private bool isDestroyed;
		private int destroyPrize;
		private int id;
		private int x;
		private int y;
		
		public int Id
		{
			get
			{
				return id;
			}
		}
		
		public List<Deck> Decks
		{
			get
			{
				return Deck.decks.Where(deck => deck.Ship == this).ToList();
			}
		}
		
		public Ship(int x, int y, int size, string orientation)
		{
			this.x = x;
			this.y = y;
			this.id = counter;
			counter++;
			
			this.isDestroyed = false;
			this.orientation = orientation;
			this.size = size;
			ships.Add(this);
			
			stability = QuantifyStability();
		}
		
		private int QuantifyStability()
		{
			return size*3;
		}
		
		public int GetAward()
		{
			return size*5;
		}
		
		public int GetOneDeckAward()
		{
			return size;
		}
		
		public Deck GetDeck(int number)
		{
			return Decks.ElementAt(number);
		}
		
		public int GetSize()
		{
			return this.Decks.Count;
		}
	}
}
