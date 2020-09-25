/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/22/2020
 * Time: 00:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Config;

namespace GameParts
{
	/// <summary>
	/// Description of Bomb.
	/// </summary>
	public class Bomb: Cell
	{
		protected int radius;
		
		public Bomb(int x, int y)
		{
			Init(x, y, Images.BOMB);
			radius = 1;
		}
	}
}
