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

using ItemViews;

using Config;
using Shop;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for Shop.xaml
	/// </summary>
	public partial class Shop : Window
	{
		
		private Player player;
		private List<DescriptionWrapper> bombItems;
		private List<DescriptionWrapper> gunItems;
		
		private ShopItem selectedItem;
		
		public List<Gun> Guns
		{
			get; private set;
		}
		
		public Shop(Player player)
		{
			InitializeComponent();
			
			this.SizeToContent = SizeToContent.WidthAndHeight;
			this.bombItems = new List<DescriptionWrapper>();
			this.gunItems = new List<DescriptionWrapper>();
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
			InitGrid();
			const int itemSize = Gameplay.SHOP_ITEM_SIZE;
			const int descriptionSize = Gameplay.SHOP_ITEM_DESCRIPTION_SIZE;
			int i = 0;
			foreach (BombKind kind in (BombKind[])Enum.GetValues(typeof(BombKind)))
			{
				var bomb = ShopBombProcessor.GenerateBomb(kind);
				var bombsBuyer = new BuyBombs(this);
				var count = player.ShopBombs.ContainsKey(kind) ? player.ShopBombs[kind] : 0;
				var bombItem = new DescriptionWrapper(bomb, itemSize, descriptionSize, count, bombsBuyer);
				bombItems.Add(bombItem);
				Grid.SetRow(bombItem, i);
				Grid.SetColumn(bombItem, 0);
				gItems.Children.Add(bombItem);
				i++;
			}
			
			i = 0;
			foreach (GunKind kind in (GunKind[])Enum.GetValues(typeof(GunKind)))
			{
				var gun = GunProcessor.GenerateGun(kind);
				var gunItem = TryGetFromPlayer(gun);
				if (gunItem == null)
					gunItem = new DescriptionWrapper(gun, itemSize, descriptionSize, 0, new BuyGuns(this));
				else
					gunItem.Disable();
				gunItem.PreviewMouseLeftButtonUp += Select;
				gunItems.Add(gunItem);
				Grid.SetRow(gunItem, i);
				Grid.SetColumn(gunItem, 1);
				gItems.Children.Add(gunItem);
				i++;
			}
		}
		
		private DescriptionWrapper TryGetFromPlayer(Gun gun)
		{
			foreach (var playerGun in player.Guns)
			{
				if (playerGun.Equals(gun))
					return new DescriptionWrapper(
						playerGun,
						Gameplay.SHOP_ITEM_SIZE,
						Gameplay.SHOP_ITEM_DESCRIPTION_SIZE,
						1,
						new BuyGuns(this));
			}
			return null;
		}
		
		private void DeselectAll()
		{
			foreach (var gun in gunItems)
				gun.Deselect();
		}
		
		private void Select(object sender, EventArgs e)
		{
			bBuyBullets.IsEnabled = false;
			selectedItem = null;
			var clicked = (DescriptionWrapper)sender;
			DeselectAll();
			foreach (var gun in player.Guns)
			{
				if (gun.Equals(clicked.Item))
				{
					if (clicked.Select())
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
			
			var bulletsShop = new BulletsShop(gun, player);
			bulletsShop.ShowDialog();
			Refresh();
		}
		
		private void Refresh()
		{
			ShowPlayerInformation();
			RefreshBombs();
			RefreshGuns();
		}
		
		private void RefreshBombs()
		{
			foreach (var bomb in bombItems)
				if (!player.CanBuy(bomb.Item))
					bomb.Disable();
				else
					bomb.Enable();
		}
		
		private void RefreshGuns()
		{
			foreach (var gun in gunItems)
				if (!player.CanBuy(gun.Item))
					gun.Disable();
				else
					gun.Enable();
		}
		
		private class BuyBombs: DescriptionWrapper.Buyable
		{
			private Shop parent;
			
			public BuyBombs(Shop parent)
			{
				this.parent = parent;
			}
			
			public void Buy(object sender, RoutedEventArgs e)
			{
				var button = (Button)sender;
				var shopBomb = (ShopBomb)button.Tag;
				var kind = ShopBombProcessor.GetKind(shopBomb);
				if (!kind.HasValue)
					return;
				parent.player.BuyBomb(kind.Value);
				parent.Refresh();
			}
		}
		
		private class BuyGuns: DescriptionWrapper.Buyable
		{
			private Shop parent;
			
			public BuyGuns(Shop parent)
			{
				this.parent = parent;
			}
			
			public void Buy(object sender, RoutedEventArgs e)
			{
				var button = (Button)sender;
				var gun = (Gun)button.Tag;
				var kind = GunProcessor.GetKind(gun);
				if (!kind.HasValue)
					return;
				parent.player.BuyGun(gun);
				parent.Refresh();
			}
		}
		
	}
}