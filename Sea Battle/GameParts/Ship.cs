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
		protected int size;
		protected string orientation;
		private List<Deck> ship;
		private bool isDestroyed;
		
		public Ship(int x, int y, int size, string orientation)
		{
			this.isDestroyed = false;
			ship = new List<Deck>();
			this.orientation = orientation;
			this.size = size;
			for (int i = 0; i < size; i++)
			{
				if (orientation.Equals(Config.HORIZONTAL_ORIENTATION))
				{
					AddDeck(x, y + i, DeckKindProcessor.GetDeckKind(i, size), orientation);
				}
				else
				{
					AddDeck(x + i, y, DeckKindProcessor.GetDeckKind(i, size), orientation);
				}
			}
		}
		
		private void AddDeck(int x, int y, DeckKind kind, string orientation)
		{
			ship.Add(new Deck(x, y, kind, orientation));
		}
		
		public Deck GetDeck(int number)
		{
			return ship.ElementAt(number);
		}
		
		public int GetSize()
		{
			return ship.Count;
		}
	}
}
