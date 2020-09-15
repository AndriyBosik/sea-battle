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
using System.Windows.Media.Imaging;

namespace GameParts
{
	/// <summary>
	/// Description of Cell.
	/// </summary>
	public class Cell
	{
		private Point point;
		private string icon;
		private ImageBrush image;
		
		public ImageBrush Image
		{
			get
			{
				return image;
			}
		}
		
		public Cell(int x, int y)
		{
			point.X = x;
			point.Y = y;
			icon = Config.EMPTY_CELL;
			image = GetImage();
		}
		
		public Cell(int x, int y, string icon): this(x, y)
		{
			
			this.icon = icon;
		}
		
		private ImageBrush GetImage()
		{
			string path = Config.PROJECT_DIRECTORY + "Icons/" + icon + ".png";
			BitmapImage btm = new BitmapImage();
			btm.BeginInit();
			btm.UriSource = new Uri(path, UriKind.Relative);
			btm.EndInit();
			btm.Rotation = Rotation.Rotate90;
			return new ImageBrush(btm);
		}
	}
}
