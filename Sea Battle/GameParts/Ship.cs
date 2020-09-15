/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 11:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Windows.Media;
using GameObjects;
using System;
using System.Windows.Controls;
using System.Collections.Generic;

namespace GameParts
{
	/// <summary>
	/// Description of Ship.
	/// </summary>
	public class Ship: Cell
	{
		protected int size;
		protected string orientation;
		private List<Ship> ship;
		
		public Ship(int x, int y)
		{
			Init(x, y, Config.DEFAULT_SHIP_SIZE, Config.ONE_DECK_SHIP, Config.HORIZONTAL_ORIENTATION);
		}
		
		protected void Init(int x, int y, int _size, string _icon, string _orientation)
		{
			base.Init(x, y, _icon);
			size = _size;
			orientation = _orientation;
			if (orientation.Equals(Config.VERTICAL_ORIENTATION))
			{
				image.LayoutTransform = new RotateTransform(90);
			}
		}
	}
}
