/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11.11.2020
 * Time: 12:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Models
{
	/// <summary>
	/// Description of Base.
	/// </summary>
	public abstract class Base
	{
		public Guid ID
		{
			get;
			private set;
		}
		
		public Base()
		{
			ID = Guid.NewGuid();
		}
	}
}
