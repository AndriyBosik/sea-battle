/*
 * Created by SharpDevelop.
 * User: Andriy
 * Date: 09/27/2020
 * Time: 15:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using System.Collections.Generic;

using System.Windows;
using Entities;

using Config;
using GameObjects;
using Models;
using Processors;

namespace Entities
{
	/// <summary>
	/// Description of PlayerData.
	/// </summary>
	public class Player
	{
		private Field field;
		
		public User User
		{ get; private set; }
		
		public Gun SelectedGun
		{ get; private set; }
		
		public BulletPack SelectedBulletPack
		{ get; private set; }
		
		public Direction ShotDirection
		{ get; private set; }
		
		public DamageKind DamageKind
		{
			get { return SelectedGun == null ? DamageKind.LINEAR : SelectedGun.DamageKind; }
		}
		
		public int Radius
		{
			get { return SelectedBulletPack == null ? 1 : SelectedBulletPack.Radius + SelectedGun.Bonus.Radius; }
		}
		
		public bool Move
		{ get; set; }
		
		public Field Field
		{
			get { return field; }
			set { field = value; }
		}
		
		public int Money
		{ get; set; }
		
		public Dictionary<BombKind, int> ShopBombs
		{ get; private set; }
		
		public List<Gun> Guns
		{ get; private set; }
		
		public string Name
		{ get { return User.Username; } }
		
		public List<BulletPack> BulletPacks;
		
		public Player(User user)
		{
			this.User = user;
			this.Money = Gameplay.INITIAL_MONEY;
			
			this.ShotDirection = Direction.UP;
			
			Guns = new List<Gun>();
			ShopBombs = new Dictionary<BombKind, int>();
			BulletPacks = new List<BulletPack>();
		}
		
		public void SelectGun(Gun gun)
		{
			SelectedGun = gun;
		}
		
		public void DeselectAll()
		{
			SelectedGun = null;
			SelectedBulletPack = null;
		}
		
		public void SelectBulletPack(BulletPack bulletPack)
		{
			SelectedBulletPack = bulletPack;
		}
		
		public void NextDirection()
		{
			ShotDirection = DirectionProcessor.GetNextDirection(ShotDirection);
		}
		
		public void PreviousDirection()
		{
			ShotDirection = DirectionProcessor.GetPreviousDirection(ShotDirection);
		}
		
		public int Shot(Field field, int row, int column)
		{
			int opponentMoney = 0;
			Money += SelectedGun.Shot(
				field, SelectedBulletPack, new GameObjects.Point(row, column), ShotDirection, ref opponentMoney);
			return opponentMoney;
		}
		
		public void BuyBomb(BombKind kind)
		{
			if (!ShopBombs.ContainsKey(kind))
				ShopBombs.Add(kind, 0);
			ShopBombs[kind]++;
			var shopBomb = ShopBombProcessor.GenerateBomb(kind);
			Money -= shopBomb.CostByOne;
			shopBomb.Buy();
		}
		
		public bool ReadyToShot()
		{
			return SelectedGun != null && SelectedBulletPack != null;
		}
		
		public void BuyGun(Gun gun)
		{
			Money -= gun.CostByOne;
			Guns.Add(gun);
			gun.Buy();
		}
		
		public bool HasBullets()
		{
			foreach (var gun in Guns)
			{
				foreach (var bulletPack in gun.BulletPacks)
					if (BulletPackInGun.GetCount(gun, bulletPack) != 0)
						return true;
				if (Money >= BulletPack.GetTheCheapestPrice(gun.DamageKind))
					return true;
			}
			return false;
		}
		
		public bool CanBuy(ShopItem item)
		{
			return Money >= item.CostByOne && !item.BuyedMaxCount;
		}
		
		public void SellUnpastedBombs()
		{
			foreach (BombKind kind in (BombKind[])Enum.GetValues(typeof(BombKind)))
			{
				if (!ShopBombs.ContainsKey(kind))
					continue;
				var count = ShopBombs[kind];
				Money += ShopBombProcessor.GenerateBomb(kind).CostByOne*count;
				ShopBombs.Remove(kind);
			}
		}
		
	}
}
