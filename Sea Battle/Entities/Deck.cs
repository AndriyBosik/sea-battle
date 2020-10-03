/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 03.10.2020
 * Time: 0:03
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Entities
{
	/// <summary>
	/// Description of Deck.
	/// </summary>
	public class Deck
	{
		public Deck()
		{
			
			Database.decks.Add(this);
		}
	}
}
