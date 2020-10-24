/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Entities
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
