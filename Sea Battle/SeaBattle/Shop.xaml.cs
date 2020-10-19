/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/08/2020
 * Time: 13:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Collections.Generic;

using System.Windows.Controls;

using GameObjects;

using Entities;

using FieldEditorParts;

using Shop;

using Config;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for Shop.xaml
	/// </summary>
	public partial class Shop : Window
	{
		
		private Player player;
		private List<ShopItemView> items;
		
		private ShopItem selectedItem;
		
		public List<ShopBomb> ShopBombs
		{
			get; set;
		}
		
		public List<Gun> Guns
		{
			get; private set;
		}
		
		public Shop(Player player)
		{
			InitializeComponent();
			
			this.SizeToContent = SizeToContent.WidthAndHeight;
			this.items = new List<ShopItemView>();
			this.player = player;
			
			ShowPlayerInformation();
			
			GetBombs();
		}
		
		private void ShowPlayerInformation()
		{
			lPlayerInformation.Content = "You have " + player.Money + " coins";
		}
		
		private void InitGrid()
		{
			//gItems = new Grid();
			
			gItems.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60, GridUnitType.Star) });
			gItems.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60, GridUnitType.Star) });
			gItems.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60, GridUnitType.Star) });
			
			gItems.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(300) });
			gItems.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(300) });
		}
		
		private void GetBombs()
		{
			ShopBombs = new List<ShopBomb>();
			
			InitGrid();
			
			int i = 0;
			foreach (BombKind kind in (BombKind[])Enum.GetValues(typeof(BombKind)))
			{
				var bomb = new ShopItemView(ShopBombGenerator.GenerateBomb(kind), Buy, player.Money);
				items.Add(bomb);
				Grid.SetRow(bomb, i);
				Grid.SetColumn(bomb, 0);
				gItems.Children.Add(bomb);
				i++;
			}
			
			i = 0;
			foreach (GunKind kind in (GunKind[])Enum.GetValues(typeof(GunKind)))
			{
				var gun = new ShopItemView(GunGenerator.GenerateGun(kind), Buy, player.Money);
				gun = TryGetFromPlayer(gun);
				gun.PreviewMouseLeftButtonUp += Select;
				items.Add(gun);
				Grid.SetRow(gun, i);
				Grid.SetColumn(gun, 1);
				gItems.Children.Add(gun);
				i++;
			}
		}
		
		private ShopItemView TryGetFromPlayer(ShopItemView item)
		{
			foreach (var playerGun in player.Guns)
			{
				if (playerGun.Equals(item.Item))
					return new ShopItemView(playerGun, Buy, player.Money);
			}
			return item;
		}
		
		private void Select(object sender, EventArgs e)
		{
			bBuyBullets.IsEnabled = false;
			selectedItem = null;
			var clicked = (ShopItemView)sender;
			foreach (var item in items)
			{
				item.Unselect();
				if (item.Item.Equals(clicked.Item))
				{
					if (item.TrySelect())
					{
						selectedItem = (Gun)clicked.Item;
						bBuyBullets.IsEnabled = true;
						bBuyBullets.Tag = (Gun)clicked.Item;
					}
				}
			}
		}
		
		private void bOKClick(object sender, EventArgs e)
		{
			DialogResult = true;
			Close();
		}
		
		private void bBuyBulletsClick(object sender, EventArgs e)
		{
			var clicked = (Button)sender;
			var gun = (Gun)clicked.Tag;
			
			var bulletsShop = new BulletsShop(gun, player.Money);
			if (bulletsShop.ShowDialog() == true)
			{
				player.Money = bulletsShop.Money;
				Refresh();
			}
		}
		
		private void Buy(object sender, RoutedEventArgs e)
		{
			ShopItem item =  (ShopItem)((Button)sender).Tag;
			if (!item.TryBuy())
				return;
			if (item is ShopBomb)
			{
				ShopBombs.Add((ShopBomb)item);
			} else if (item is Gun)
			{
				player.Guns.Add((Gun)item);
			}
			player.Money -= item.CostByOne;
			Refresh();
		}
		
		private void Refresh()
		{
			ShowPlayerInformation();
			foreach (var item in items)
			{
				item.Refresh(player.Money);
			}
		}
		
	}
}