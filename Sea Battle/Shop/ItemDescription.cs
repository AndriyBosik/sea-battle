/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 12.10.2020
 * Time: 0:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Media;
using Processors;

using Entities;

namespace ItemViews
{
	/// <summary>
	/// Description of ShopItemView.
	/// </summary>
	public class ItemDescription: Border
	{
		protected StackPanel spContent;
		protected TextBlock description;
		
		protected int iconSize;
		protected int fontSize;
		
		public int Count
		{ get; private set; }
		
		public Picture<ShopItem> Picture
		{ get; private set; }
		
		public ItemDescription(Picture<ShopItem> picture, int iconSize, int fontSize, int count)
		{
			this.Picture = picture;
			this.iconSize = iconSize;
			this.fontSize = fontSize;
			this.Count = count;
			BorderBrush = Brushes.Green;
			BorderThickness = new Thickness(0);
			Child = InitContent();
		}
		
		private StackPanel GetDescription()
		{
			StackPanel sp = new StackPanel();
			sp.Orientation = Orientation.Horizontal;
			sp.VerticalAlignment = VerticalAlignment.Bottom;
			
			sp.Children.Add(ImageProcessor.GetImage(Picture.Icon, iconSize, iconSize));
			description = new TextBlock();
			InitInformation();
			sp.Children.Add(description);
			
			return sp;
		}
		
		public void IncreaseCount()
		{
			Count++;
			InitInformation();
		}
		
		public void DecreaseCount()
		{
			Count--;
			InitInformation();
		}
		
		protected virtual void InitInformation()
		{
			description.Text = Picture + "You have: " + Count;
			description.Margin = new Thickness(10, 0, 0, 0);
			description.FontFamily = new FontFamily("Comic Sans MS");
		}
		
		private Image GetImage()
		{
			Image im = ImageProcessor.GetImage(Picture.Icon, iconSize, iconSize);
			im.Margin = new Thickness(0, 0, 10, 0);
			return im;
		}
		
		private StackPanel InitContent()
		{
			spContent = new StackPanel();
			spContent.Orientation = Orientation.Vertical;
			
			spContent.Children.Add(GetDescription());
			
			spContent.HorizontalAlignment = HorizontalAlignment.Center;
			spContent.Margin = new Thickness(10);
			return spContent;
		}
		
		public virtual void Disable()
		{
			description.Opacity = 0.5;
		}
		
		public virtual void Enable()
		{
			description.Opacity = 1;
		}
		
		public virtual bool Select()
		{
			BorderThickness = new Thickness(2);
			return true;
		}
		
		public void Deselect()
		{
			BorderThickness = new Thickness(0);
		}
		
		public bool Equals(object obj)
		{
			var other = (ItemDescription)obj;
			if (other == null)
			{
				return false;
			}
			return other.Picture == Picture;
		}
		
	}
}
