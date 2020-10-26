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

using Shop;

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
		private Label[][] field;
		private TextBlock information;
		private CellStatus[][] status;
		private bool isFirstPlayerReady;
		
		private Field grid;
		private BombKind bombKind;
		private Player firstPlayer;
		private Player secondPlayer;
		
		public FieldEditor(int rows, int columns)
		{
			InitializeComponent();
			information = new TextBlock();
			
			this.rows = rows;
			this.columns = columns;
			this.isFirstPlayerReady = false;
			
			firstPlayer = new Player();
			secondPlayer = new Player();
			
			InitializeField();
			
			SetWindowSize();
			
			SetGrid();
		}
		
		// Initializes arrays field and status
		private void InitializeField()
		{
			field = new Label[rows][];
			status = new CellStatus[rows][];
			for (int i = 0; i < rows; i++)
			{
				field[i] = new Label[columns];
				status[i] = new CellStatus[columns];
			}
		}
		
		// Changes the window size
		private void SetWindowSize()
		{
			this.SizeToContent = SizeToContent.WidthAndHeight;
			
			this.ResizeMode = ResizeMode.NoResize;
		}

		private void SetGrid()
		{
			StackPanel content = new StackPanel();
			content.Orientation = Orientation.Vertical;
			InitData();
			PlayerInfo();
			content.Children.Add(information);
			content.Children.Add(FieldComponents());
			//content.Children.Add(Footer());
			
			Border border = new Border();
			border.Padding = new Thickness(10);
			border.Child = content;
			Content = border;
		}
		
		private void InitData()
		{
			size = 1;
			orientation = Gameplay.HORIZONTAL_ORIENTATION;
		}
		
		private void PlayerInfo()
		{
			information = new TextBlock();
			information.Text = GetPlayerMoneyInformation();
			information.FontSize = 22;
			information.TextWrapping = TextWrapping.Wrap;
		}
		
		private string GetPlayerMoneyInformation()
		{
			return "You have " + GetCurrentPlayer().Money + " coins";
		}
		
		private UIElement FieldComponents()
		{
			StackPanel components = new StackPanel();
			components.Orientation = Orientation.Vertical;
			
			StackPanel horizontal = new StackPanel();
			horizontal.Orientation = Orientation.Horizontal;
			horizontal.Margin = new Thickness(10);
			
			grid = new Field(rows, columns);
			grid.PreviewMouseLeftButtonDown += TryPasteBomb;
			
			horizontal.Children.Add(grid);
			
			grid.PreviewMouseLeftButtonDown += TryPasteShip;
			
			rightColumn = new StackPanel();
			rightColumn.Orientation = Orientation.Vertical;
			orientationGroup = new OrientationGroup(ChangeOrientation);
			rightColumn.Children.Add(orientationGroup);
			
			Button goBack = new Button();
			goBack.Content = "Go back";
			goBack.PreviewMouseLeftButtonDown += ShowMainWindow;
			
			Button shop = new Button();
			shop.Content = "Shop";
			shop.PreviewMouseLeftButtonUp += OpenShop;
			
			Button next = new Button();
			next.Content = "Next";
			next.PreviewMouseLeftButtonUp += NextStep;
			
			rightColumn.Children.Add(next);
			rightColumn.Children.Add(shop);
			rightColumn.Children.Add(goBack);
			bombsGroup = GetBombsRadioGroup();
			rightColumn.Children.Add(bombsGroup);
			rightColumn.Margin = new Thickness(10, 0, 0, 0);
			
			horizontal.Children.Add(rightColumn);
			
			components.Children.Add(horizontal);
			sizeRadios = new SizeRadios(grid.MaxShipSize, ChangeSize);
			components.Children.Add(sizeRadios);
			
			return components;
		}
		
		private void ChangeSize(object sender, RoutedEventArgs e)
		{
			var rb = (RadioButton)sender;
			size = Int32.Parse(rb.Content.ToString());
		}
		
		private void ChangeOrientation(object sender, RoutedEventArgs e)
		{
			var rb = (RadioButton)sender;
			orientation = rb.Content.ToString();
		}
		
		private void TryPasteShip(object sender, RoutedEventArgs e)
		{
			var elem = (UIElement)e.Source;
			var row = Grid.GetRow(elem);
			var column = Grid.GetColumn(elem);
			grid.TryPasteShip(row, column, size, orientation);
			if (grid.AllPasted(size))
				sizeRadios.MakeDisabled(size);
		}
		
		private void TryPasteBomb(object sender, MouseButtonEventArgs e)
		{
			if (!grid.IsAllShipsPasted())
				return;
			var kind = grid.BombKind;
			if (!GetCurrentPlayer().ShopBombs.ContainsKey(kind) || GetCurrentPlayer().ShopBombs[kind] == 0)
				return;
			GetCurrentPlayer().ShopBombs[kind]--;
			
			var elem = (UIElement)e.Source;
			var row = Grid.GetRow(elem);
			var column = Grid.GetColumn(elem);
			
			grid.PasteBomb(row, column);
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
				rb.Checked += (object sender, RoutedEventArgs e) => grid.BombKind = kind;
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
			information.Text = GetPlayerMoneyInformation();
			UpdateShopBombs(shop.ShopBombs);
			UpdateBombsRadioGroup();
		}
		
		private void UpdateShopBombs(Dictionary<BombKind, int> shopBombs)
		{
			foreach (BombKind kind in (BombKind[])Enum.GetValues(typeof(BombKind)))
			{
				if (!GetCurrentPlayer().ShopBombs.ContainsKey(kind))
					GetCurrentPlayer().ShopBombs.Add(kind, 0);
				if (!shopBombs.ContainsKey(kind))
					continue;
				GetCurrentPlayer().ShopBombs[kind] += shopBombs[kind];
			}
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
				var count = GetCurrentPlayer().ShopBombs[kind];
				rb.Content = rb.Tag + "(" + count + ")";
			}
		}
		
		private void NextStep(object sender, RoutedEventArgs e)
		{
			if (!grid.IsAllShipsPasted())
			{
				MessageBox.Show(ALL_SHIPS_ARE_NOT_PASTED, ERROR_TITLE);
				return;
			}
			
			DisconnectGrid();
			
			if (isFirstPlayerReady)
			{
				StartGame(sender, e);
				return;
			}
			isFirstPlayerReady = true;
			firstPlayer.Field = grid;
			
			SetGrid();
		}
		
		private void DisconnectGrid()
		{
			var smth = (Panel)grid.Parent;
			smth.Children.Clear();
		}
		
		private void StartGame(object sender, RoutedEventArgs e)
		{
			secondPlayer.Field = grid;
			
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
		
	}

}