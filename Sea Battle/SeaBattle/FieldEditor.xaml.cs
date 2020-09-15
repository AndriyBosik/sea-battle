/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/07/2020
 * Time: 19:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
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
		
		// Adds grid
		private void InitGrid(Grid mainGrid)
		{
			// Adding grid for field
			for (int i = 0; i < rows; i++)
			{
				mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CELL_SIZE) });
			}
			for (int j = 0; j < columns; j++)
			{
				mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(CELL_SIZE) });
			}
			
			// Adding additional column for radioButtons(Vertical/Horizontal)
			mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
			
			//Adding additional row for radioButtons(the count of decks on next ship)
			mainGrid.RowDefinitions.Add(new RowDefinition());
		}
		
		private void SetGrid()
		{
			
			Grid mainGrid = new Grid();
			mainGrid.Margin = new Thickness(MARGIN);
			
			InitGrid(mainGrid);
			
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					Label water = new Label();
					water.MouseLeftButtonUp += PasteShip;
					water.Background = GetBackground("water");
					water.Name = preffix + i + separator + j;
					water.LayoutTransform = new RotateTransform(0);
					
					Grid.SetRow(water, i);
					Grid.SetColumn(water, j);
					mainGrid.Children.Add(water);
					
					field[i][j] = water;
					status[i][j] = CellStatus.EMPTY;
				}
			}
			
			RadioButton rbHor = new RadioButton()
			{
				Content = HORIZONTAL,
				GroupName = ORIENTATION_GROUP,
				IsChecked = true
			};
			rbHor.Checked += ChangeOrientation;
			rbHor.Margin = new Thickness(10, 0, 0, 0);
			
			RadioButton rbVer = new RadioButton()
			{
				GroupName = ORIENTATION_GROUP,
				Content = VERTICAL
			};
			rbVer.Checked += ChangeOrientation;
			rbVer.Margin = new Thickness(10, 0, 0, 0);
			
			Grid.SetRow(rbHor, 0);
			Grid.SetColumn(rbHor, columns);
			mainGrid.Children.Add(rbHor);
			
			Grid.SetRow(rbVer, 1);
			Grid.SetColumn(rbVer, columns);
			mainGrid.Children.Add(rbVer);
			
			AddSizeRadios(mainGrid);
			
			this.Content = mainGrid;
		}
		
		// Adds radio buttons to change the size of a ship
		private void AddSizeRadios(Grid mainGrid)
		{
			for (int i = 1; i <= maxSizeShip; i++)
			{
				RadioButton rb = new RadioButton()
				{
					Content = i.ToString(),
					GroupName = SIZE_GROUP,
					IsChecked = i == 1
				};
				rb.Margin = new Thickness(10, 0, 0, 0);
				Grid.SetRow(rb, rows);
				Grid.SetColumn(rb, i - 1);
				mainGrid.Children.Add(rb);
			}
		}
		
		// Changes the orientation of the next pasted ship
		private void ChangeOrientation(object sender, RoutedEventArgs e)
		{
			orientation = (string)((RadioButton)sender).Content;
		}
		
		// Puts the ship
		private void PasteShip(object sender, RoutedEventArgs e)
		{
			Label current = (Label)sender;
			string name = current.Name;
			int x, y;
			GetCoords(name, out x, out y);
			
			if (status[x][y] == CellStatus.EMPTY && hasNoNeighbours(x, y))
			{
				field[x][y].Background = GetBackground("ship");
				status[x][y] = CellStatus.SHIP;
			} else if (status[x][y] == CellStatus.SHIP)
			{
				Label temp = field[x][y];
				RotateTransform rotation = (RotateTransform)temp.LayoutTransform;
				int newAngle;
				if (rotation == null)
				{
					newAngle = 0;
				} else {
					newAngle = (int)(rotation.Angle + 90) % 360;
				}
				temp.LayoutTransform = new RotateTransform(newAngle);
				//field[x][y] = temp;
			}
		}
		
		private bool hasNoNeighbours(int x, int y)
		{
			for (int i = 0; i < xi.Length; i++)
			{
				if (insideField(x + xi[i], y + yi[i]) && status[x + xi[i]][y + yi[i]] != CellStatus.EMPTY)
				{
					return false;
				}
			}
			return true;
		}
		
		private bool insideField(int x, int y)
		{
			return x >= 0 && x < rows && y >= 0 && y < columns;
		}
		
		private void GetCoords(string name, out int x, out int y)
		{
			string coordsWithSeparator = name.Substring(preffix.Length);
			string[] coords = coordsWithSeparator.Split(separator);
			x = Int32.Parse(coords[0]);
			y = Int32.Parse(coords[1]);
		}
		
		// Sets background to the ship
		private ImageBrush GetBackground(string filename)
		{
			string path = Config.projectDirectory + "Icons/" + filename + ".png";
			BitmapImage btm = new BitmapImage();
			btm.BeginInit();
			if (orientation.Equals(VERTICAL))
			{
				btm.Rotation = Rotation.Rotate90;
			}
			btm.UriSource = new Uri(path, UriKind.Relative);
			btm.EndInit();
			return new ImageBrush(btm);
		}
		
	}
}