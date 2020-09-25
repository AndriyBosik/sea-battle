/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/20/2020
 * Time: 15:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using GameParts;

using GameObjects;

using Config;

using System;
using System.Windows;
using System.Windows.Controls;


namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for Game.xaml
	/// </summary>
	public partial class GameField: Window
	{
		private Cell[][] firstPlayerField;
		private Cell[][] secondPlayerField;
		
		public GameField(Cell[][] firstPlayerField, Cell[][] secondPlayerField)
		{
			InitializeComponent();
			
			this.SizeToContent = SizeToContent.WidthAndHeight;
			this.ResizeMode = ResizeMode.NoResize;

			spContent.Margin = new Thickness(10, 10, 10, 10);
			
			this.firstPlayerField = firstPlayerField;
			this.secondPlayerField = secondPlayerField;
			
			Grid field = GetGrid(firstPlayerField);
			field.Name = Gameplay.FIRST_PLAYER_FIELD;
			spContent.Children.Add(field);
			
			spContent.Children.Add(GetSeparator());
			
			field = GetGrid(secondPlayerField);
			field.Name = Gameplay.SECOND_PLAYER_FIELD;
			spContent.Children.Add(field);
			
			spContent.MouseLeftButtonUp += ProcessMove;
		}
		
		private Label GetSeparator()
		{
			Label l = new Label();
			l.FontSize = 30;
			l.Content = "VS";
			l.VerticalAlignment = VerticalAlignment.Center;
			l.HorizontalAlignment = HorizontalAlignment.Center;
			return l;
		}
		
		private Grid GetGrid(Cell[][] firstPlayerField)
		{
			int rows = firstPlayerField.Length;
			int columns = firstPlayerField[0].Length;
			
			Grid grid = new Grid();
			
			for (int i = 0; i < rows; i++)
			{
				grid.RowDefinitions.Add(new RowDefinition() {Height = new GridLength(Gameplay.CELL_SIZE)} );
			}
			
			for (int i = 0; i < columns; i++)
			{
				grid.ColumnDefinitions.Add(new ColumnDefinition() {Width = new GridLength(Gameplay.CELL_SIZE)} );
			}
			
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					Grid.SetRow(firstPlayerField[i][j].Image, i);
					Grid.SetColumn(firstPlayerField[i][j].Image, j);
					grid.Children.Add(firstPlayerField[i][j].Image);
				}
			}
			grid.Margin = new Thickness(10, 10, 10, 10);
			return grid;
		}
		
		public void ProcessMove(object sender, RoutedEventArgs e)
		{
			Field field = GetField(e);
			
			Title = field.Name;
		}
		
		private Field GetField(RoutedEventArgs e)
		{
			if (!(e.Source is Label))
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