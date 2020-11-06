/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/05/2020
 * Time: 15:04
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
	public class BombKindProcessor
	{
		public static string GetIcon(BombKind kind)
		{
			return kind.ToString();
		}
	}
}
