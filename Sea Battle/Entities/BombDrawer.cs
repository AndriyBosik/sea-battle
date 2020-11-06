/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/05/2020
 * Time: 19:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Windows;
using Config;
using GameObjects;

namespace Entities
{
	/// <summary>
	/// Description of BombDrawer.
	/// </summary>
	public class BombDrawer: Drawer
	{
		private BombKind kind;
		
		private BombDrawer(Bomb fieldComponent): base(fieldComponent)
		{}
		
		public BombDrawer(Bomb fieldComponent, BombKind kind): base(fieldComponent)
		{
			this.kind = kind;
		}
		
		protected override string GetIcon()
		{
			return BombKindProcessor.GetIcon(kind);
		}
		
		protected override CellStatus GetStatus()
		{
			return CellStatus.BOMB;
		}
		
		protected override List<UIElement> GetChildren()
		{
			return new List<UIElement>();
		}
		
		protected override void TransformImage()
		{
			return;
		}
	}
}
