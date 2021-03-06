﻿/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/12/2020
 * Time: 16:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using GameObjects;

using Config;

namespace Entities
{
	/// <summary>
	/// Description of ShopBombGenerator.
	/// </summary>
	public class ShopBombProcessor
	{
		public static ShopBomb GenerateBomb(BombKind kind)
		{
			switch (kind)
			{
				case BombKind.SmallBomb:
					return new ShopBomb(1, 100, 100);
				case BombKind.MediumBomb:
					return new ShopBomb(2, 400, 100);
				case BombKind.LargeBomb:
					return new ShopBomb(3, 1000, 60);
				default:
					return null;
			}
		}
		
		public static string GetBombIcon(BombKind kind)
		{
			return kind.ToString();;
		}
		
		public static BombKind? GetKind(ShopBomb shopBomb)
		{
			foreach (BombKind kind in (BombKind[])Enum.GetValues(typeof(BombKind)))
				if (shopBomb.Equals(ShopBombProcessor.GenerateBomb(kind)))
					return kind;
			return null;
		}
	}
}
