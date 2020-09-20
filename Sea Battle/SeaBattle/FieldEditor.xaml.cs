/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/07/2020
 * Time: 19:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
using GameParts;

using GameObjects;

using Processors;

using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SeaBattle
{
	/// <summary>
	/// Interaction logic for FieldEditor.xaml
	/// </summary>
	public partial class FieldEditor : Window
	{
		
		private const int CELL_SIZE = 35;
		private const int MARGIN = 10;
		
		private Grid mainGrid;
		private string orientation;
		private int rows;
		private int columns;
		private readonly int maxShipSize;
		private Label[][] field;
		private CellStatus[][] status;
		
		public FieldEditor(int rows, int columns)
		{
			InitializeComponent();
			
			orientation = Config.HORIZONTAL_ORIENTATION;
			
			this.rows = rows;
			this.columns = columns;
			
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
			Button next = new Button();
			next.Content = "Next";
			next.IsEnabled = false;
			next.PreviewMouseLeftButtonUp += CreateAnotherGrid;
			
			Field mainGrid = new Field(rows, columns, maxShipSize, next);
			
			SizeToContent = SizeToContent.WidthAndHeight;
			DockPanel content = new DockPanel();
			StackPanel fieldWithSizeRadios = new StackPanel();
			fieldWithSizeRadios.Orientation = Orientation.Vertical;
			
			StackPanel sizeRadios = mainGrid.SizeRadios;
			StackPanel orientationGroup = mainGrid.OrientationGroup;
			orientationGroup.MinHeight = mainGrid.ActualHeight;
			
			StackPanel withBackButton = new StackPanel();
			withBackButton.Orientation = Orientation.Vertical;
			withBackButton.Margin = new Thickness(0, 0, 10, 0);
			
			Button goBack = new Button();
			goBack.Content = "Go back";
			goBack.PreviewMouseLeftButtonDown += ShowMainWindow;
			
			withBackButton.Children.Add(orientationGroup);
			withBackButton.Children.Add(next);
			withBackButton.Children.Add(goBack);
			
			fieldWithSizeRadios.Children.Add(mainGrid);
			fieldWithSizeRadios.Children.Add(sizeRadios);

			DockPanel.SetDock(fieldWithSizeRadios, Dock.Left);
			DockPanel.SetDock(withBackButton, Dock.Right);
			content.Children.Add(fieldWithSizeRadios);
			content.Children.Add(withBackButton);
			Content = content;
		}
		
		private void CreateAnotherGrid(object sender, RoutedEventArgs e)
		{
			SetGrid();
		}
		
		private void ShowMainWindow(object sender, RoutedEventArgs e)
		{
			StartMenu start = new StartMenu();
			start.Show();
			this.Close();
		}
		
	}

}