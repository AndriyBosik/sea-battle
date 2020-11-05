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
			if (lastDamageKind == DamageKind.LINEAR)
				DeselectLinearArea(field);
			else
				DeselectCircleArea(field);
			lastRow = lastColumn = -1;
		}
		
		private void DeselectLinearArea(Field field)
		{
			int x = 0;
			int y = 0;
			GetAdditors(ref x, ref y, lastDirection);
			for (int i = 0; i < lastRadius; i++)
				if (field.IsInsideField(lastRow + x*i, lastColumn + y*i))
					field.GetElement(lastRow + x*i, lastColumn + y*i).Deselect();
		}
		
		private void DeselectCircleArea(Field field)
		{
			for (int i = -lastRadius + 1; i <= lastRadius - 1; i++)
			{
				int leftSide = -(lastRadius - 1 - Math.Abs(i));
				int rightSide = -leftSide;
				for (int j = leftSide; j <= rightSide; j++)
				{
					int x = lastRow + i;
					int y = lastColumn + j;
					if (!field.IsInsideField(x, y))
						continue;
					field.GetElement(x, y).Deselect();
				}
			}
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
			if (kind == DamageKind.LINEAR)
				SelectLinearArea(field, row, column, radius, direction);
			else
				SelectCircleArea(field, row, column, radius);
			if (lastRow != row || lastColumn != column || lastRadius != radius)
			{
				lastRow = row;
				lastColumn = column;
				lastRadius = radius;
				lastDamageKind = kind;
				lastDirection = direction;
			}
		}
		
		private void SelectLinearArea(Field field, int row, int column, int radius, Direction direction)
		{
			int x = 0;
			int y = 0;
			GetAdditors(ref x, ref y, direction);
			for (int i = 0; i < radius; i++)
			{
				if (field.IsInsideField(row + x*i, column + y*i))
				{
					field.GetElement(row + x*i, column + y*i).Select();
				}
			}
		}
		
		private void SelectCircleArea(Field field, int row, int column, int radius)
		{
			for (int i = -radius + 1; i <= radius - 1; i++)
			{
				int leftSide = -(radius - 1 - Math.Abs(i));
				int rightSide = -leftSide;
				for (int j = leftSide; j <= rightSide; j++)
				{
					int x = row + i;
					int y = column + j;
					if (!field.IsInsideField(x, y))
						continue;
					field.GetElement(x, y).Select();
				}
			}
		}
	}
}
