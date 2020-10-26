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
using Processors;

namespace Entities
{
	/// <summary>
	/// Description of PlayerData.
	/// </summary>
	public class Player
	{
		
		private Field field;
		
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
			get { return SelectedBulletPack == null ? 1 : SelectedBulletPack.Radius; }
		}
		
		public bool Move
		{ get; set; }
		
		public Field Field
		{
			get { return field; }
			set { field = value; }
		}
		
		public int Money
		{
			get; set;
		}
		
		public Dictionary<BombKind, int> ShopBombs
		{ get; private set; }
		
		public List<Gun> Guns
		{ get; private set; }
		
		public List<BulletPack> BulletPacks;
		
		public int HealthPoints
		{
			get; set;
		}
		
		public Player()
		{
			this.Money = Gameplay.INITIAL_MONEY;
			this.HealthPoints = Gameplay.INITIAL_HEALTH_POINT;
			
			this.ShotDirection = Direction.UP;
			
			Guns = new List<Gun>();
			ShopBombs = new Dictionary<BombKind, int>();
			BulletPacks = new List<BulletPack>();
		}
		
		public void HealDeck(Deck deck)
		{
			int health = Math.Min(HealthPoints, deck.TotalHealth - deck.CurrentHealth);
			deck.Heal(health);
			HealthPoints -= health;
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
		
		public void Shot(Field field, int row, int column)
		{
			SelectedGun.Shot(field, SelectedBulletPack, new GameObjects.Point(row, column), ShotDirection);
		}
		
	}
}
