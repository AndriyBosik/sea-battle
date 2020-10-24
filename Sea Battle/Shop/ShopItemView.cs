/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 12.10.2020
 * Time: 0:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Input;
using System.Windows.Controls;

using Entities;

namespace ItemViews
{
	/// <summary>
	/// Description of ShopItemView.
	/// </summary>
	public class ShopItemView: ItemDescription
	{
		private Button bBuy;
		
		public ShopItemView(ShopItem item, int iconSize, int fontSize, MouseButtonEventHandler buyMethod, int money):
			base(item, iconSize, fontSize)
		{
			spContent.Children.Add(AddBuyButton(buyMethod));
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
		
		public override bool Select()
		{
			if (Item.BuyedCount != 0)
			{
				base.Select();
				return true;
			}
			return false;
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
