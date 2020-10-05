/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/07/2020
 * Time: 19:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using FieldEditorParts;

using GameObjects;

using Config;

using Processors;

using System;
using System.Windows;
using System.Windows.Controls;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for FieldEditor.xaml
	/// </summary>
	public partial class FieldEditor : Window
	{
		
		private const int CELL_SIZE = 35;
		private const int MARGIN = 10;
		
		private string orientation;
		private int rows;
		private int columns;
		private readonly int maxShipSize;
		private Label[][] field;
		private CellStatus[][] status;
		
		private Field grid;
		
		private Field firstPlayer;
		private Field secondPlayer;
		
		public FieldEditor(int rows, int columns)
		{
			InitializeComponent();
			
			orientation = Gameplay.HORIZONTAL_ORIENTATION;
			
			this.rows = rows;
			this.columns = columns;
			
			InitializeField();
			
			maxShipSize = ShipProcessor.GetMaxShipSize(rows, columns);
			
			SetWindowSize();
			
			Button next = new Button();
			next.Content = "Next";
			next.IsEnabled = false;
			next.PreviewMouseLeftButtonUp += NextPlayer;
			
			SetGrid(next);
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

		private void SetGrid(Button b)
		{
			grid = new Field(rows, columns, maxShipSize, b);
			
			SizeToContent = SizeToContent.WidthAndHeight;
			DockPanel content = new DockPanel();
			StackPanel fieldWithSizeRadios = new StackPanel();
			fieldWithSizeRadios.Orientation = Orientation.Vertical;
			
			StackPanel sizeRadios = grid.SizeRadios;
			StackPanel orientationGroup = grid.OrientationGroup;
			orientationGroup.MinHeight = grid.ActualHeight;
			
			StackPanel withBackButton = new StackPanel();
			withBackButton.Orientation = Orientation.Vertical;
			withBackButton.Margin = new Thickness(0, 0, 10, 0);
			
			Button goBack = new Button();
			goBack.Content = "Go back";
			goBack.PreviewMouseLeftButtonDown += ShowMainWindow;
			
			withBackButton.Children.Add(orientationGroup);
			withBackButton.Children.Add(b);
			withBackButton.Children.Add(goBack);
			
			fieldWithSizeRadios.Children.Add(grid);
			fieldWithSizeRadios.Children.Add(sizeRadios);
			
			DockPanel.SetDock(fieldWithSizeRadios, Dock.Left);
			DockPanel.SetDock(withBackButton, Dock.Right);
			content.Children.Add(fieldWithSizeRadios);
			content.Children.Add(withBackButton);
			Content = content;
		}
		
		private void NextPlayer(object sender, RoutedEventArgs e)
		{
			DisconnectGrid();
			firstPlayer = grid;
			
			//grid.Children.Clear();
			
			Button start = new Button();
			start.Content = "Start game";
			start.IsEnabled = false;
			start.PreviewMouseLeftButtonUp += StartGame;
			
			SetGrid(start);
		}
		
		private void DisconnectGrid()
		{
			var smth = (Panel)grid.Parent;
			smth.Children.Clear();
		}
		
		private void StartGame(object sender, RoutedEventArgs e)
		{
			DisconnectGrid();
			secondPlayer = grid;
			
			//grid.Children.Clear();
			
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
		
	}

}