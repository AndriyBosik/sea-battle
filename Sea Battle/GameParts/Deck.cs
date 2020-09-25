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
		
		public Deck(
			int x,
			int y,
			Ship ship,
			DeckKind kind = DeckKind.ONE_DECK,
			string orientation = Gameplay.HORIZONTAL_ORIENTATION)
		{
			Ship = ship;
			Init(x, y, kind, orientation);
			decks.Add(this);
		}
		
		private void Init(int x, int y, DeckKind kind, string orientation)
		{
			this.kind = kind;
			this.icon = DeckKindProcessor.GetIcon(kind);
			
			base.Init(x, y, this.icon);
			
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
	}
}
