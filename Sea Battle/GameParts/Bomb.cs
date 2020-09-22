/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/22/2020
 * Time: 00:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using GameObjects;

namespace GameParts
{
	/// <summary>
	/// Description of Bomb.
	/// </summary>
	public class Bomb: Cell
	{
		public Bomb(int x, int y)
		{
			Init(x, y, Config.BOMB);
		}
	}
}
