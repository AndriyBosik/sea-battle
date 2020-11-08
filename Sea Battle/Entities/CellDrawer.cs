/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/05/2020
 * Time: 19:46
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
	/// Description of CellDrawer.
	/// </summary>
	public class CellDrawer: Drawer
	{
		public CellDrawer(Cell cell): base(cell, Images.EMPTY_CELL)
		{}
		
//		protected override string GetIcon()
//		{
//			return Images.EMPTY_CELL;
//		}
		
		protected override CellStatus GetStatus()
		{
			return CellStatus.EMPTY;
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
