/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 12.10.2020
 * Time: 0:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace GameObjects
{
	/// <summary>
	/// Description of DamageKindProcessor.
	/// </summary>
	public static class DamageKindExtensions
	{
		public static string ToString(this DamageKind dk)
		{
			switch (dk)
			{
				case DamageKind.SPLASH:
					return "Splash";
				case DamageKind.LINEAR:
					return "Linear";
				default:
					return "One cell";
			}
		}
	}
}
