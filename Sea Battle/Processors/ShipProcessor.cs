/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/19/2020
 * Time: 22:07
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Processors
{
	/// <summary>
	/// Description of FieldProcessor.
	/// </summary>
	public static class ShipProcessor
	{
		public static int GetMaxShipSize(int rows, int columns)
		{
			if (rows*columns < 5)
			{
				return 1;
			}
			int l = 0;
			int r = Math.Max(rows, columns);
			while (r > l + 1)
			{
				int middle = (l + r) / 2;
				if (CanPlace(middle, rows, columns))
				{
					l = middle;
				}
				else
				{
					r = middle;
				}
			}
			return l;
		}
		
		private static bool CanPlace(int size, int rows, int columns)
		{
			int empty = rows*columns;
			int filled = 0;
			
			int currentSize = 1;
			int totalCount = size;
			int count = size;
			int currentRow = 0;
			int free = columns;
			while (currentSize <= size)
			{
				if (currentRow >= rows)
				{
					return false;
				}
				if ((currentSize + 1)*count - 1 <= free)
				{
					free -= Math.Min((currentSize + 1)*count, free);
					filled += currentSize*totalCount;
					currentSize++;
					totalCount--;
					count = totalCount;
				}
				else
				{
					int possibleCount = (free + 1) / (currentSize + 1);
					count -= possibleCount;
					currentRow += 2;
					free = columns;
				}
			}
			return filled*1.0/empty <= 1.0/5;
		}
		
		public static bool CanPasteShip(int shipSize, int count, int maxSize)
		{
			return (count + 1) + shipSize <= maxSize + 1;
		}
	}
}
