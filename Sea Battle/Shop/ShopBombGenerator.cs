/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/12/2020
 * Time: 16:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Entities;

using GameObjects;

using Config;

namespace Shop
{
	/// <summary>
	/// Description of ShopBombGenerator.
	/// </summary>
	public class ShopBombGenerator
	{
		public static ShopBomb GenerateBomb(BombKind kind)
		{
			string icon = kind.ToString();
			switch (kind)
			{
				case BombKind.SmallBomb:
					return new ShopBomb(1, 100, 100, 100, icon);
				case BombKind.MediumBomb:
					return new ShopBomb(2, 400, 100, 300, icon);
				case BombKind.LargeBomb:
					return new ShopBomb(3, 1000, 110, 1500, icon);
				default:
					return null;
			}
		}
	}
}
