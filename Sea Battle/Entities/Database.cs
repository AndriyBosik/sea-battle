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
		public static List<BulletPack> bulletPacks = new List<BulletPack>();
		public static List<GameProcess> gameProcess = new List<GameProcess>();
		public static List<BulletPackInGun> bulletPackInGuns = new List<BulletPackInGun>();
		
		public static readonly List<Picture<BulletPack>> shopBulletPacks = new List<Picture<BulletPack>>()
		{
			BulletPackProcessor.GenerateBulletPackPicture(GameObjects.BulletPack.SmallBullet),
			BulletPackProcessor.GenerateBulletPackPicture(GameObjects.BulletPack.MediumBullet),
			BulletPackProcessor.GenerateBulletPackPicture(GameObjects.BulletPack.LargeBullet),
			BulletPackProcessor.GenerateBulletPackPicture(GameObjects.BulletPack.SmallCore),
			BulletPackProcessor.GenerateBulletPackPicture(GameObjects.BulletPack.MediumCore),
			BulletPackProcessor.GenerateBulletPackPicture(GameObjects.BulletPack.LargeCore),
		};
	}
}
