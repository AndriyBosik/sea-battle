/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/18/2020
 * Time: 00:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Config;

namespace GameObjects
{
	/// <summary>
	/// Description of DeckKindProcessor.
	/// </summary>
	public class DeckKindProcessor
	{
		public static DeckKind GetDeckKind(int numberOfDeck, int shipSize)
		{
			if (numberOfDeck == 0 && shipSize == 1)
			{
				return DeckKind.ONE_DECK;
			}
			if (numberOfDeck == 0)
			{
				return DeckKind.BEGIN;
			}
			if (numberOfDeck == shipSize - 1)
			{
				return DeckKind.END;
			}
			return DeckKind.INTERNAL;
		}
		
		public static string GetIcon(DeckKind kind)
		{
			switch (kind)
			{
				case DeckKind.BEGIN:
					return Images.BEGIN;
				case DeckKind.INTERNAL:
					return Images.INTERNAL;
				case DeckKind.END:
					return Images.END;
				default:
					return Images.ONE_DECK;
			}
		}
	}
}
