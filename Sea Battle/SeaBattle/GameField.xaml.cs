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
using Processors;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for Game.xaml
	/// </summary>
	public partial class GameField: Window
	{
		private Game game;
		
		private int lastHoverRow;
		private int lastHoverColumn;
		private Direction lastHoverDirection;
		private Direction currentDirection;
		
		private Player firstPlayer;
		private Player secondPlayer;
		private ShadowDrawer drawer;
		
		private List<ItemDescription> gunViews;
		private List<ItemDescription> bulletViews;
		
		private Gun selectedGun;
		private Entities.BulletPack selectedBulletPack;
		
		public GameField(Player firstPlayer, Player secondPlayer)
		{
			InitializeComponent();
			
			this.firstPlayer = firstPlayer;
			this.secondPlayer = secondPlayer;
			
			lastHoverRow = lastHoverColumn = -1;
			lastHoverDirection = Direction.NO_DIRECTION;
			currentDirection = Direction.UP;
			
			game = new Game(firstPlayer, secondPlayer);
			drawer = new ShadowDrawer(game);
			
			FillWithGuns(firstPlayer);
			
			firstPlayer.Field.Name = Gameplay.FIRST_PLAYER_FIELD;
			
			spContent.Children.Add(GetGameField(firstPlayer.Field));
			
			spContent.Children.Add(GetVSLabel());
			
			secondPlayer.Field.Name = Gameplay.SECOND_PLAYER_FIELD;
			spContent.Children.Add(GetGameField(secondPlayer.Field));
			
			spContent.MouseLeftButtonUp += ProcessMove;
		}
		
		private void FillWithGuns(Player player)
		{
			spGuns.Children.Clear();
			var guns = player.Guns;
			gunViews = new List<ItemDescription>();
			foreach (var gun in guns)
			{
				gunViews.Add(new ItemDescription(gun, Gameplay.ITEM_SIZE, Gameplay.ITEM_DESCRIPTION_SIZE));
				gunViews.LastOrDefault().PreviewMouseLeftButtonDown += RefreshBullets;
				spGuns.Children.Add(gunViews.LastOrDefault());
			}
		}
		
		private void RefreshBullets(object sender, EventArgs e)
		{
			var item = (ItemDescription)sender;
			Select(item, gunViews);
			var gun = (Gun)item.Item;
			selectedGun = gun;
			spBullets.Children.Clear();
			bulletViews = new List<ItemDescription>();
			foreach (var bullet in gun.BulletPacks)
			{
				bulletViews.Add(new ItemDescription(bullet, Gameplay.ITEM_SIZE, Gameplay.ITEM_DESCRIPTION_SIZE));
				bulletViews.LastOrDefault().PreviewMouseLeftButtonDown += (s, ev) => {
					var bulletPack = (ItemDescription)s;
					Select(bulletPack, bulletViews);
					selectedBulletPack = (Entities.BulletPack)bulletPack.Item;
				};
				spBullets.Children.Add(bulletViews.LastOrDefault());
			}
		}
		
		private void Select(ItemDescription selected, List<ItemDescription> items)
		{
			foreach(var item in items)
			{
				item.Deselect();
			}
			selected.Select();
		}
		
		private Grid GetGameField(Field field)
		{
			int n = field.RowDefinitions.Count;
			int m = field.ColumnDefinitions.Count;
			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < m; j++)
				{
					field.cells[i][j].Cover();
					field.Repaint(i, j);
					RefreshDataAfterMove(field);
				}
			}
			field.PreviewMouseLeftButtonDown += TryUncover;
			field.PreviewMouseMove += MakeSelected;
			field.MouseLeave += (sender, e) =>
				drawer.DrawSelectedArea(-1, -1, GetDamageKind(), GetRadius(), currentDirection);
			field.PreviewMouseWheel += ChangeDirection;
			return field;
		}
		
		private void ChangeDirection(object sender, MouseWheelEventArgs e)
		{
			var field = (Field)sender;
			if (field != game.GetCurrentField())
				return;
			var cell = (UIElement)e.Source;
			var row = Grid.GetRow(cell);
			var column = Grid.GetColumn(cell);
			
			if (e.Delta > 0)
				ChangeDirectionUp();
			if (e.Delta < 0)
				ChangeDirectionDown();
			drawer.DrawSelectedArea(row, column, GetDamageKind(), GetRadius(), currentDirection);
		}
		
		private void ChangeDirectionDown()
		{
			if (currentDirection == Direction.UP)
				currentDirection = Direction.RIGHT;
			else if (currentDirection == Direction.RIGHT)
				currentDirection = Direction.DOWN;
			else if (currentDirection == Direction.DOWN)
				currentDirection = Direction.LEFT;
			else
				currentDirection = Direction.UP;
		}
		
		private void ChangeDirectionUp()
		{
			if (currentDirection == Direction.UP)
				currentDirection = Direction.LEFT;
			else if (currentDirection == Direction.LEFT)
				currentDirection = Direction.DOWN;
			else if (currentDirection == Direction.DOWN)
				currentDirection = Direction.RIGHT;
			else
				currentDirection = Direction.UP;
		}
		
		private void MakeSelected(object sender, MouseEventArgs e)
		{
			var field = (Field)sender;
			if (field != game.GetCurrentField())
				return;
			var cell = (UIElement)e.Source;
			var row = Grid.GetRow(cell);
			var column = Grid.GetColumn(cell);
			drawer.DrawSelectedArea(row, column, GetDamageKind(), GetRadius(), currentDirection);
		}
		
		private void RefreshDataAfterMove(Field field)
		{
			spGuns.Children.Clear();
			spBullets.Children.Clear();
			selectedGun = null;
			selectedBulletPack = null;
			FillWithGuns(game.GetCurrentPlayer());
		}
		
		private void TryUncover(object sender, MouseButtonEventArgs e)
		{
			drawer.DrawSelectedArea(-1, -1, GetDamageKind(), GetRadius(), Direction.NO_DIRECTION);
			var field = (Field)sender;
			if (!game.TryMove(field))
				return;
			var element = (UIElement)e.Source;
			var row = Grid.GetRow(element);
			var column = Grid.GetColumn(element);
			var cell = field.cells[row][column];
			game.MakeMove(row, column);
			if (selectedGun != null && selectedBulletPack != null)
			{
				selectedGun.Shot(
					field, selectedBulletPack, new GameObjects.Point(row, column), currentDirection);
			}
			field.Repaint(row, column);
			RefreshDataAfterMove(field);
		}
		
		private int GetRadius()
		{
			if (selectedBulletPack == null)
				return 1;
			return selectedBulletPack.Radius;
		}
		
		private DamageKind GetDamageKind()
		{
			if (selectedGun == null)
				return DamageKind.LINEAR;
			return selectedGun.DamageKind;
		}
		
		private Field GetAnotherField(Field field)
		{
			if (field == firstPlayer.Field)
				return secondPlayer.Field;
			return firstPlayer.Field;
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
		
		private Label GetVSLabel()
		{
			Label l = new Label();
			l.FontSize = 30;
			l.Content = "VS";
			l.VerticalAlignment = VerticalAlignment.Center;
			l.HorizontalAlignment = HorizontalAlignment.Center;
			return l;
		}
		
		public void ProcessMove(object sender, RoutedEventArgs e)
		{
			Field field = GetField(e);
			if (field == null) return;
		}
		
		private Field GetField(RoutedEventArgs e)
		{
			if (!(e.Source is Image))
			{
				return null;
			}
			Label cell = (Label)e.Source;
			if (!(cell.Parent is Field))
			{
				return null;
			}
			var field = (Field)cell.Parent;
			return field;
		}
		
	}
}