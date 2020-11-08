/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/20/2020
 * Time: 15:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using Entities;
using FieldEditorParts;

using GameObjects;

using ItemViews;

using Config;

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for Game.xaml
	/// </summary>
	public partial class GameField: Window, Game.Messanger
	{
		private Game game;
		
		private Player firstPlayer;
		private Player secondPlayer;
		private ShadowDrawer drawer;
		
		private List<ItemDescription> gunViews;
		private List<ItemDescription> bulletViews;
		
		private ItemDescription selectedBulletPack;
		
		public GameField(Player firstPlayer, Player secondPlayer)
		{
			InitializeComponent();
			
			this.firstPlayer = firstPlayer;
			this.secondPlayer = secondPlayer;
			game = new Game(firstPlayer, secondPlayer, this);
			
			RefreshContent();
			
			drawer = new ShadowDrawer(game);
			
			FillWithGuns();
			
			bBuyBullets.PreviewMouseLeftButtonDown += OpenBulletsShop;
			
			InitField(firstPlayer.Field);
			InitField(secondPlayer.Field);
			bFirstPlayer.Child = firstPlayer.Field;
			bSecondPlayer.Child = secondPlayer.Field;
			
			spContent.PreviewMouseLeftButtonUp += ProcessMove;
		}
		
		private void RefreshContent()
		{
			tbPlayerInformation.Text =  "You have:\n" +
										game.GetCurrentPlayer().Money + " coins\n";
			if (game.IsFirstPlayer(game.GetCurrentPlayer()))
			{
				bSecondPlayer.BorderBrush = Brushes.Blue;
				bFirstPlayer.BorderBrush = Brushes.Transparent;
			}
			else
			{
				bSecondPlayer.BorderBrush = Brushes.Transparent;
				bFirstPlayer.BorderBrush = Brushes.Blue;
			}
		}
		
		private void OpenBulletsShop(object sender, EventArgs e)
		{
			if (game.GetCurrentPlayer().SelectedGun == null)
				return;
			var shop = new BulletsShop(game.GetCurrentPlayer().SelectedGun, game.GetCurrentPlayer());
			shop.ShowDialog();
			RefreshContent();
			RefreshBulletsList(game.GetCurrentPlayer().SelectedGun);
		}
		
		private void FillWithGuns()
		{
			spGuns.Children.Clear();
			var guns = game.GetCurrentPlayer().Guns;
			gunViews = new List<ItemDescription>();
			foreach (var gun in guns)
			{
				var picture = new Picture<ShopItem>(
					gun, GunProcessor.GetGunIcon(GunProcessor.GetKind(gun).Value));
				gunViews.Add(new ItemDescription(
					picture, Gameplay.ITEM_SIZE, Gameplay.ITEM_DESCRIPTION_SIZE, 0));
				gunViews.LastOrDefault().PreviewMouseLeftButtonDown += RefreshBullets;
				spGuns.Children.Add(gunViews.LastOrDefault());
			}
		}
		
		private void RefreshBullets(object sender, EventArgs e)
		{
			var item = (ItemDescription)sender;
			Select(item, gunViews);
			var gun = (Gun)item.Picture.Item;
			RefreshBulletsList(gun);
		}
		
		private void RefreshBulletsList(Gun gun)
		{
			game.GetCurrentPlayer().SelectGun(gun);
			spBullets.Children.Clear();
			bulletViews = new List<ItemDescription>();
			foreach (var bullet in gun.BulletPacks)
			{
				var count = BulletPackInGun.GetCount(gun, bullet);
				var bulletIcon = BulletPackProcessor.GetBulletPackIcon(BulletPackProcessor.GetKind(bullet).Value);
				var bulletPicture = new Picture<ShopItem>(bullet, bulletIcon);
				var bulletView = new ItemDescription(
					bulletPicture, Gameplay.ITEM_SIZE, Gameplay.ITEM_DESCRIPTION_SIZE, count);
				spBullets.Children.Add(bulletView);
				if (count == 0)
					bulletView.Disable();
				else
					bulletView.PreviewMouseLeftButtonDown += (s, ev) => {
						var bulletPack = (ItemDescription)s;
						Select(bulletPack, bulletViews);
						game.GetCurrentPlayer().SelectBulletPack(bullet);
					};
				bulletViews.Add(bulletView);
			}
		}
		
		private int GetCount(Entities.BulletPack bulletPack, Gun gun)
		{
			var bullets = Database.bulletPackInGuns.Where(bpig => bpig.Gun == gun && bpig.BulletPack.Equals(bulletPack))
				.FirstOrDefault();
			if (bullets == null)
				return 0;
			return bullets.Count;
		}
		
		private void Select(ItemDescription selected, List<ItemDescription> items)
		{
			foreach(var item in items)
			{
				item.Deselect();
			}
			selectedBulletPack = selected;
			selected.Select();
		}
		
		private void InitField(Field field)
		{
			field.Cover();
			RefreshDataAfterMove();
			field.PreviewMouseLeftButtonDown += TryUncover;
			field.PreviewMouseMove += MakeSelected;
			var player = game.GetCurrentPlayer();
			field.MouseLeave += (sender, e) =>
				drawer.DrawSelectedArea(-1, -1, player.DamageKind, player.Radius, player.ShotDirection);
			field.PreviewMouseWheel += ChangeDirection;
		}
		
		private void ChangeDirection(object sender, MouseWheelEventArgs e)
		{
			var field = (Field)sender;
			if (field != game.GetCurrentField())
				return;
			int row, column;
			GetCoords(out row, out column, e);
			if (e.Delta > 0)
				game.GetCurrentPlayer().PreviousDirection();
			if (e.Delta < 0)
				game.GetCurrentPlayer().NextDirection();
			var player = game.GetCurrentPlayer();
			drawer.DrawSelectedArea(row, column, player.DamageKind, player.Radius, player.ShotDirection);
		}
		
		private void MakeSelected(object sender, MouseEventArgs e)
		{
			var field = (Field)sender;
			if (field != game.GetCurrentField())
				return;
			int row, column;
			GetCoords(out row, out column, e);
			var player = game.GetCurrentPlayer();
			drawer.DrawSelectedArea(row, column, player.DamageKind, player.Radius, player.ShotDirection);
		}
		
		private void RefreshDataAfterMove()
		{
			spGuns.Children.Clear();
			spBullets.Children.Clear();
			game.GetCurrentPlayer().DeselectAll();
			gunViews = null;
			bulletViews = null;
			RefreshContent();
			game.GetCurrentPlayer().DeselectAll();
			FillWithGuns();
		}
		
		private void TryUncover(object sender, MouseButtonEventArgs e)
		{
			drawer.DrawSelectedArea(
				-1, -1, game.GetCurrentPlayer().DamageKind, game.GetCurrentPlayer().Radius, Direction.NO_DIRECTION);
			
			var field = (Field)sender;
			int row, column;
			GetCoords(out row, out column, e);
			var cell = field.GetElement(row, column);
			if (game.MakeMove(field, row, column))
			{
				RefreshDataAfterMove();
			}
		}
		
		private StackPanel GetGuns(Player player)
		{
			var sp = new StackPanel();
			sp.Orientation = Orientation.Vertical;
			foreach (var item in player.Guns)
			{
				var l = new Label();
				l.Content = item.ToString();
				l.Content += item.BulletPacks.Count.ToString();
				sp.Children.Add(l);
			}
			return sp;
		}
		
		private void GetCoords(out int row, out int column, RoutedEventArgs e)
		{
			var element = (UIElement)e.Source;
			row = Grid.GetRow(element);
			column = Grid.GetColumn(element);
		}
		
		public void ProcessMove(object sender, RoutedEventArgs e)
		{
			RefreshDataAfterMove();
		}
		
		public void CongratulatePlayer(string message)
		{
			var congratulateWindow = new CongratulateWindow(message);
			if (congratulateWindow.ShowDialog() == true)
			{
				var menu = new StartMenu();
				menu.Show();
				this.Close();
			}
			else
			{
				Application.Current.Shutdown();
			}
		}
		
	}
}