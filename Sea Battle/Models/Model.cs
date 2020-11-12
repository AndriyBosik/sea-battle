/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11.11.2020
 * Time: 12:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Models
{
	/// <summary>
	/// Description of Model.
	/// </summary>
	public class Model<T>: Base
	{
		public static List<T> Items = new List<T>();
	}
}
