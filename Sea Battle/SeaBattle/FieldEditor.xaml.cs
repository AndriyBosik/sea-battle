/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/07/2020
 * Time: 19:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using FieldEditorParts;

using Shop;

using GameObjects;

using Config;

using Entities;

using Processors;

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
		private const string ALL_SHIPS_ARE_NOT_PASTED = "You must place all the ships!!!";
		private const string ERROR_TITLE = "Error";
		private const int CELL_SIZE = 35;
		private const int MARGIN = 10;
		
		private StackPanel rightColumn;
		
		private string orientation;
		private int rows;
		private int columns;
		private readonly int maxShipSize;
		private Label[][] field;
		private TextBlock information;
		private CellStatus[][] status;
		private bool isFirstPlayerReady;
		
		private Field grid;
		
		private Player firstPlayer;
		private Player secondPlayer;
		
		public FieldEditor(int rows, int columns)
		{
			InitializeComponent();
			
			information = new TextBlock();
			
			orientation = Gameplay.HORIZONTAL_ORIENTATION;
			
			this.rows = rows;
			this.columns = columns;
			this.isFirstPlayerReady = false;
			
			firstPlayer = new Player();
			secondPlayer = new Player();
			
			InitializeField();
			
			maxShipSize = ShipProcessor.GetMaxShipSize(rows, columns);
			
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
			
			content.Children.Add(PlayerInfo());
			content.Children.Add(FieldComponents());
			//content.Children.Add(Footer());
			
			Border border = new Border();
			border.Padding = new Thickness(10);
			border.Child = content;
			Content = border;
		}
		
		private UIElement PlayerInfo()
		{
			information.Text = "You have " + GetCurrentPlayer().Money + " coins";
			information.FontSize = 22;
			information.TextWrapping = TextWrapping.Wrap;
			return information;
		}
		
		private UIElement FieldComponents()
		{
			StackPanel components = new StackPanel();
			components.Orientation = Orientation.Vertical;
			
			StackPanel horizontal = new StackPanel();
			horizontal.Orientation = Orientation.Horizontal;
			horizontal.Margin = new Thickness(10);
			
			grid = new Field(rows, columns, maxShipSize);
			
			horizontal.Children.Add(grid);
			
			rightColumn = new StackPanel();
			rightColumn.Orientation = Orientation.Vertical;
			rightColumn.Children.Add(grid.OrientationGroup);
			
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
			rightColumn.Margin = new Thickness(10, 0, 0, 0);
			
			horizontal.Children.Add(rightColumn);
			
			components.Children.Add(horizontal);
			components.Children.Add(grid.SizeRadios);
			
			return components;
		}
		
		private void OpenShop(object sender, RoutedEventArgs e)
		{
//			if (!grid.AllShipsPasted)
//			{
//				MessageBox.Show(ALL_SHIPS_ARE_NOT_PASTED, ERROR_TITLE);
//				return;
//			}
			Shop shop = new Shop(GetCurrentPlayer());
			if (shop.ShowDialog() == true)
			{
				PlayerInfo();
				AddBombsRadioButtons(shop.ShopBombs);
			}
		}
		
		private void AddBombsRadioButtons(List<ShopBomb> shopBombs)
		{
			int smallBombsCount = GetBombCount(shopBombs, BombKind.SmallBomb);
			int mediumBombsCount = GetBombCount(shopBombs, BombKind.MediumBomb);
			int largeBombsCount = GetBombCount(shopBombs, BombKind.LargeBomb);
			
			RadioButton small = new RadioButton();
			small.Content = BombKind.SmallBomb + "(" + smallBombsCount + ")";
			RadioButton medium = new RadioButton();
			medium.Content = BombKind.MediumBomb + "(" + mediumBombsCount + ")";
			RadioButton large = new RadioButton();
			large.Content = BombKind.LargeBomb + "(" + largeBombsCount + ")";
			rightColumn.Children.Add(small);
			rightColumn.Children.Add(medium);
			rightColumn.Children.Add(large);
		}
		
		private int GetBombCount(List<ShopBomb> shopBombs, BombKind kind)
		{
			ShopBomb sample = ShopBombGenerator.GenerateBomb(kind);
			return shopBombs.Where(shopBomb => shopBomb.Equals(sample)).Count();
		}
		
		private void NextStep(object sender, RoutedEventArgs e)
		{
			if (!grid.AllShipsPasted)
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
			
			GameField game = new GameField(firstPlayer, secondPlayer);
			
			this.Close();
			game.Show();
			
		}
		
		private void ShowMainWindow(object sender, RoutedEventArgs e)
		{
			StartMenu start = new StartMenu();
			start.Show();
			this.Close();
		}
		
		private Player GetCurrentPlayer()
		{
			return isFirstPlayerReady ? secondPlayer : firstPlayer;
		}
		
	}

}