/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 10/25/2020
 * Time: 23:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Windows.Input;
using Entities;
using ItemViews;

namespace Shop
{
	/// <summary>
	/// Description of BulletPackDescription.
	/// </summary>
	public class BulletPackDescription: ShopItemView
	{
		private Gun gun;
		
		public BulletPackDescription(
			Gun gun,
			BulletPack bulletPack,
			int iconSize,
			int fontSize,
			MouseButtonEventHandler buyMethod,
			Player player):
			base(bulletPack, iconSize, fontSize, buyMethod, player)
		{
			this.gun = gun;
		}
		
		protected override void InitInformation()
		{
			base.InitInformation();
			description.Text += "You have: " + GetCount(gun);
		}
		
		private int GetCount(Gun gun)
		{
			var bullets = Database.bulletPackInGuns.Where(bpig => bpig.Gun == gun && bpig.BulletPack.Equals(Item))
				.FirstOrDefault();
			if (bullets == null)
				return 0;
			return bullets.Count;
		}
	}
}
