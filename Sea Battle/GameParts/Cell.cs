/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 1:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using GameObjects;
using System;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GameParts
{
	/// <summary>
	/// Description of Cell.
	/// </summary>
	public class Cell
	{
		protected Point point;
		protected string icon;
		protected Label image;
		
		public Label Image
		{
			get
			{
				return image;
			}
		}
		
		public Cell()
		{
			
		}
		
		public Cell(int x, int y)
		{
			Init(x, y, Config.EMPTY_CELL);
		}
		
		public Cell(int x, int y, string icon)
		{
			Init(x, y, icon);
		}
		
		protected void Init(int x, int y, string icon)
		{
			point = new Point(x, y);
			this.icon = icon;
			image = GetLabel();
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
			string path = Config.PROJECT_DIRECTORY + "Icons/" + icon + ".png";
			BitmapImage btm = new BitmapImage();
			btm.BeginInit();
			btm.UriSource = new Uri(path, UriKind.Relative);
			btm.EndInit();
			return new ImageBrush(btm);
		}
	}
}
