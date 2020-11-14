/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/14/2020
 * Time: 20:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Entities
{
	/// <summary>
	/// Description of FieldList.
	/// </summary>
	[Serializable]
	public class FieldList
	{
		public List<string> List
		{ get; set; }
		
		public FieldList() {}
	}
}
