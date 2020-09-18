/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 11:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using GameObjects;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.Collections.Generic;

namespace GameParts
{
	/// <summary>
	/// Description of FIeld.
	/// </summary>
	public class Field: Grid
	{
		private const int MARGIN = 10;
		
		private int rows;
		private int columns;
		private string orientation;
		private int size;
		private Cell[][] cells;
		private List<Ship> ships;
		
		private Deck readyDeck;
		
		public string Orientation
		{
			get
			{
				return orientation;
			}
			set
			{
				orientation = value;
			}
		}
		
		public int Size
		{
			get
			{
				return size;
			}
			set
			{
				size = value;
			}
		}
		
		public Field(int rows, int columns)
		{
			readyDeck = new Deck(0, 0, DeckKind.ONE_DECK, Config.HORIZONTAL_ORIENTATION);
			
			this.orientation = Config.HORIZONTAL_ORIENTATION;
			this.size = 1;
			this.Margin = new Thickness(10);
			this.rows = rows;
			this.columns = columns;
			this.ships = new List<Ship>();
			
			InitializeGrid(rows, columns);
			InitializeCells(rows, columns);
			
			MouseLeftButtonUp += PasteShip;
		}
		
		private void PasteShip(object sender, MouseButtonEventArgs e)
		{
			var element = (UIElement)e.Source;
			int row = Grid.GetRow(element);
			int column = Grid.GetColumn(element);
			
			if (EmptyAround(row, column))
			{
				Paste(row, column);
			}
			
		}
		
		private bool EmptyAround(int row, int column)
		{
			for (int i = 0; i < size; i++)
			{
				if (orientation.Equals(Config.VERTICAL_ORIENTATION))
				{
					if (!IsInsideField(row + i, column) || !CheckAround(row + i, column))
					{
						return false;
					}
				}
				else
				{
					if (!IsInsideField(row, column + i) || !CheckAround(row, column + i))
					{
						return false;
					}
				}
			}
			return true;
		}
		
		private bool CheckAround(int row, int column)
		{
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					if (i == 0 && j == 0)
					{
						continue;
					}
					if (!IsInsideField(row + i, column + j))
					{
						continue;
					}
					if (cells[row+i][column+j].Status == CellStatus.DECK)
					{
						return false;
					}
				}
			}
			return true;
		}
		
		private bool IsInsideField(int row, int column)
		{
			return row >= 0 && row < rows && column >= 0 && column < columns;
		}
		
		private void Paste(int row, int column)
		{
			Ship ship = new Ship(row, column, size, orientation);
			
			//cells[0][0].Image = readyDeck.Image;
			//cells[5][3].Image = GetLabel();
			for (int i = 0; i < size; i++)
			{
				int currentRow = row + i;
				int currentColumn = column;
				if (orientation.Equals(Config.HORIZONTAL_ORIENTATION))
				{
					currentRow = row;
					currentColumn = column + i;
				}
				Deck deck = ship.GetDeck(i);
				cells[currentRow][currentColumn] = deck;
				
				Grid.SetRow(deck.Image, currentRow);
				Grid.SetColumn(deck.Image, currentColumn);
				Children.Add(deck.Image);
			}
			ships.Add(ship);
		}
		
		private Label GetLabel()
		{
			Label label = new Label();
			//label.MouseLeftButtonUp = SomeMethod;
			label.Background = GetImage();
			//label.Name = SomeName;
			label.LayoutTransform = new RotateTransform(0);
			return label;
		}
		
		private ImageBrush GetImage()
		{
			string path = Config.PROJECT_DIRECTORY + "Icons/background.png";
			BitmapImage btm = new BitmapImage();
			btm.BeginInit();
			btm.UriSource = new Uri(path, UriKind.Relative);
			btm.EndInit();
			return new ImageBrush(btm);
		}
		
		
		
		
		
		private void InitializeGrid(int rows, int columns)
		{
			cells = new Cell[rows][];
			
			for (int i = 0; i < rows; i++)
			{
				RowDefinitions.Add(new RowDefinition() { Height = new GridLength(Config.CELL_SIZE) });
				cells[i] = new Cell[columns];
			}
			
			for (int i = 0; i < columns; i++)
			{
				ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Config.CELL_SIZE) });
			}
		}
		
		private void InitializeCells(int rows, int columns)
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					Cell cell = new Cell(i, j);
					Grid.SetRow(cell.Image, i);
					Grid.SetColumn(cell.Image, j);
					Children.Add(cell.Image);
					
					cells[i][j] = cell;
				}
			}
		} 
	}
}
