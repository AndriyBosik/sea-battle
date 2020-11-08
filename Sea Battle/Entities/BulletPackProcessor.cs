/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 11/08/2020
 * Time: 10:57
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Entities
{
	/// <summary>
	/// Description of BulletPackProcessor.
	/// </summary>
	public static class BulletPackProcessor
	{
		public static Picture<BulletPack> GenerateBulletPackPicture(GameObjects.BulletPack kind)
		{
			string icon = kind.ToString();
			switch (kind)
			{
				case GameObjects.BulletPack.SmallBullet:
					return new Picture<BulletPack>(new LinearBullets(2, 1, 10), icon);
				case GameObjects.BulletPack.MediumBullet:
					return new Picture<BulletPack>(new LinearBullets(3, 1, 10), icon);
				case GameObjects.BulletPack.LargeBullet:
					return new Picture<BulletPack>(new LinearBullets(4, 1, 10), icon);
				case GameObjects.BulletPack.SmallCore:
					return new Picture<BulletPack>(new Core(1, 10, 10), icon);
				case GameObjects.BulletPack.MediumCore:
					return new Picture<BulletPack>(new Core(2, 20, 30), icon);
				case GameObjects.BulletPack.LargeCore:
					return new Picture<BulletPack>(new Core(3, 30, 50), icon);
				default:
					return null;
			}
		}
		
		public static string GetBulletPackIcon(GameObjects.BulletPack kind)
		{
			return kind.ToString();
		}
		
		public static GameObjects.BulletPack? GetKind(BulletPack bulletPack)
		{
			foreach (GameObjects.BulletPack kind in (GameObjects.BulletPack[])Enum.GetValues(typeof(GameObjects.BulletPack)))
				if (bulletPack.Equals(BulletPackProcessor.GenerateBulletPackPicture(kind).Item))
					return kind;
			return null;
		}
	}
}
