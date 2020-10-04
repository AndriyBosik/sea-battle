/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/03/2020
 * Time: 11:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

namespace Entities
{
	/// <summary>
	/// Description of Ship.
	/// </summary>
	public class Ship: Base
	{
		public bool isDestroyed
		{
			get;
			private set;
		}
		
		public int PrizeForDestroy
		{
			get;
			private set;
		}
		
		public int Size
		{
			get;
			private set;
		}
		
		public string Orientation
		{
			get;
			private set;
		}
		
		public List<Deck> Decks
		{
			get
			{
				return Database.decks.Where(deck => deck.Ship == this).ToList();
			}
		}
		
		public Ship(int size, string orientation)
		{
			Size = size;
			Orientation = orientation;
			
			Database.ships.Add(this);
		}
		
		public int GetAward()
		{
			return Size*5;
		}
		
		public Deck getDeck(int number)
		{
			return Decks.ElementAt(number);
		}
	}
}
