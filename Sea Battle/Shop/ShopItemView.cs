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
using System.Windows.Input;

using Processors;

using Entities;

namespace Shop
{
	/// <summary>
	/// Description of ShopItemView.
	/// </summary>
	public class ShopItemView
	{
		private const int IMAGE_WIDTH = 60;
		private const int IMAGE_HEIGHT = 60;
		
		private ShopItem item;
		private StackPanel spContent;
		private TextBlock description;
		private Button bBuy;
		
		public ShopItemView(ShopItem item, MouseButtonEventHandler buyMethod)
		{
			this.item = item;
			InitContent(buyMethod);
		}
		
		private Button AddBuyButton(MouseButtonEventHandler buyMethod)
		{
			bBuy = new Button();
			bBuy.Content = "BUY";
			bBuy.Tag = item;
			bBuy.PreviewMouseLeftButtonDown += buyMethod;
			return bBuy;
		}
		
		private StackPanel GetDescription()
		{
			StackPanel sp = new StackPanel();
			sp.Orientation = Orientation.Horizontal;
			sp.VerticalAlignment = VerticalAlignment.Bottom;
			
			sp.Children.Add(ImageProcessor.GetImage(item.icon, IMAGE_WIDTH, IMAGE_HEIGHT));
			sp.Children.Add(GetInformation());
			
			return sp;
		}
		
		private TextBlock GetInformation()
		{
			description = new TextBlock();
			description.Text = item.ToString();
			description.Margin = new Thickness(10, 0, 0, 0);
			description.FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS");
			return description;
		}
		
		private Image GetImage()
		{
			Image im = ImageProcessor.GetImage(item.icon, IMAGE_WIDTH, IMAGE_HEIGHT);
			im.Margin = new Thickness(0, 0, 10, 0);
			return im;
		}
		
		private void InitContent(MouseButtonEventHandler buyMethod)
		{
			spContent = new StackPanel();
			spContent.Orientation = Orientation.Vertical;
			
			spContent.Children.Add(GetDescription());
			spContent.Children.Add(AddBuyButton(buyMethod));
			
			spContent.HorizontalAlignment = HorizontalAlignment.Center;
			spContent.Margin = new Thickness(10);
		}
		
		public double GetHeight()
		{
			return spContent.ActualHeight;
		}
		
		public UIElement GetView(int money)
		{
			Refresh(money);
			return spContent;
		}
		
		public void Refresh(int money)
		{
			if (money >= item.CostByOne)
			{
				bBuy.IsEnabled = true;
				description.IsEnabled = true;
			}
			else
			{
				bBuy.IsEnabled = false;
				description.Opacity = 0.5;
			}
		}
		
	}
}
