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
using System.Windows.Media;
using Config;
using GameObjects;
using Processors;

namespace Entities
{
	/// <summary>
	/// Description of Drawer.
	/// </summary>
	public abstract class Drawer: Picture<IFieldComponent>
	{
		private bool isCovered;
		
		public CellStatus Status
		{ get; private set; }
		
		public Drawer(IFieldComponent fieldComponent, string icon): base(fieldComponent, icon)
		{
			this.Status = GetStatus();
			this.isCovered = false;
			//ChangeImageCharacteristics();
		}
		
		protected abstract CellStatus GetStatus();
		protected abstract List<UIElement> GetChildren();
		protected abstract void TransformImage();
		
		protected override void InitImage()
		{
			base.InitImage();
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
			Image.Background = GetImage(this.Icon);
		}
		
		public void GetDamage(int damage, ref int money, ref int opponentMoney)
		{
			Item.GetDamage(damage, ref money, ref opponentMoney);
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
