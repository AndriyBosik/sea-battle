/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/20/2020
 * Time: 00:52
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of BombProcessor.
	/// </summary>
	public class BombProcessor
	{
		public static Bomb Generate(int x, int y, BombKind kind)
		{
			ShopBomb sb = ShopBombGenerator.GenerateBomb(kind);
			return new Bomb(new Point(x, y), sb.Radius, sb.CostByOne, sb.Damage, sb.DeactivationPrice, sb.icon);
		}
		
		public static BombKind? GetKind(Bomb bomb)
		{
			foreach (BombKind kind in Enum.GetValues(typeof(BombKind)))
			{
				if (bomb.Equals(BombProcessor.Generate(0, 0, kind)))
					return kind;
			}
			return null;
		}
	}
}
