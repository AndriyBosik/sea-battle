/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/05/2020
 * Time: 14:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using Entities;
using GameObjects;

namespace FieldDrawers
{
	/// <summary>
	/// Description of BombDrawer.
	/// </summary>
	public class BombDrawer: Drawer<Bomb>
	{
		private BombKind kind;
		
		public BombDrawer(Bomb fieldComponent, BombKind kind): base(fieldComponent)
		{
			this.kind = kind;
		}
		
		protected abstract string GetIcon()
		{
			return BombKindProcessor.GetIcon(kind);
		}
		
		protected abstract CellStatus GetStatus()
		{
			return CellStatus.BOMB;
		}
		
		protected abstract List<UIElement> GetChildren()
		{
			return new List<UIElement>();
		}
		
		protected abstract void TransformImage()
		{
			return;
		}
	}
}
