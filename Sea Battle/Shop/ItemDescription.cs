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
		public StackPanel spContent;
		protected TextBlock description;
		protected int iconSize;
		protected int fontSize;
		
		public ShopItem Item
		{ get; private set; }
		
		public ItemDescription(ShopItem item, int iconSize, int fontSize)
		{
			this.Item = item;
			this.iconSize = iconSize;
			this.fontSize = fontSize;
			BorderBrush = Brushes.Green;
			BorderThickness = new Thickness(0);
			Child = InitContent();
		}
		
		private StackPanel GetDescription()
		{
			StackPanel sp = new StackPanel();
			sp.Orientation = Orientation.Horizontal;
			sp.VerticalAlignment = VerticalAlignment.Bottom;
			
			sp.Children.Add(ImageProcessor.GetImage(Item.icon, iconSize, iconSize));
			sp.Children.Add(GetInformation());
			
			return sp;
		}
		
		private TextBlock GetInformation()
		{
			description = new TextBlock();
			description.Text = Item.ToString();
			description.Margin = new Thickness(10, 0, 0, 0);
			description.FontFamily = new FontFamily("Comic Sans MS");
			return description;
		}
		
		private Image GetImage()
		{
			Image im = ImageProcessor.GetImage(Item.icon, iconSize, iconSize);
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
			var other = (ShopItemView)obj;
			if (other == null)
			{
				return false;
			}
			return other.Item == Item;
		}
		
	}
}
