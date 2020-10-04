/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 02.10.2020
 * Time: 23:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Entities
{
	/// <summary>
	/// Description of Database.
	/// </summary>
	public class Database
	{
		public static List<Gun> guns = new List<Gun>();
		public static List<Deck> decks = new List<Deck>();
		public static List<Ship> ships = new List<Ship>();
		public static List<Bomb> bombs = new List<Bomb>();
		public static List<BulletPack> bulletPacks = new List<BulletPack>();
		public static List<BulletPackInGun> bulletPackInGuns = new List<BulletPackInGun>();
	}
}
