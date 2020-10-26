/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 1:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Windows.Shapes;
using GameObjects;

using Config;
using Processors;

using System;
using System.Windows.Media;
using System.Windows.Controls;

namespace Entities
{
	/// <summary>
	/// Description of Cell.
	/// </summary>
	public class Cell: Base
	{
		public string icon;
		protected Canvas image;
		protected CellStatus status;
		protected bool isCovered;
		
		public Canvas Image
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
		
		public Cell(
			string icon = Images.EMPTY_CELL): base()
		{
			Init(icon);
			isCovered = false;
		}
		
		protected void Init(string icon)
		{
			status = CellStatus.EMPTY;
			this.icon = icon;
			image = new Canvas();
			image.Background = GetImage();
		}
		
		private ImageBrush GetImage(string icon)
		{
			return ImageProcessor.GetBackground(icon);
		}
		
		private ImageBrush GetImage()
		{
			return ImageProcessor.GetBackground(icon);
		}
		
		public virtual void Uncover()
		{
			image.Background = GetImage();
			isCovered = false;
		}
		
		public void Select()
		{
			if (isCovered)
			{
				image.Background = GetImage(Images.SELECTED_COVER);
			}
			else
				image.Opacity = 0.7;
		}
		
		public void Deselect()
		{
			if (isCovered)
				Cover();
			else
				image.Opacity = 1;
		}
		
		public virtual void Cover()
		{
			isCovered = true;
			image.Background = GetImage(Images.COVER);
		}

	}
}
