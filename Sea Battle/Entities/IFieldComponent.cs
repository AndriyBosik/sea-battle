/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/05/2020
 * Time: 13:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Entities
{
	/// <summary>
	/// Description of IFieldComponent.
	/// </summary>
	public interface IFieldComponent
	{
		void GetDamage(int damage, ref int money, ref int opponentMoney);
	}
}
