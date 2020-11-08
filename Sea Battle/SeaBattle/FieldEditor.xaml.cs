/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/07/2020
 * Time: 19:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using System.Windows.Input;
using FieldEditorParts;

using GameObjects;

using Config;

using Entities;

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for FieldEditor.xaml
	/// </summary>
	public partial class FieldEditor : Window
	{
		private const string BOMBS_GROUP = "bombs";
		private const string ALL_SHIPS_ARE_NOT_PASTED = "You must place all the ships!!!";
		private const string ERROR_TITLE = "Error";
		private const int CELL_SIZE = 35;
		private const int MARGIN = 10;
		
		private StackPanel rightColumn;
		private StackPanel bombsGroup;
		private SizeRadios sizeRadios;
		private OrientationGroup orientationGroup;
		
		private string orientation;
		private int size;
		private int rows;
		private int columns;
		private bool isFirstPlayerReady;
		
		private BombKind bombKind;
		private Player firstPlayer;
		private Player secondPlayer;
		private Dictionary<BombKind, int> shopBombs;
		
		public FieldEditor(int rows, int columns)
		{
			InitializeComponent();
			
			this.rows = rows;
			this.columns = columns;
			this.isFirstPlayerReady = false;
			
			firstPlayer = new Player();
			firstPlayer.Field = new Field(rows, columns);
			
			secondPlayer = new Player();
			secondPlayer.Field = new Field(rows, columns);
			
			SetButtonsOnClickListeners();
			
			SetWindowSize();
			
			SetGrid(firstPlayer.Field);
		}
		
		private void SetButtonsOnClickListeners()
		{
			bGoBack.PreviewMouseLeftButtonDown += ShowMainWindow;
			bShop.PreviewMouseLeftButtonUp += OpenShop;
			bNext.PreviewMouseLeftButtonUp += NextStep;
		}
		
		// Changes the window size
		private void SetWindowSize()
		{
			this.SizeToContent = SizeToContent.WidthAndHeight;
			
			this.ResizeMode = ResizeMode.NoResize;
		}

		private void SetGrid(Field field)
		{
			shopBombs = new Dictionary<BombKind, int>();
			
			InitData();
			
			ShowPlayerInfo();
			InitFieldComponents(field);
		}
		
		private void InitData()
		{
			size = 1;
			orientation = Gameplay.HORIZONTAL_ORIENTATION;
		}
		
		private void ShowPlayerInfo()
		{
			tbInformation.Text = GetPlayerMoneyInformation();
		}
		
		private string GetPlayerMoneyInformation()
		{
			return "You have " + GetCurrentPlayer().Money + " coins";
		}
		
		private void InitFieldComponents(Field field)
		{
			field.PreviewMouseLeftButtonDown += TryPasteShip;
			field.PreviewMouseLeftButtonDown += TryPasteBomb;
			spField.Children.Clear();
			spField.Children.Add(field);
			
			orientationGroup = new OrientationGroup(UpdateOrientation);
			spOrientationGroup.Children.Clear();
			spOrientationGroup.Children.Add(orientationGroup);
			
			bombsGroup = GetBombsRadioGroup();
			spBombsRadios.Children.Clear();
			spBombsRadios.Children.Add(bombsGroup);
			
			sizeRadios = new SizeRadios(field.MaxShipSize, UpdateSize);
			spSizeRadios.Children.Clear();
			spSizeRadios.Children.Add(sizeRadios);
		}
		
		private void UpdateSize(object sender, RoutedEventArgs e)
		{
			var rb = (RadioButton)sender;
			size = Int32.Parse(rb.Content.ToString());
		}
		
		private void UpdateOrientation(object sender, RoutedEventArgs e)
		{
			var rb = (RadioButton)sender;
			orientation = rb.Content.ToString();
		}
		
		private void TryPasteShip(object sender, RoutedEventArgs e)
		{
			var elem = (UIElement)e.Source;
			var row = Grid.GetRow(elem);
			var column = Grid.GetColumn(elem);
			GetCurrentField().TryPasteShip(row, column, size, orientation);
			if (GetCurrentField().AllPasted(size))
				sizeRadios.MakeDisabledAndJumpToNext(size);
		}
		
		private void TryPasteBomb(object sender, MouseButtonEventArgs e)
		{
			if (!GetCurrentField().IsAllShipsPasted())
				return;
			var kind = GetCurrentField().BombKind;
			if (!GetCurrentPlayer().ShopBombs.ContainsKey(kind) || GetCurrentPlayer().ShopBombs[kind] == 0)
				return;
			var elem = (UIElement)e.Source;
			var row = Grid.GetRow(elem);
			var column = Grid.GetColumn(elem);
			
			if (!GetCurrentField().PasteBomb(GetAnotherField(), row, column))
				return;
			
			GetCurrentPlayer().ShopBombs[kind]--;
			
			UpdateBombsRadioGroup();
		}
		
		private StackPanel GetBombsRadioGroup()
		{
			var spContent = new StackPanel();
			spContent.Orientation = Orientation.Vertical;
			bool first = true;
			var label = new Label();
			label.Content = "Place your bombs:";
			spContent.Children.Add(label);
			foreach (BombKind kind in (BombKind[])Enum.GetValues(typeof(BombKind)))
			{
				var rb = new RadioButton();
				if (first)
				{
					rb.IsChecked = true;
					first = false;
					bombKind = kind;
				}
				rb.Checked += (sender, e) => GetCurrentField().BombKind = kind;
				rb.GroupName = BOMBS_GROUP;
				rb.Tag = kind;
				rb.Content = kind + "(" + 0 + ")";
				spContent.Children.Add(rb);
			}
			return spContent;
		}
		
		private void OpenShop(object sender, RoutedEventArgs e)
		{
//			if (!grid.AllShipsPasted)
//			{
//				MessageBox.Show(ALL_SHIPS_ARE_NOT_PASTED, ERROR_TITLE);
//				return;
//			}
			Shop shop = new Shop(GetCurrentPlayer());
			shop.ShowDialog();
			ShowPlayerInfo();
			UpdateBombsRadioGroup();
		}
		
		private void UpdateBombsRadioGroup()
		{
			foreach (var elem in bombsGroup.Children)
			{
				if (!(elem is RadioButton))
				{
					continue;
				}
				var rb = (RadioButton)elem;
				var kind = (BombKind)rb.Tag;
				var count = 0;
				if (GetCurrentPlayer().ShopBombs.ContainsKey(kind))
					count = GetCurrentPlayer().ShopBombs[kind];
				rb.Content = rb.Tag + "(" + count + ")";
			}
		}
		
		private void NextStep(object sender, RoutedEventArgs e)
		{
			if (!GetCurrentField().IsAllShipsPasted())
			{
				MessageBox.Show(ALL_SHIPS_ARE_NOT_PASTED, ERROR_TITLE);
				return;
			}
			
			DisconnectGrid();
			PrepareFieldToGame(GetCurrentField());
			
			if (isFirstPlayerReady)
			{
				StartGame(sender, e);
				return;
			}
			isFirstPlayerReady = true;
			
			SetGrid(secondPlayer.Field);
		}
		
		private void PrepareFieldToGame(Field field)
		{
			field.PreviewMouseLeftButtonDown -= TryPasteBomb;
		}
		
		private void DisconnectGrid()
		{
			var parent = (Panel)GetCurrentField().Parent;
			parent.Children.Clear();
		}
		
		private void StartGame(object sender, RoutedEventArgs e)
		{
			secondPlayer.Field = GetCurrentField();
			
			GameField gameField = new GameField(firstPlayer, secondPlayer);
			
			this.Close();
			gameField.Show();
			
		}
		
		private void ShowMainWindow(object sender, RoutedEventArgs e)
		{
			StartMenu start = new StartMenu();
			start.Show();
			this.Close();
		}
		
		private Player GetAnotherPlayer()
		{
			return isFirstPlayerReady ? firstPlayer: secondPlayer;
		}
		
		private Player GetCurrentPlayer()
		{
			return isFirstPlayerReady ? secondPlayer : firstPlayer;
		}
		
		private Field GetCurrentField()
		{
			return isFirstPlayerReady ? secondPlayer.Field : firstPlayer.Field;
		}
		
		private Field GetAnotherField()
		{
			return isFirstPlayerReady ? firstPlayer.Field : secondPlayer.Field;
		}
	}

}