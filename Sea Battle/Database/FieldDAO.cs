/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 28.11.2020
 * Time: 9:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data.Common;
using Models;

namespace Database
{
	/// <summary>
	/// Description of FieldDAO.
	/// </summary>
	public class FieldDAO: DAO<Field>
	{
		public const string ID = "ID";
		public const string ROWS = "Rows";
		public const string COLUMNS = "Columns";
		public const string OPTIMIZATION = "Optimization";
		
		protected override string SetTableName()
		{
			return "Fields";
		}
		
		protected override void RemoveLinks(Guid id)
		{
			new ShipDAO().DeleteByQuery(ShipDAO.FIELD_ID + "='" + id + "'");
		}
		
		protected override Field Parse(DbDataReader reader)
		{
			return new Field(
				Guid.Parse(reader.GetString(0)),
				Int32.Parse(reader.GetString(1)),
				Int32.Parse(reader.GetString(2)),
				Int32.Parse(reader.GetString(3))
			);
		}
		
		protected override List<string> KeysToInsert()
		{
			return new List<string> {
				ID,
				ROWS,
				COLUMNS,
				OPTIMIZATION
			};
		}
		
		protected override List<string> ValuesToInsert(Field model)
		{
			return new List<string> {
				model.ID.ToString(),
				model.Rows.ToString(),
				model.Columns.ToString(),
				model.Optimization.ToString()
			};
		}
		
		protected override List<string> KeysToUpdate()
		{
			return new List<string> {
				ROWS,
				COLUMNS,
				OPTIMIZATION
			};
		}
		
		protected override List<string> ValuesToUpdate(Field model)
		{
			return new List<string> {
				model.Rows.ToString(),
				model.Columns.ToString(),
				model.Optimization.ToString()
			};
		}
	}
}
