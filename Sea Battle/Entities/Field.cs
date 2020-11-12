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
		private List<Ship> ships;
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
		
		public Field(int rows, int columns)
		{
			this.Rows = rows;
			this.Columns = columns;
			this.MaxShipSize = ShipProcessor.GetMaxShipSize(rows, columns);
			
			this.ships = new List<Ship>();
			shipsCounter = new int[MaxShipSize + 1];
			
			this.currentShipsCount = 0;
			cells = new Drawer[rows,columns];
			InitializeGrid(rows, columns);
			InitializeCells(rows, columns);
		}
		
		public bool AreAllShipsDestroyed()
		{
			foreach (var ship in ships)
				if (!ship.IsDestroyed)
					return false;
			return true;
		}
		
		public bool IsAllShipsPasted()
		{
			return ShipProcessor.ShipsCount(MaxShipSize) == currentShipsCount;
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
			
			Ship ship = new Ship(size, orientation);
			
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
			
			ships.Add(ship);
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
	}
}
