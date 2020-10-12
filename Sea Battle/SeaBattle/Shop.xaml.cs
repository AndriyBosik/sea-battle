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
		
		public List<ShopBomb> ShopBombs
		{
			get; set;
		}
		
		public List<ShopGun> ShopGuns
		{
			get; set;
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
			ShopGuns = new List<ShopGun>();
			
			InitGrid();
			
			int i = 0;
			foreach (BombKind kind in (BombKind[])Enum.GetValues(typeof(BombKind)))
			{
				var bomb = new ShopItemView(ShopBombGenerator.GenerateBomb(kind), Buy);
				items.Add(bomb);
				Grid.SetRow(bomb.GetView(player.Money), i);
				Grid.SetColumn(bomb.GetView(player.Money), 0);
				gItems.Children.Add(bomb.GetView(player.Money));
				i++;
			}
			
			i = 0;
			foreach (GunKind kind in (GunKind[])Enum.GetValues(typeof(GunKind)))
			{
				var gun = new ShopItemView(ShopGunGenerator.GenerateGun(kind), Buy);
				items.Add(gun);
				Grid.SetRow(gun.GetView(player.Money), i);
				Grid.SetColumn(gun.GetView(player.Money), 1);
				gItems.Children.Add(gun.GetView(player.Money));
				i++;
			}
		}
		
		private void bOKClick(object sender, EventArgs e)
		{
			DialogResult = true;
			Close();
		}
		
		private void Buy(object sender, RoutedEventArgs e)
		{
			ShopItem item =  (ShopItem)((Button)sender).Tag;
			if (item is ShopBomb)
			{
				ShopBombs.Add((ShopBomb)item);
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