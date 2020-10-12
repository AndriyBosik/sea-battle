﻿/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 1:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

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
		}
		
		protected void Init(string icon)
		{
			status = CellStatus.EMPTY;
			this.icon = icon;
			image = GetImage();
		}
		
		private Canvas GetImage()
		{
			Canvas canvas = new Canvas();canvas.Background = ImageProcessor.GetBackground(this.icon);
			return canvas;
		}

	}
}
