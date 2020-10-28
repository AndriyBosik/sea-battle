/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/26/2020
 * Time: 14:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Entities;
using GameObjects;
using Shop;

namespace SeaBattle
{
	/// <summary>
	/// Description of BuyBombs.
	/// </summary>
	public class _BuyBombs: DescriptionWrapper.Buyable
	{
		private Dictionary<BombKind, int> shopBombs;
		private Player player;
		
		public _BuyBombs(Player player, Dictionary<BombKind, int> shopBombs)
		{
			this.player = player;
			this.shopBombs = shopBombs;
		}
		
		public void Buy(object sender, RoutedEventArgs e)
		{
			var button = (Button)sender;
			var shopBomb = (ShopBomb)button.Tag;
			var kind = ShopBombProcessor.GetKind(shopBomb);
			if (!kind.HasValue)
				return;
			if (!shopBombs.ContainsKey(kind.Value))
				shopBombs.Add(kind.Value, 0);
			shopBombs[kind.Value]++;
			player.Money -= shopBomb.CostByOne;
		}
		
	}
}
