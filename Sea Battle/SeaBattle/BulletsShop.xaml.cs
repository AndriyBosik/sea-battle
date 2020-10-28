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

using ItemViews;

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
		
		private List<BulletPack> bullets;
		private List<DescriptionWrapper> items = new List<DescriptionWrapper>();
		
		private Gun gun;
		private Player player;
		
		public BulletsShop(Gun gun, Player player)
		{
			InitializeComponent();
			SizeToContent = SizeToContent.WidthAndHeight;
			
			this.player = player;
			this.gun = gun;
			bullets = Database.shopBulletPacks.Where(bulletPack => bulletPack.DamageKind == gun.DamageKind).ToList();
			InitGrid();
			FillWithBulletPacks();
		}
		
		private void InitGrid()
		{
			for (int i = 0; i < NUMBER_OF_COLUMNS; i++)
			{
				gItems.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(300) });
			}
			
			int count = bullets.Count;
			int rowsCount = count / NUMBER_OF_COLUMNS + (count % NUMBER_OF_COLUMNS > 0 ? 1 : 0);
			
			for (int i = 0; i < rowsCount; i++)
			{
				gItems.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(60, GridUnitType.Star) });
			}
		}
		
		private void FillWithBulletPacks()
		{
			int current = 0;
			foreach (var bulletPack in bullets)
			{
				var count = GetCount(bulletPack);
				var item = new DescriptionWrapper(
					bulletPack,
					Gameplay.SHOP_ITEM_SIZE,
					Gameplay.SHOP_ITEM_DESCRIPTION_SIZE,
					BulletPackInGun.GetCount(gun, bulletPack),
					new BuyBullets(this));
				
				int row = current / NUMBER_OF_COLUMNS;
				int column = current % NUMBER_OF_COLUMNS;
				Grid.SetRow(item, row);
				Grid.SetColumn(item, column);
				gItems.Children.Add(item);
				items.Add(item);
				current++;
			}
		}
		
		private int GetCount(BulletPack bulletPack)
		{
			var bullets = Database.bulletPackInGuns.Where(bpig => bpig.Gun == gun && bpig.BulletPack.Equals(bulletPack))
				.FirstOrDefault();
			if (bullets == null)
				return 0;
			return bullets.Count;
		}
		
		private void BuyMethod(object sender, EventArgs e)
		{
			var button = (Button)sender;
			var item = (ShopItem)button.Tag;
			new BulletPackInGun(gun, (BulletPack)item);
			player.Money -= item.CostByOne;
		}
		
		private void bOKClick(object sender, EventArgs e)
		{
			DialogResult = true;
			Close();
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
				var item = (BulletPack)button.Tag;
				new BulletPackInGun(parent.gun, (BulletPack)item);
				parent.player.Money -= item.CostByOne;
			}
		}
	}
}