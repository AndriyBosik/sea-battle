/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 1:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

//using System.Windows;
using GameObjects;

using Processors;

using System;
using System.Windows.Media;
//using System.Windows.Media.Imaging;
using System.Windows.Controls;

using Config;

namespace GameParts
{
	/// <summary>
	/// Description of Cell.
	/// </summary>
	public class Cell
	{
		protected Point point;
		public string icon;
		protected Label image;
		protected CellStatus status;
		
		public Label Image
		{
			get
			{
				return image;
			}
			set
			{
				image = value;
			}
		}
		
		public CellStatus Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
			}
		}
		
		public Cell()
		{
			
		}
		
		public Cell(
			int x,
			int y,
			string icon = Images.EMPTY_CELL)
		{
			Init(x, y, icon);
		}
		
		protected void Init(int x, int y, string icon)
		{
			status = CellStatus.EMPTY;
			point = new Point(x, y);
			this.icon = icon;
			image = GetLabel();
		}
		
		private Label GetLabel()
		{
			Label label = new Label();
			label.Background = ImageProcessor.GetImage(this.icon);
			label.LayoutTransform = new RotateTransform(0);
			return label;
		}

	}
}
