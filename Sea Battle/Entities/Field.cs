/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 11:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using GameObjects;

using Config;

using Processors;

using System;
using System.Windows;
using System.Windows.Controls;

using System.Collections.Generic;

namespace Entities
{
	/// <summary>
	/// Description of Field.
	/// </summary>
	public class Field: Grid
	{
		private int currentShipsCount;
		private int optimization = -1;
		private List<Bomb> bombs;
		private int[] shipsCounter;
		private Drawer[,] cells;
		
		public BombKind BombKind
		{ get; set; }
		
		public int Rows
		{ get; private set; }
		
		public int Columns
		{ get; private set; }
		
		public int MaxShipSize
		{ get; set; }
		
		public List<Ship> Ships
		{ get; private set; }
		
		public int Optimization
		{
			get
			{
				if (optimization < 0 && IsAllShipsPasted())
					optimization = CalculateOptimization();
				return optimization;
			}
		}
		
		public Field(int rows, int columns)
		{
			this.Rows = rows;
			this.Columns = columns;
			this.MaxShipSize = ShipProcessor.GetMaxShipSize(rows, columns);
			
			this.Ships = new List<Ship>();
			shipsCounter = new int[MaxShipSize + 1];
			
			this.bombs = new List<Bomb>();
			
			this.currentShipsCount = 0;
			cells = new Drawer[rows,columns];
			InitializeGrid(rows, columns);
			InitializeCells(rows, columns);
		}
		
		public bool AreAllShipsDestroyed()
		{
			foreach (var ship in Ships)
				if (!ship.IsDestroyed)
					return false;
			return true;
		}
		
		public bool IsAllShipsPasted()
		{
			return ShipProcessor.ShipsCount(MaxShipSize) == currentShipsCount;
		}
		
		private int CalculateOptimization()
		{
			int[,] a = new int[Rows,Columns];
			int result = 0;
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Columns; j++)
				{
					if (cells[i, j].Status == CellStatus.DECK)
					{
						result += IncreaseAround(a, i, j);
					}
				}
			}
			return result;
		}
		
		private int IncreaseAround(int[,] a, int x, int y)
		{
			int result = 0;
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
					var _x = x + i;
					var _y = y + j;
					if (!IsInsideField(_x, _y))
						continue;
					if (cells[_x,_y].Status != CellStatus.DECK)
						a[_x,_y]++;
					if (a[_x,_y] == 1)
						result++;
				}
			}
			return result;
		}
		
		public void TryPasteShip(int row, int column, int size, string orientation)
		{
			if (EmptyAround(row, column, size, orientation))
				PasteShip(row, column, size, orientation);
		}
		
		public bool PasteBomb(Field opponentField, int row, int column)
		{
			if (cells[row,column].Status != CellStatus.EMPTY)
				return false;
			Children.Remove(cells[row,column].Image);
			var pictureBomb = BombProcessor.Generate(opponentField, row, column, BombKind);
			bombs.Add(pictureBomb.Item);
			var bombDrawer = new BombDrawer(pictureBomb.Item, BombKind);
			cells[row,column] = bombDrawer;
			Repaint(row, column);
			return true;
		}
		
		private void Repaint(int row, int column)
		{
			var cell = cells[row,column];
			Children.Remove(cell.Image);
			InitCell(cell.Image, row, column);
		}
		
		private bool EmptyAround(int row, int column, int size, string orientation)
		{
			for (int i = 0; i < size; i++)
			{
				if (orientation.Equals(Gameplay.VERTICAL_ORIENTATION))
				{
					if (!IsInsideField(row + i, column) || !CheckAroundOneDeck(row + i, column))
						return false;
				}
				else
				{
					if (!IsInsideField(row, column + i) || !CheckAroundOneDeck(row, column + i))
						return false;
				}
			}
			return true;
		}
		
		private bool CheckAroundOneDeck(int row, int column)
		{
			for (int i = -1; i <= 1; i++)
				for (int j = -1; j <= 1; j++)
					if (IsInsideField(row + i, column + j) && cells[row+i,column+j].Status == CellStatus.DECK)
						return false;
			return true;
		}
		
		public bool IsInsideField(int x, int y)
		{
			return x >= 0 && x < Rows && y >= 0 && y < Columns;
		}
		
		private void PasteShip(int row, int column, int size, string orientation)
		{
			if (!ShipProcessor.CanPasteShip(size, shipsCounter[size], MaxShipSize))
				return;
			
			Ship ship = new Ship(size, orientation, new GameObjects.Point(row, column));
			
			for (int order = 0; order < size; order++)
			{
				int currentRow = row + order;
				int currentColumn = column;
				if (orientation.Equals(Gameplay.HORIZONTAL_ORIENTATION))
				{
					currentRow = row;
					currentColumn = column + order;
				}
				Children.Remove(cells[currentRow,currentColumn].Image);
				var deck = new Deck(ship);
				var deckDrawer = new DeckDrawer(deck, DeckKindProcessor.GetDeckKind(order, size), orientation);
				cells[currentRow,currentColumn] = deckDrawer;
				
				InitCell(deckDrawer.Image, currentRow, currentColumn);
			}
			
			Ships.Add(ship);
			shipsCounter[ship.Size]++;
			currentShipsCount++;
		}
		
		private void InitCell(UIElement elem, int row, int column)
		{
			Grid.SetRow(elem, row);
			Grid.SetColumn(elem, column);
			Children.Add(elem);
		}
		
		public bool AllPasted(int size)
		{
			return !ShipProcessor.CanPasteShip(size, shipsCounter[size], MaxShipSize);
		}
		
		private void InitializeGrid(int rows, int columns)
		{
			for (int i = 0; i < rows; i++)
				RowDefinitions.Add(new RowDefinition() { Height = new GridLength(Gameplay.CELL_SIZE) });
			
			for (int i = 0; i < columns; i++)
				ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Gameplay.CELL_SIZE) });
		}
		
		private void InitializeCells(int rows, int columns)
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					var cell = new Cell();
					var cellDrawer = new CellDrawer(cell);
					InitCell(cellDrawer.Image, i, j);

					cells[i,j] = cellDrawer;
				}
			}
		}
		
		public void Cover()
		{
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Columns; j++)
					cells[i,j].Cover();
		}
		
		public Drawer GetElement(int x, int y)
		{
			return cells[x,y];
		}
		
		public int GetPastedBombsCost()
		{
			int cost = 0;
			foreach (var bomb in bombs)
			{
				cost += bomb.CostByOne;
			}
			return cost;
		}
		
		public SerializableField GetSerializable()
		{
			return new SerializableField(Rows, Columns, Ships);
		}
		
		public static Field LoadSerializable(SerializableField ser)
		{
			var field = new Field(ser.Rows, ser.Columns);
			foreach (var serShip in ser.Ships)
			{
				var row = serShip.Point.X;
				var column = serShip.Point.Y;
				field.TryPasteShip(row, column, serShip.Size, serShip.Orientation);
			}
			return field;
		}
		
		public static bool CanReplaceWith(int firstFieldRows, int firstFieldColumns,
		                                  int secondFieldRows, int secondFiedColumns)
		{
			return (secondFieldRows <= firstFieldRows && secondFiedColumns <= firstFieldColumns);
		}
	}
}
