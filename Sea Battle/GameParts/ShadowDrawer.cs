/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/24/2020
 * Time: 12:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Entities;

using GameObjects;

namespace FieldEditorParts
{
	/// <summary>
	/// Description of ShadowDrawer.
	/// </summary>
	public class ShadowDrawer
	{
		private Game game;
		private Direction lastDirection;
		private int lastRow;
		private int lastColumn;
		private int lastRadius;
		private DamageKind lastDamageKind;
		
		public ShadowDrawer(Game game)
		{
			this.game = game;
			lastDirection = Direction.NO_DIRECTION;
			lastRow = lastColumn = -1;
		}
		
		private void DeselectLastArea()
		{
			if (lastRow == -1)
				return;
			var field = game.GetCurrentField();
			int x = 0;
			int y = 0;
			GetAdditors(ref x, ref y, lastDirection);
			for (int i = 0; i < lastRadius; i++)
			{
				if (field.IsInsideField(lastRow + x*i, lastColumn + y*i))
				{
					field.cells[lastRow + x*i][lastColumn + y*i].Deselect();
					field.Repaint(lastRow + x*i, lastColumn + y*i);
				}
			}
			lastRow = lastColumn = -1;
		}
		
		private void GetAdditors(ref int x, ref int y, Direction direction)
		{
			x = 0; y = 0;
			if (direction == Direction.UP)
				x = -1;
			if (direction == Direction.DOWN)
				x = 1;
			if (direction == Direction.LEFT)
				y = -1;
			if (direction == Direction.RIGHT)
				y = 1;
		}
		
		public void DrawSelectedArea(int row, int column, DamageKind kind, int radius, Direction direction)
		{
			DeselectLastArea();
			Field field = game.GetCurrentField();
			if (!field.IsInsideField(row, column))
				return;
			field.cells[row][column].Select();
			int x = 0;
			int y = 0;
			GetAdditors(ref x, ref y, direction);
			for (int i = 0; i < radius; i++)
			{
				if (field.IsInsideField(row + x*i, column + y*i))
				{
					field.cells[row + x*i][column + y*i].Select();
					field.Repaint(row + x*i, column + y*i);
				}
			}
			if (lastRow != row || lastColumn != column || lastRadius != radius)
			{
				lastRow = row;
				lastColumn = column;
				lastRadius = radius;
				lastDamageKind = kind;
				lastDirection = direction;
			}
			field.Repaint(row, column);
		}
	}
}
