﻿/*
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
		public BombKind BombKind
		{
			get; set;
		}
		
		private const int MARGIN = 10;
		
		private int rows;
		private int columns;
		private int currentShipsCount;
		private List<Ship>[] ships;
		
		public int MaxShipSize
		{ get; set; }
		
		public Cell[][] cells;
		
		public Field(int rows, int columns)
		{
			//this.Margin = new Thickness(10);
			this.rows = rows;
			this.columns = columns;
			this.MaxShipSize = ShipProcessor.GetMaxShipSize(rows, columns);
			this.ships = new List<Ship>[MaxShipSize + 1];
			this.currentShipsCount = 0;
			
			InitializeGrid(rows, columns);
			InitializeCells(rows, columns);
		}
		
		public bool IsAllShipsPasted()
		{
			return ShipProcessor.ShipsCount(MaxShipSize) == currentShipsCount;
		}
		
		public void TryPasteShip(int row, int column, int size, string orientation)
		{
			if (EmptyAround(row, column, size, orientation))
			{
				PasteShip(row, column, size, orientation);
			}
		}
		
		public void PasteBomb(int row, int column)
		{
			if (cells[row][column].Status == CellStatus.BOMB)
				return;
			Children.Remove(cells[row][column].Image);
			var bomb = BombGenerator.Generate(row, column, BombKind);
			
			cells[row][column] = bomb;
			Repaint(row, column);
		}
		
		public void Repaint(int row, int column)
		{
			Cell cell = cells[row][column];
			Children.Remove(cell.Image);
			
			// Paste method for uncovering
			
			Grid.SetRow(cell.Image, row);
			Grid.SetColumn(cell.Image, column);
			Children.Add(cell.Image);
		}
		
		private bool EmptyAround(int row, int column, int size, string orientation)
		{
			for (int i = 0; i < size; i++)
			{
				if (orientation.Equals(Gameplay.VERTICAL_ORIENTATION))
				{
					if (!IsInsideField(row + i, column) || !CheckAroundOneDeck(row + i, column))
					{
						return false;
					}
				}
				else
				{
					if (!IsInsideField(row, column + i) || !CheckAroundOneDeck(row, column + i))
					{
						return false;
					}
				}
			}
			return true;
		}
		
		private bool CheckAroundOneDeck(int row, int column)
		{
			for (int i = -1; i <= 1; i++)
			{
				for (int j = -1; j <= 1; j++)
				{
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
		
		public bool IsInsideField(int x, int y)
		{
			return x >= 0 && x < rows && y >= 0 && y < columns;
		}
		
		private void PasteShip(int row, int column, int size, string orientation)
		{
			if (ships[size] != null && !ShipProcessor.CanPasteShip(size, ships[size].Count, MaxShipSize))
			{
				return;
			}
			
			Ship ship = new Ship(size, orientation);
			
			for (int i = 0; i < size; i++)
			{
				int currentRow = row + i;
				int currentColumn = column;
				if (orientation.Equals(Gameplay.HORIZONTAL_ORIENTATION))
				{
					currentRow = row;
					currentColumn = column + i;
				}
				Deck deck = new Deck(
					ship,
					DeckKindProcessor.GetDeckKind(i, size),
					orientation);
				cells[currentRow][currentColumn] = deck;
				
				Grid.SetRow(deck.Image, currentRow);
				Grid.SetColumn(deck.Image, currentColumn);
				Children.Add(deck.Image);
			}
			
			int index = ship.Size;
			if (ships[index] == null)
			{
				ships[index] = new List<Ship>();
			}
			ships[size].Add(ship);
			currentShipsCount++;
		}
		
		public bool AllPasted(int size)
		{
			if (ships[size] == null)
			{
				ships[size] = new List<Ship>();
			}
			return !ShipProcessor.CanPasteShip(size, ships[size].Count, MaxShipSize);
		}
		
		private void InitializeGrid(int rows, int columns)
		{
			cells = new Cell[rows][];
			
			for (int i = 0; i < rows; i++)
			{
				RowDefinitions.Add(new RowDefinition() { Height = new GridLength(Gameplay.CELL_SIZE) });
				cells[i] = new Cell[columns];
			}
			
			for (int i = 0; i < columns; i++)
			{
				ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Gameplay.CELL_SIZE) });
			}
		}
		
		private void InitializeCells(int rows, int columns)
		{
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					Cell cell = new Cell();
					Grid.SetRow(cell.Image, i);
					Grid.SetColumn(cell.Image, j);
					Children.Add(cell.Image);
					
					cells[i][j] = cell;
				}
			}
		}
		
		public Cell[][] GetField()
		{
			return this.cells;
		}
	}
}