/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/15/2020
 * Time: 22:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Serializators
{
	/// <summary>
	/// Description of FieldList.
	/// </summary>
	[Serializable]
	public class FieldList
	{
		public List<FieldDescription> Descriptions
		{ get; set; }
		
		public FieldList() {}
		
		[Serializable]
		public class FieldDescription
		{
			public string Name
			{ get; set; }
			
			public int Rows
			{ get; set; }
			
			public int Columns
			{ get; set; }
			
			public FieldDescription() {}
			
			public FieldDescription(string name, int rows, int columns)
			{
				Name = name;
				Rows = rows;
				Columns = columns;
			}
			
			public override string ToString()
			{
				return Name + "(Rows=" + Rows + ", Columns=" + Columns + ")";
			}
			
			public override bool Equals(object obj)
			{
				FieldDescription other = obj as FieldDescription;
				if (other == null)
					return false;
				return this.Name == other.Name;
			}

		}
	}
}
