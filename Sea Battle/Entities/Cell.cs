/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 15.09.2020
 * Time: 1:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Windows.Shapes;
using GameObjects;

using Config;
using Processors;

using System;
using System.Windows.Media;
using System.Windows.Controls;

namespace Entities
{
	/// <summary>
	/// Description of Cell.
	/// </summary>
	public class Cell: Base, IFieldComponent
	{
		public void GetDamage(int damage, ref int money, ref int opponentMoney)
		{
			return;
		}
	}
}
