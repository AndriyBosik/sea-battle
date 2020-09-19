/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/17/2020
 * Time: 23:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using GameObjects;

using System;
using System.Windows.Media;

namespace GameParts
{
	/// <summary>
	/// Description of Deck.
	/// </summary>
	public class Deck: Cell
	{
		private DeckKind kind;
		
		public Deck(int x, int y)
		{
			Init(x, y, DeckKind.ONE_DECK, Config.HORIZONTAL_ORIENTATION);
		}
		
		public Deck(int x, int y, DeckKind kind)
		{
			Init(x, y, kind, Config.HORIZONTAL_ORIENTATION);
		}
		
		public Deck(int x, int y, string orientation)
		{
			Init(x, y, DeckKind.ONE_DECK, orientation);
		}
		
		public Deck(int x, int y, DeckKind kind, string orientation)
		{
			Init(x, y, kind, orientation);
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
			if (orientation == Config.VERTICAL_ORIENTATION)
			{
				image.LayoutTransform = new RotateTransform(90);
			}
		}
	}
}
