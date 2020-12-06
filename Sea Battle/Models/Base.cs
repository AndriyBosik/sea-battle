/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11.11.2020
 * Time: 12:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Models
{
	/// <summary>
	/// Description of Base.
	/// </summary>
	public abstract class Base<T> where T: Base<T>
	{
		public static List<T> Items = new List<T>();
		
		public Guid ID
		{ get; private set; }
		
		public Base()
		{
			ID = Guid.NewGuid();
			Items.Add((T)this);
		}
		
		public Base(Guid id)
		{
			ID = id;
			Items.Add((T)this);
		}
	}
}