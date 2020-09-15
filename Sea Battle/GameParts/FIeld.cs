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
using System.Windows.Controls;

namespace GameParts
{
	/// <summary>
	/// Description of FIeld.
	/// </summary>
	public class Field: Grid
	{	
		public Field(int rows, int columns)
		{
			InitializeGrid(rows, columns);
			InitializeCells(rows, columns);
		}
		
		private void InitializeGrid(int rows, int columns)
		{
			for (int i = 0; i < rows; i++)
			{
				RowDefinitions.Add(new RowDefinition() { Height = new GridLength(Config.CELL_SIZE) });
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
				}
			}
		}
	}
}
