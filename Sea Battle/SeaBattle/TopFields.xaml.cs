/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 28.11.2020
 * Time: 12:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using Database;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for TopFields.xaml
	/// </summary>
	public partial class TopFields : Window
	{
		private const int DEFAULT_COUNT = 10;
		
		private int currentIndex;
		private int rows;
		private int columns;
		private List<Models.Field> fields;
		
		public TopFields(int rows, int columns)
		{
			InitializeComponent();
			
			this.rows = rows;
			this.columns = columns;
			
			tbRows.Text = rows.ToString();
			tbColumns.Text = columns.ToString();
			tbCount.Text = DEFAULT_COUNT.ToString();
		}
		
		private void Get(object sender, EventArgs e)
		{
			int userRows = Int32.Parse(tbRows.Text);
			int userColumns = Int32.Parse(tbColumns.Text);
			int count = Int32.Parse(tbCount.Text);
			var fieldDAO = new FieldDAO();
			fields = fieldDAO.SelectAllByQuery(
				"[Rows]='" + userRows + "' and [Columns]='" + userColumns + "'",
				count,
				FieldDAO.OPTIMIZATION,
				FieldDAO.Sorting.asc);
			currentIndex = 0;
			RefreshContent();
		}
		
		private void RefreshContent()
		{
			tblOptimization.Text =
				"Optimization: " + Math.Round(fields[currentIndex].OptimizationPercentage, 2) + "%";
			PasteField();
			RefreshButtons();
		}
		
		private void RefreshButtons()
		{
			if (currentIndex == 0)
				bBack.IsEnabled = false;
			else
				bBack.IsEnabled = true;
			if (currentIndex == fields.Count - 1)
				bNext.IsEnabled = false;
			else
				bNext.IsEnabled = true;
		}
		
		private void PasteField()
		{
			var databaseField = fields[currentIndex];
			var shipDAO = new ShipDAO();
			var ships = shipDAO.SelectAllByQuery(ShipDAO.FIELD_ID + "='" + databaseField.ID + "'");
			var field = new Entities.Field(databaseField.Rows, databaseField.Columns);
			foreach (var ship in ships)
			{
				field.TryPasteShip(ship.Point.X, ship.Point.Y, ship.Size, ship.Orientation);
			}
			spField.Children.Clear();
			spField.Children.Add(field);
		}
		
		private void Back(object sender, EventArgs e)
		{
			currentIndex--;
			RefreshContent();
		}
		
		private void Next(object sender, EventArgs e)
		{
			currentIndex++;
			RefreshContent();
		}
		
		private void OK(object sender, EventArgs e)
		{
			DialogResult = true;
		}
	}
}