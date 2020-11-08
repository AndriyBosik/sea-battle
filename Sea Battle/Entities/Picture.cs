/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/07/2020
 * Time: 22:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Controls;
using System.Windows.Media;
using Processors;

namespace Entities
{
	/// <summary>
	/// Description of Picture.
	/// </summary>
	public class Picture<T>: Base
	{
		private Canvas image;
		
		public string Icon
		{ get; private set; }
		
		public T Item
		{ get; private set; }
		
		public Canvas Image
		{
			get
			{
				if (image == null)
					InitImage();
				return image;
			}
			private set
			{
				image = value;
			}
		}
		
		public Picture(T item, string icon)
		{
			this.Icon = icon;
			this.Item = item;
		}
		
		public override string ToString()
		{
			return Item.ToString();
		}
 
		
		protected virtual void InitImage()
		{
			image = new Canvas();
			image.Background = GetImage(Icon);
		}
		
		private ImageBrush GetImage(string icon)
		{
			return ImageProcessor.GetBackground(icon);
		}
		
	}
}
