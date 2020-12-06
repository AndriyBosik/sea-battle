/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 28.11.2020
 * Time: 9:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data.Common;
using GameObjects;
using Models;

namespace Database
{
	/// <summary>
	/// Description of ShipDAO.
	/// </summary>
	public class ShipDAO: DAO<Ship>
	{
		public const string ID = "ID";
		public const string X = "X";
		public const string Y = "Y";
		public const string SIZE = "Size";
		public const string ORIENTATION = "Orientation";
		public const string FIELD_ID = "FieldID";
		
		protected override string SetTableName()
		{
			return "Ships";
		}
		
		protected override void RemoveLinks(Guid id) {}
		
		protected override Ship Parse(DbDataReader reader)
		{
			var field = new FieldDAO().SelectByID(Guid.Parse(reader.GetString(5)));
			var x = Int32.Parse(reader.GetString(1));
			var y = Int32.Parse(reader.GetString(2));
			return new Ship(
				Guid.Parse(reader.GetString(0)),
				new Point(x, y),
				Int32.Parse(reader.GetString(3)),
				reader.GetString(4),
				field
			);
		}
		
		protected override List<string> KeysToInsert()
		{
			return new List<string> {
				ID,
				X,
				Y,
				SIZE,
				ORIENTATION,
				FIELD_ID
			};
		}
		
		protected override List<string> ValuesToInsert(Ship model)
		{
			return new List<string> {
				model.ID.ToString(),
				model.Point.X.ToString(),
				model.Point.Y.ToString(),
				model.Size.ToString(),
				model.Orientation,
				model.Field.ID.ToString()
			};
		}
		
		protected override List<string> KeysToUpdate()
		{
			return new List<string> {
				X,
				Y,
				SIZE,
				ORIENTATION,
				FIELD_ID
			};
		}
		
		protected override List<string> ValuesToUpdate(Ship model)
		{
			return new List<string> {
				model.Point.X.ToString(),
				model.Point.Y.ToString(),
				model.Size.ToString(),
				model.Orientation,
				model.Field.ID.ToString()
			};
		}
	}
}
