/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Entities
{
	/// <summary>
	/// Description of Gun.
	/// </summary>
	public class Gun: Base
	{
		public int Cost
		{
			get;
			private set;
		}
		
		private string Icon
		{
			get;
			private set;
		}
		
		private int Deterioration
		{
			get;
			private set;
		}
		
		public Gun()
		{
			
			Database.guns.Add(this);
		}
	}
}
