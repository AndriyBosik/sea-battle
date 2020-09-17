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
		private const string VERTICAL = "Vertical";
		private const string HORIZONTAL = "Horizontal";
		private const string ORIENTATION_GROUP = "Orientation";
		private const string SIZE_GROUP = "Size group";
		private const string preffix = "f";
		private const char separator = '_';
		private int[] xi = {-1, -1, -1, 0, 1, 1, 1, 0};
		private int[] yi = {-1, 0, 1, 1, 1, 0, -1, -1};
		
		private Grid mainGrid;
		private string orientation;
		private int rows;
		private int columns;
		private readonly int maxSizeShip;
		private Label[][] field;
		private CellStatus[][] status;
		
		public FieldEditor(int rows, int columns)
		{
			InitializeComponent();
			
			orientation = HORIZONTAL;
			
			this.rows = rows;
			this.columns = columns;
			
			InitializeField();
			
			maxSizeShip = QuantifyMaxSizeShip();
			
			SetWindowSize();
			
			SetGrid();
		}
		
		// Checks whatever we can place all the ships with sizes from 1 to size
		private bool CanPlace(int size)
		{
			int empty = rows*columns;
			int filled = 0;
			
			int currentSize = 1;
			int totalCount = size;
			int count = size;
			int currentRow = 0;
			int free = columns;
			while (currentSize <= size)
			{
				if (currentRow >= rows)
				{
					return false;
				}
				if ((currentSize + 1)*count - 1 <= free)
				{
					free -= Math.Min((currentSize + 1)*count, free);
					filled += currentSize*totalCount;
					currentSize++;
					totalCount--;
					count = totalCount;
				}
				else
				{
					int possibleCount = (free + 1) / (currentSize + 1);
					count -= possibleCount;
					currentRow += 2;
					free = columns;
				}
			}
			return filled*1.0/empty <= 1.0/5;
		}
		
		// Quantifies the maximum size of ship we can place
		private int QuantifyMaxSizeShip()
		{
			int l = 0;
			int r = Math.Max(rows, columns);
			while (r > l + 1)
			{
				int middle = (l + r) / 2;
				if (CanPlace(middle))
				{
					l = middle;
				}
				else
				{
					r = middle;
				}
			}
			return l;
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
			SizeToContent = SizeToContent.WidthAndHeight;
			DockPanel content = new DockPanel();
			StackPanel fieldWithSizeRadios = new StackPanel();
			fieldWithSizeRadios.Orientation = Orientation.Vertical;
			
			Field mainGrid = new Field(rows, columns);
			
			StackPanel sizeRadios = new SizeRadios(maxSizeShip, mainGrid);
			StackPanel orientationGroup = new OrientationGroup(mainGrid);

			fieldWithSizeRadios.Children.Add(mainGrid);
			fieldWithSizeRadios.Children.Add(sizeRadios);

			DockPanel.SetDock(fieldWithSizeRadios, Dock.Left);
			content.Children.Add(fieldWithSizeRadios);
			content.Children.Add(orientationGroup);
			Content = content;
		}
		
	}

}