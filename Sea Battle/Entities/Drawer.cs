/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/05/2020
 * Time: 19:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Config;
using GameObjects;
using Processors;

namespace Entities
{
	/// <summary>
	/// Description of Drawer.
	/// </summary>
	public abstract class Drawer: Base
	{
		private string icon;
		private bool isCovered;
		private Canvas image;
		
		protected IFieldComponent fieldComponent;
		
		public Canvas Image
		{
			get
			{
				if (image == null)
				{
					image = new Canvas();
					this.icon = GetIcon();
					InitImage();
				}
				return image;
			}
			private set
			{
				image = value;
			}
		}
		
		public CellStatus Status
		{ get; private set; }
		
		protected Drawer() {}
		
		public Drawer(IFieldComponent fieldComponent)
		{
			this.fieldComponent = fieldComponent;
			this.Status = GetStatus();
			this.isCovered = false;
		}
		
		protected abstract string GetIcon();
		protected abstract CellStatus GetStatus();
		protected abstract List<UIElement> GetChildren();
		protected abstract void TransformImage();
		
		private void InitImage()
		{
			Image.Background = GetImage(this.icon);
			TransformImage();
			RefreshChildren();
		}
		
		private void RefreshChildren()
		{
			Image.Children.Clear();
			var children = GetChildren();
			foreach (var child in children)
				Image.Children.Add(child);
		}
		
		private ImageBrush GetImage(string icon)
		{
			return ImageProcessor.GetBackground(icon);
		}
		
		public void Cover()
		{
			isCovered = true;
			Image.Children.Clear();
			Image.Background = GetImage(Images.COVER);
		}
		
		public void Uncover()
		{
			isCovered = false;
			RefreshChildren();
			Image.Background = GetImage(this.icon);
		}
		
		public void GetDamage(int damage, ref int money, ref int opponentMoney)
		{
			fieldComponent.GetDamage(damage, ref money, ref opponentMoney);
			RefreshChildren();
		}
		
		public void Select()
		{
			if (isCovered)
				Image.Background = GetImage(Images.SELECTED_COVER);
			else
				Image.Opacity = 0.7;
		}
		
		public void Deselect()
		{
			if (isCovered)
				Cover();
			else
				Image.Opacity = 1;
		}
	}
}
