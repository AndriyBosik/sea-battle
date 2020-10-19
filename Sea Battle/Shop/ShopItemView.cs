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
using System.Windows.Input;
using System.Windows.Controls;

using System.Windows.Media;
using Processors;

using Entities;

namespace Shop
{
	/// <summary>
	/// Description of ShopItemView.
	/// </summary>
	public class ShopItemView: Border
	{
		private const int IMAGE_WIDTH = 60;
		private const int IMAGE_HEIGHT = 60;
		
		public StackPanel spContent;
		private TextBlock description;
		private Button bBuy;
		
		public ShopItem Item
		{ get; private set; }
		
		public ShopItemView(ShopItem item, MouseButtonEventHandler buyMethod, int money)
		{
			this.Item = item;
			BorderBrush = Brushes.Green;
			BorderThickness = new Thickness(0);
			Child = InitContent(buyMethod);
			Refresh(money);
		}
		
		private Button AddBuyButton(MouseButtonEventHandler buyMethod)
		{
			bBuy = new Button();
			bBuy.Content = "BUY";
			bBuy.Tag = Item;
			bBuy.PreviewMouseLeftButtonDown += buyMethod;
			return bBuy;
		}
		
		private StackPanel GetDescription()
		{
			StackPanel sp = new StackPanel();
			sp.Orientation = Orientation.Horizontal;
			sp.VerticalAlignment = VerticalAlignment.Bottom;
			
			sp.Children.Add(ImageProcessor.GetImage(Item.icon, IMAGE_WIDTH, IMAGE_HEIGHT));
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
			Image im = ImageProcessor.GetImage(Item.icon, IMAGE_WIDTH, IMAGE_HEIGHT);
			im.Margin = new Thickness(0, 0, 10, 0);
			return im;
		}
		
		private StackPanel InitContent(MouseButtonEventHandler buyMethod)
		{
			spContent = new StackPanel();
			spContent.Orientation = Orientation.Vertical;
			
			spContent.Children.Add(GetDescription());
			spContent.Children.Add(AddBuyButton(buyMethod));
			
			spContent.HorizontalAlignment = HorizontalAlignment.Center;
			spContent.Margin = new Thickness(10);
			return spContent;
		}
		
		public bool TrySelect()
		{
			if (Item.BuyedCount != 0)
			{
				BorderThickness = new Thickness(2);
				return true;
			}
			return false;
		}
		
		public void Unselect()
		{
			BorderThickness = new Thickness(0);
		}
		
		public double GetHeight()
		{
			return spContent.ActualHeight;
		}
		
		public void Refresh(int money)
		{
			if (money >= Item.CostByOne && !Item.MaxCountBuyed())
			{
				Enable();
			}
			else
			{
				Disable();
			}
		}
		
		private void Enable()
		{
			bBuy.IsEnabled = true;
			description.IsEnabled = true;
		}
		
		private void Disable()
		{
			bBuy.IsEnabled = false;
			description.Opacity = 0.5;
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
