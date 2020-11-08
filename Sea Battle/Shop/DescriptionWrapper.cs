/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/26/2020
 * Time: 14:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Entities;
using ItemViews;

namespace Shop
{
	/// <summary>
	/// Description of DescriptionWrapper.
	/// </summary>
	public class DescriptionWrapper: ItemDescription
	{
		protected Button buyButton;
		protected Buyable buyable;
		
		public DescriptionWrapper(Picture<ShopItem> picture, int iconSize, int fontSize, int count, Buyable buyable): 
			base(picture, iconSize, fontSize, count)
		{
			this.buyable = buyable;
			CreateBuyButton();
			spContent.Children.Add(buyButton);
		}
		
		private void CreateBuyButton()
		{
			buyButton = new Button();
			buyButton.Content = "BUY";
			buyButton.Tag = Picture;
			buyButton.PreviewMouseLeftButtonDown += Buy;
		}
		
		private void Buy(object sender, RoutedEventArgs e)
		{
			IncreaseCount();
			buyable.Buy(sender, e);
		}
		
		public override void Enable()
		{
			base.Enable();
			buyButton.IsEnabled = true;
		}
		
		public override void Disable()
		{
			base.Disable();
			buyButton.IsEnabled = false;
		}
		
		public interface Buyable
		{
			void Buy(object sender, RoutedEventArgs e);
		}
	}
}
