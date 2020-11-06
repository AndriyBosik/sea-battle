﻿/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 22:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Windows.Controls;
using Config;
using Processors;

namespace Entities
{
	/// <summary>
	/// Description of BaseBomb.
	/// </summary>
	public class ShopItem: Base
	{
		private bool oneTimeBuyable;
		public string icon;
		
		public Canvas Image
		{ get; private set; }
		
		public bool BuyedMaxCount
		{ get; private set; }
		
		public int CostByOne
		{ get; protected set; }
		
		public int Damage
		{ get; protected set; }

		public ShopItem(int costByOne, int damage, string icon, bool oneTimeBuyable = false)
		{
			this.BuyedMaxCount = false;
			this.oneTimeBuyable = oneTimeBuyable;
			CostByOne = costByOne;
			Damage = damage;
			this.icon = icon;
			Init(icon);
		}
		
		protected void Init(string icon)
		{
			this.icon = icon;
			Image = new Canvas();
			Image.Background = ImageProcessor.GetBackground(this.icon);
		}
		
		public virtual void Buy()
		{
			if (oneTimeBuyable)
				BuyedMaxCount = true;
		}
		
		public bool MaxCountBuyed()
		{
			return oneTimeBuyable;
		}
		
	}
}
