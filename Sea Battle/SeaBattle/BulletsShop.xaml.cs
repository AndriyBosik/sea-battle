/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/18/2020
 * Time: 14:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

using Entities;

using Config;
using Shop;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for BulletsShop.xaml
	/// </summary>
	public partial class BulletsShop : Window
	{
		private const int NUMBER_OF_COLUMNS = 2;
		
		private List<Picture<BulletPack>> pictureBullets;
		private List<DescriptionWrapper> descriptions = new List<DescriptionWrapper>();
		
		private Gun gun;
		private Player player;
		
		public BulletsShop(Gun gun, Player player)
		{
			InitializeComponent();
			SizeToContent = SizeToContent.WidthAndHeight;
			
			this.player = player;
			this.gun = gun;
			pictureBullets = Entities.Database.shopBulletPacks.Where(bulletPackPicture =>
				bulletPackPicture.Item.DamageKind == gun.DamageKind).ToList();
			ShowPlayerInformation();
			InitGrid();
			FillWithBulletPacks();
			RefreshBullets();
		}
		
		private void ShowPlayerInformation()
		{
			lPlayerInformation.Content = "You have " + player.Money + " coins";
		}
		
		private void InitGrid()
		{
			for (int i = 0; i < NUMBER_OF_COLUMNS; i++)
			{
				gItems.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(300) });
			}
			
			int count = pictureBullets.Count;
			int rowsCount = count / NUMBER_OF_COLUMNS + (count % NUMBER_OF_COLUMNS > 0 ? 1 : 0);
			
			for (int i = 0; i < rowsCount; i++)
			{
				gItems.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60, GridUnitType.Star) });
			}
		}
		
		private void FillWithBulletPacks()
		{
			int current = 0;
			foreach (var pictureBulletPack in pictureBullets)
			{
				var count = GetCount(pictureBulletPack.Item);
				var item = new DescriptionWrapper(
					MapToShopItemPicture(pictureBulletPack),
					Gameplay.SHOP_ITEM_SIZE,
					Gameplay.SHOP_ITEM_DESCRIPTION_SIZE,
					BulletPackInGun.GetCount(gun, pictureBulletPack.Item),
					new BuyBullets(this));
				
				int row = current / NUMBER_OF_COLUMNS;
				int column = current % NUMBER_OF_COLUMNS;
				Grid.SetRow(item, row);
				Grid.SetColumn(item, column);
				gItems.Children.Add(item);
				descriptions.Add(item);
				current++;
			}
			RefreshBullets();
		}
		
		private Picture<ShopItem> MapToShopItemPicture(Picture<BulletPack> pictureBulletPack)
		{
			return new Picture<ShopItem>(pictureBulletPack.Item, pictureBulletPack.Icon);
		}
		
		private int GetCount(BulletPack bulletPack)
		{
			var bullets = Entities.Database.bulletPackInGuns.Where(bpig => bpig.Gun == gun && bpig.BulletPack.Equals(bulletPack))
				.FirstOrDefault();
			if (bullets == null)
				return 0;
			return bullets.Count;
		}
		
		private void bOKClick(object sender, EventArgs e)
		{
			DialogResult = true;
			Close();
		}
		
		private void RefreshBullets()
		{
			foreach (var description in descriptions)
				if (player.Money < description.Picture.Item.CostByOne)
					description.Disable();
		}
		
		private class BuyBullets: DescriptionWrapper.Buyable
		{
			private BulletsShop parent;
			
			public BuyBullets(BulletsShop parent)
			{
				this.parent = parent;
			}
			
			public void Buy(object sender, RoutedEventArgs e)
			{
				var button = (Button)sender;
				var bulletPackPicture = (Picture<ShopItem>)button.Tag;
				var bulletPack = (BulletPack)bulletPackPicture.Item;
				new BulletPackInGun(parent.gun, bulletPack);
				parent.player.Money -= bulletPack.CostByOne;
				parent.RefreshBullets();
				parent.ShowPlayerInformation();
			}
		}
	}
}