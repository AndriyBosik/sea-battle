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
		public static Bomb Generate(Field field, int x, int y, BombKind kind)
		{
			ShopBomb sb = ShopBombProcessor.GenerateBomb(kind);
			return new Bomb(field, new Point(x, y), sb.Radius, sb.CostByOne, sb.Damage, sb.DeactivationPrice, sb.icon);
		}
	}
}
