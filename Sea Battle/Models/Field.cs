/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 27.11.2020
 * Time: 23:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Models
{
	/// <summary>
	/// Description of Field.
	/// </summary>
	public class Field: Base<Field>
	{
		public int Rows
		{ get; private set; }
		
		public int Columns
		{ get; private set; }
		
		public int Optimization
		{ get; private set; }
		
		public double OptimizationPercentage
		{
			get
			{
				double cellsCount = Rows*Columns*1.0;
				return 100.0 - Optimization/cellsCount*100.0;
			}
		}
		
		public Field(int rows, int columns, int optimization)
		{
			Init(rows, columns, optimization);
		}
		
		public Field(Guid id, int rows, int columns, int optimization): base(id)
		{
			Init(rows, columns, optimization);
		}
		
		private void Init(int rows, int columns, int optimization)
		{
			Rows = rows;
			Columns = columns;
			Optimization = optimization;
		}
	}
}
